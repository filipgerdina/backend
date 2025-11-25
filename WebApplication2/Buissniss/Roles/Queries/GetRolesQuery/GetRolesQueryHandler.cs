using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using MediatR;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery; // for SortItem on the request, if you reuse it here
using WebApplication2.Models;
using WebApplication2.Models.Request;                        // <-- DxLinq + DxSortItem live here
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Roles.Queries.GetRolesQuery
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, CoreListResponse<GetRolesQueryDTO>>
    {
        private readonly RoleService _roleService;
        private readonly PagesService _pagesService;

        public GetRolesQueryHandler(RoleService roleService, PagesService pagesService)
        {
            _roleService = roleService;
            _pagesService = pagesService;
        }

        public async Task<CoreListResponse<GetRolesQueryDTO>> Handle(GetRolesQuery request, CancellationToken cancellationT)
        {
            var query =
                (from r in _roleService.GetRoles().AsQueryable()
                 join p in _pagesService.GetPages().AsQueryable()
                     on r.ID_home_page equals p.Id into gj
                 from p in gj.DefaultIfEmpty()
                 select new GetRolesQueryDTO
                 {
                     Id = r.Id,
                     Name = r.Name,
                     DefaultPage = p != null ? p.Name : null,
                     DefaultPageId = p != null ? p.Id : null
                     
                 });

            query = query.AsQueryable(); // if you need IQueryable for your helpers

            // FILTER (DevExtreme array)
            if (request.filter is { Length: > 0 })
            {
                try
                {
                    var root = JsonSerializer.SerializeToElement(request.filter);
                    var predicate = DxLinq.BuildDxFilterExpression<GetRolesQueryDTO>(root);
                    if (predicate != null)
                        query = query.Where(predicate);
                }
                catch
                {
                    // optionally log malformed filters
                }
            }

            // TOTAL (before paging)
            int? totalCount = null;
            if (request.requireTotalCount)
                totalCount = query.Count();

            // SORT
            if (request.sort is { Count: > 0 })
            {
                // If your request.sort uses a different type than DxSortItem, map it:
                var sorts = request.sort
                    .Select(s => new DxSortItem { selector = s.selector, desc = s.desc })
                    .ToList();

                query = DxLinq.ApplySort(query, sorts);
            }

            // PAGING
            if (request.skip.HasValue) query = query.Skip(request.skip.Value);
            if (request.take.HasValue) query = query.Take(request.take.Value);

            // MATERIALIZE
            var data = query.ToList();

            // RESPONSE
            var result = new CoreListResponse<GetRolesQueryDTO>
            {
                Data = data
            };

            if (request.requireTotalCount && totalCount.HasValue)
                result.TotalCount = totalCount.Value;

            return await Task.FromResult(result);
        }
    }
}
