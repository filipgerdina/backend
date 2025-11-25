using MediatR;
using WebApplication2.Buissniss.Roles.Queries.GetRolesGroupQuery;
using WebApplication2.Buissniss.Roles.Queries.GetRolesQuery;
using WebApplication2.Models.Request;
using WebApplication2.Services;
using System.Linq;
using System.Text.Json;
using WebApplication2.Buissniss.Roles.Queries.GetPagesOfRoleQuery;
using WebApplication2.Models.Responses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication2.Buissniss.Roles.Queries.GetRolesGroupQuery
{
    public class GetRolesGroupQueryHandler : IRequestHandler<GetRolesGroupQuery, CoreListResponse<GroupResponse>>
    {
        private readonly RoleService _roleService;
        private readonly PagesService _pagesService;

        public GetRolesGroupQueryHandler(RoleService roleService, PagesService pagesService)
        {
            _roleService = roleService;
            _pagesService = pagesService;
        }

        public async Task<CoreListResponse<GroupResponse>> Handle(GetRolesGroupQuery request, CancellationToken ct)
        {
            // Base row query (IQueryable)
            var query =
                from r in _roleService.GetRoles().AsQueryable()   // IQueryable<Role>
                join p in _pagesService.GetPages().AsQueryable()  // IQueryable<Page>
                    on r.ID_home_page equals p.Id into gj
                from p in gj.DefaultIfEmpty()
                select new GetRolesQueryDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    DefaultPage = p != null ? p.Name : null,
                    DefaultPageId = p != null ? p.Id : null
                };

            // FILTER
            if (request.filter is { Length: > 0 })
            {
                var root = JsonSerializer.SerializeToElement(request.filter);
                var pred = DxLinq.BuildDxFilterExpression<GetRolesQueryDTO>(root);
                if (pred != null) query = query.Where(pred);
            }

            // ROW SORT (applies to items inside groups when isExpanded = true)
            if (request.sort is { Count: > 0 })
            {
                query = DxLinq.ApplySort(query, request.sort);
            }

            // GROUP (single level)
            var g = request.group?.FirstOrDefault();
            if (g == null || string.IsNullOrWhiteSpace(g.selector))
                return await Task.FromResult(new CoreListResponse<GroupResponse>());

            var keyGetter = DxLinq.BuildBoxingGetter<GetRolesQueryDTO>(g.selector);

            // Grouping (client-side; switch to IQueryable GroupBy if needed)
            var grouped = query.AsEnumerable().GroupBy(keyGetter);

            // GROUP SORT:
            //  - If sort contains "$count", sort groups by count
            //  - Else if sort contains the same selector as the group, use that direction
            //  - Else fall back to group's own desc flag (by key)
            var sortByCount = request.sort?.FirstOrDefault(s => s.selector.Equals("$count", StringComparison.OrdinalIgnoreCase));
            var sortByGroupKey = request.sort?.FirstOrDefault(s => s.selector.Equals(g.selector, StringComparison.OrdinalIgnoreCase));

            if (sortByCount != null)
            {
                grouped = sortByCount.desc
                    ? grouped.OrderByDescending(gr => gr.Count())
                    : grouped.OrderBy(gr => gr.Count());
            }
            else
            {
                bool desc = sortByGroupKey?.desc ?? g.desc;
                grouped = desc
                    ? grouped.OrderByDescending(gr => gr.Key?.ToString())
                    : grouped.OrderBy(gr => gr.Key?.ToString());
            }

            // Group-level paging
            if (request.skip.HasValue) grouped = grouped.Skip(request.skip.Value);
            if (request.take.HasValue) grouped = grouped.Take(request.take.Value);

            // Shape response
            var rows = grouped.Select(gr => new GroupResponse
            {
                key = gr.Key,
                items = g.isExpanded ? gr.ToList() : null, // keep null when collapsed
                count = gr.Count()
            }).ToList();

            var result = new CoreListResponse<GroupResponse>
            {
                Data = rows
            };
            return await Task.FromResult(result);
        }
    }
}