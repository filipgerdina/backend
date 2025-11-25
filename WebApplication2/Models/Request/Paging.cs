using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using WebApplication2.Models.Responses;

namespace WebApplication2.Models.Request
{
    // Simple paging DTO (optional)
    public sealed class Paging
    {
        public int? skip { get; set; }
        public int? take { get; set; }
        public bool requireTotalCount { get; set; }
    }

    // DevExtreme-like sort/group/request/response models (rename if you already have these elsewhere)
    public sealed class DxSortItem
    {
        public string selector { get; set; } = string.Empty;
        public bool desc { get; set; }
    }

    public sealed class GroupItem
    {
        public string selector { get; set; } = string.Empty;
        public bool desc { get; set; }
        public bool isExpanded { get; set; }
    }

    public sealed class GroupResponse : CoreQueryEntityDTO
    {
        public object? key { get; set; }
        public object? items { get; set; } = null; // keep null when collapsed
        public int count { get; set; }
    }

    /// <summary>
    /// All DevExtreme LINQ helpers in one place.
    /// </summary>
    public static class DxLinq
    {
        // ---------- Sorting ----------

        public static IQueryable<T> ApplySort<T>(IQueryable<T> source, IList<DxSortItem> sorts)
        {
            if (sorts == null || sorts.Count == 0) return source;

            IOrderedQueryable<T>? ordered = null;

            for (int i = 0; i < sorts.Count; i++)
            {
                var s = sorts[i];
                var (lambda, propType) = BuildPropertyAccessor<T>(s.selector);
                if (lambda == null) continue;

                if (i == 0)
                {
                    ordered = s.desc
                        ? CallOrderBy(source, nameof(Queryable.OrderByDescending), lambda, propType)
                        : CallOrderBy(source, nameof(Queryable.OrderBy), lambda, propType);
                }
                else
                {
                    ordered = s.desc
                        ? CallThenBy(ordered!, nameof(Queryable.ThenByDescending), lambda, propType)
                        : CallThenBy(ordered!, nameof(Queryable.ThenBy), lambda, propType);
                }
            }

            return ordered ?? source;
        }

        private static IOrderedQueryable<T> CallOrderBy<T>(IQueryable<T> src, string method, LambdaExpression keySelector, Type keyType)
        {
            var call = Expression.Call(typeof(Queryable), method, new[] { typeof(T), keyType }, src.Expression, Expression.Quote(keySelector));
            return (IOrderedQueryable<T>)src.Provider.CreateQuery<T>(call);
        }

        private static IOrderedQueryable<T> CallThenBy<T>(IOrderedQueryable<T> src, string method, LambdaExpression keySelector, Type keyType)
        {
            var call = Expression.Call(typeof(Queryable), method, new[] { typeof(T), keyType }, src.Expression, Expression.Quote(keySelector));
            return (IOrderedQueryable<T>)src.Provider.CreateQuery<T>(call);
        }

        // Case-insensitive Property accessor (supports dotted paths)
        public static (LambdaExpression? lambda, Type propType) BuildPropertyAccessor<T>(string? propPath)
        {
            if (string.IsNullOrWhiteSpace(propPath))
                return (null, typeof(object));

            var param = Expression.Parameter(typeof(T), "x");
            Expression body = param;
            Type type = typeof(T);

            foreach (var part in propPath.Split('.'))
            {
                var prop = type.GetProperty(part, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (prop == null) return (null, typeof(object));
                body = Expression.Property(body, prop);
                type = prop.PropertyType;
            }

            var lambda = Expression.Lambda(body, param);
            return (lambda, type);
        }

        // ---------- Filtering (DevExtreme array filter) ----------

        // Chain-aware, skips invalid fields, supports NOT and nested groups.
        public static Expression<Func<T, bool>>? BuildDxFilterExpression<T>(JsonElement element)
        {
            // Simple condition: ["field","op","value"]
            if (element.ValueKind == JsonValueKind.Array &&
                element.GetArrayLength() >= 3 &&
                element[0].ValueKind == JsonValueKind.String &&
                element[1].ValueKind == JsonValueKind.String &&
                element[2].ValueKind != JsonValueKind.Undefined)
            {
                var field = element[0].GetString()!;
                var op = element[1].GetString()!;
                var valueEl = element[2];

                var (member, param) = BuildMemberAccess<T>(field); // case-insensitive
                if (member == null) return null; // unknown field -> skip

                var constant = ConvertJsonTo(member.Type, valueEl);

                if (member.Type == typeof(string))
                {
                    return op.ToLowerInvariant() switch
                    {
                        "contains" => BuildStringCall<T>(param, member, nameof(string.Contains), constant),
                        "startswith" => BuildStringCall<T>(param, member, nameof(string.StartsWith), constant),
                        "endswith" => BuildStringCall<T>(param, member, nameof(string.EndsWith), constant),
                        "=" => BuildBinary<T>(param, member, Expression.Equal, constant),
                        "<>" or "!=" => BuildBinary<T>(param, member, Expression.NotEqual, constant),
                        _ => BuildBinary<T>(param, member, MapBinary(op), constant)
                    };
                }

                return op.ToLowerInvariant() switch
                {
                    "=" => BuildBinary<T>(param, member, Expression.Equal, constant),
                    "<>" or "!=" => BuildBinary<T>(param, member, Expression.NotEqual, constant),
                    ">" => BuildBinary<T>(param, member, Expression.GreaterThan, constant),
                    ">=" => BuildBinary<T>(param, member, Expression.GreaterThanOrEqual, constant),
                    "<" => BuildBinary<T>(param, member, Expression.LessThan, constant),
                    "<=" => BuildBinary<T>(param, member, Expression.LessThanOrEqual, constant),
                    _ => null
                };
            }

            // Unary NOT: ["!", cond] or ["not", cond]
            if (element.ValueKind == JsonValueKind.Array &&
                element.GetArrayLength() == 2 &&
                element[0].ValueKind == JsonValueKind.String &&
                (string.Equals(element[0].GetString(), "!", StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(element[0].GetString(), "not", StringComparison.OrdinalIgnoreCase)))
            {
                var inner = BuildDxFilterExpression<T>(element[1]);
                if (inner == null) return null;
                var p = Expression.Parameter(typeof(T), "x");
                var body = ReplaceParam(inner.Body, inner.Parameters[0], p);
                return Expression.Lambda<Func<T, bool>>(Expression.Not(body), p);
            }

            // Group/chain: [cond, "and|or", cond, ...] any length
            if (element.ValueKind == JsonValueKind.Array && element.GetArrayLength() >= 1)
            {
                Expression? aggregate = null;
                var p = Expression.Parameter(typeof(T), "x");
                string currentOp = "and";

                for (int i = 0; i < element.GetArrayLength(); i++)
                {
                    var token = element[i];

                    if (token.ValueKind == JsonValueKind.String)
                    {
                        var opStr = token.GetString();
                        if (string.Equals(opStr, "and", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(opStr, "or", StringComparison.OrdinalIgnoreCase))
                        {
                            currentOp = opStr!.ToLowerInvariant();
                        }
                        continue;
                    }

                    var cond = BuildDxFilterExpression<T>(token);
                    if (cond == null) continue; // skip invalid

                    var condBody = ReplaceParam(cond.Body, cond.Parameters[0], p);

                    aggregate = aggregate == null
                        ? condBody
                        : currentOp == "and"
                            ? Expression.AndAlso(aggregate, condBody)
                            : Expression.OrElse(aggregate, condBody);
                }

                if (aggregate == null)
                    return x => true; // nothing valid -> no filtering

                return Expression.Lambda<Func<T, bool>>(aggregate, p);
            }

            return null;
        }

        private static (MemberExpression? member, ParameterExpression param) BuildMemberAccess<T>(string field)
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression body = param;
            Type cur = typeof(T);
            foreach (var part in field.Split('.'))
            {
                var prop = cur.GetProperty(part, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (prop == null) return (null, param);
                body = Expression.Property(body, prop);
                cur = prop.PropertyType;
            }
            return ((MemberExpression)body, param);
        }

        private static Expression<Func<T, bool>> BuildStringCall<T>(ParameterExpression param, MemberExpression member, string method, ConstantExpression constant)
        {
            var notNull = Expression.NotEqual(member, Expression.Constant(null, typeof(string)));
            var call = Expression.Call(member, typeof(string).GetMethod(method, new[] { typeof(string) })!, constant);
            var body = Expression.AndAlso(notNull, call);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        private static Expression<Func<T, bool>> BuildBinary<T>(ParameterExpression param, MemberExpression member, Func<Expression, Expression, BinaryExpression> op, ConstantExpression constant)
        {
            Expression left = member;
            Expression right = constant;

            if (left.Type != right.Type)
            {
                if (Nullable.GetUnderlyingType(left.Type) == right.Type)
                    right = Expression.Convert(right, left.Type);
                else if (Nullable.GetUnderlyingType(right.Type) == left.Type)
                    left = Expression.Convert(left, right.Type);
                else
                    right = Expression.Convert(right, left.Type);
            }

            var body = op(left, right);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        private static Func<Expression, Expression, BinaryExpression> MapBinary(string op) => op switch
        {
            ">" => Expression.GreaterThan,
            ">=" => Expression.GreaterThanOrEqual,
            "<" => Expression.LessThan,
            "<=" => Expression.LessThanOrEqual,
            _ => Expression.Equal
        };

        private static ConstantExpression ConvertJsonTo(Type targetType, JsonElement el)
        {
            object? val = null;
            var nn = Nullable.GetUnderlyingType(targetType);
            var t = nn ?? targetType;

            if (t == typeof(string))
                val = el.ValueKind == JsonValueKind.Null ? null : el.GetString();
            else if (t == typeof(int))
                val = el.ValueKind == JsonValueKind.Number ? el.GetInt32() : int.TryParse(el.ToString(), out var i) ? i : 0;
            else if (t == typeof(long))
                val = el.ValueKind == JsonValueKind.Number ? el.GetInt64() : long.TryParse(el.ToString(), out var l) ? l : 0L;
            else if (t == typeof(bool))
                val = el.ValueKind == JsonValueKind.True || (el.ValueKind == JsonValueKind.String && bool.TryParse(el.GetString(), out var b) && b);
            else if (t == typeof(DateTime))
            {
                if (el.ValueKind == JsonValueKind.String && DateTime.TryParse(el.GetString(), out var dt)) val = dt;
            }
            else
            {
                var s = el.ToString();
                try { val = Convert.ChangeType(s, t); } catch { val = null; }
            }

            return Expression.Constant(val, nn != null ? typeof(Nullable<>).MakeGenericType(t) : t);
        }

        private static Expression ReplaceParam(Expression body, ParameterExpression from, ParameterExpression to)
            => new ParamReplacer(from, to).Visit(body)!;

        private sealed class ParamReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _from;
            private readonly ParameterExpression _to;
            public ParamReplacer(ParameterExpression from, ParameterExpression to) { _from = from; _to = to; }
            protected override Expression VisitParameter(ParameterExpression node) => node == _from ? _to : base.VisitParameter(node);
        }

        // ---------- Grouping helper ----------

        // Builds a boxed getter for grouping (case-insensitive)
        public static Func<T, object?> BuildBoxingGetter<T>(string propPath)
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression body = param;
            Type type = typeof(T);

            foreach (var part in propPath.Split('.'))
            {
                var prop = type.GetProperty(part, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (prop == null) return _ => null;
                body = Expression.Property(body, prop);
                type = prop.PropertyType;
            }

            var boxed = Expression.Convert(body, typeof(object));
            return Expression.Lambda<Func<T, object?>>(boxed, param).Compile();
        }
    }
}
