using MediatR;
using System.Security.Claims;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUserInformationQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetPagesQuery
{
    public class GetPagesQueryHandler : IRequestHandler<GetPagesQuery, CoreListResponse<GetPagesQueryDTO>>
    {
        private readonly UserService _userService;
        private readonly PagesService _pagesService;
        private readonly PagePermissionsService _pagePermissionsService;
        private readonly NavigationGroupService _navigationGroupService;
        private readonly NavigationGroupPermissionsService _navigationGroupPermissionsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPagesQueryHandler(UserService userService, PagesService pagesService, PagePermissionsService pagePermissionsService, NavigationGroupService navigationGroupService, NavigationGroupPermissionsService navigationGroupPermissionsService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _pagesService = pagesService;
            _pagePermissionsService = pagePermissionsService;
            _navigationGroupService = navigationGroupService;
            _navigationGroupPermissionsService = navigationGroupPermissionsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CoreListResponse<GetPagesQueryDTO>> Handle(GetPagesQuery request, CancellationToken cancellationT)
        {
            var result = new CoreListResponse<GetPagesQueryDTO>();

            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                result.Messages.Add(new WebApplication2.Models.IResponse.Message
                {
                    Text = "User not authenticated."
                });
                return result;
            }

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEntity = _userService.GetUserById(Int32.Parse(userIdClaim));
            var userRoleIds = _userService.GetRolesForUser(userEntity).Select(r => r.Id).ToList();

            var allPages = _pagesService.GetPages().ToList();
            var allNavGroups = _navigationGroupService.GetNavigationGroups().ToList();

            // Admin has access to all
            if (userRoleIds.Contains(1))
            {
                result.Data = allPages.Select(p => new GetPagesQueryDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    PageComponent = p.PageComponent,
                    Path = p.Path,
                    IconUrl = p.IconUrl,
                    Module = p.Module,
                    NavigationGroup = p.NavigationGroup
                }).ToList();
                return result;
            }

            // Step 1: Get permitted page IDs
            var permittedPageIds = _pagePermissionsService.GetPagePermissions()
                .Where(p => p.RoleId != null && userRoleIds.Contains((int)p.RoleId))
                .Select(p => p.PageId)
                .Distinct()
                .ToHashSet();

            // Step 2: Get directly permitted nav group IDs
            var directGroupIds = _navigationGroupPermissionsService.GetNavigationGroupPermissions()
                .Where(p => p.RoleId != null && userRoleIds.Contains((int)p.RoleId))
                .Select(p => p.NavigationGroupId)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToHashSet();

            // Step 3: Recursively collect parent group IDs
            HashSet<int> GetAllParentGroups(IEnumerable<int> groupIds)
            {
                var allowed = new HashSet<int>(groupIds);
                var parentMap = allNavGroups.ToDictionary(g => g.Id, g => g.ParentGroupId);

                var queue = new Queue<int>(groupIds);
                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    if (parentMap.TryGetValue(current, out var parentId) && parentId.HasValue && !allowed.Contains(parentId.Value))
                    {
                        allowed.Add(parentId.Value);
                        queue.Enqueue(parentId.Value);
                    }
                }

                return allowed;
            }

            var allowedGroupIds = GetAllParentGroups(directGroupIds);

            foreach (var item in directGroupIds)
            {
                var tmp = GetGroupSubgroups(item, new List<int>());
                foreach (var item1 in tmp)
                {
                    if (!allowedGroupIds.Contains(item1))
                        allowedGroupIds.Add(item1);
                }
            }

            // Step 4: Filter pages
            var userPages = allPages
                .Where(p =>
                    permittedPageIds.Contains(p.Id) ||
                    (p.NavigationGroup != null && allowedGroupIds.Contains(p.NavigationGroup.Id))
                )
                .ToList();

            // Step 5: Map to DTO
            result.Data = userPages.Select(p => new GetPagesQueryDTO
            {
                Id = p.Id,
                Name = p.Name,
                PageComponent = p.PageComponent,
                Path = p.Path,
                IconUrl = p.IconUrl,
                Module = p.Module,
                NavigationGroup = p.NavigationGroup
            }).ToList();

            return result;
        }
        private List<int> GetGroupSubgroups(int groupId, List<int> list)
        {
            list.Add(groupId);

            foreach (var item in _navigationGroupService.GetNavigationGroups())
            {
                if (item.ParentGroupId == groupId)
                {
                    if (_navigationGroupService.GetNavigationGroups().Any(g => g.ParentGroupId == item.Id))
                        GetGroupSubgroups(item.Id, list);
                    else
                        list.Add(item.Id);
                }
            }

            return list;
        }
    }
}
