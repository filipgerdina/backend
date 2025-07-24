using MediatR;
using System.Security.Claims;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetNavigationGroupsQuery
{
    public class GetNavigationGroupsQueryHandler : IRequestHandler<GetNavigationGroupsQuery, CoreListResponse<GetNavigationGroupsQueryDTO>>
    {
        private readonly UserService _userService;
        private readonly PagesService _pagesService;
        private readonly PagePermissionsService _pagePermissionsService;
        private readonly NavigationGroupService _navigationGroupService;
        private readonly NavigationGroupPermissionsService _navigationGroupPermissionsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetNavigationGroupsQueryHandler(UserService userService, PagesService pagesService, PagePermissionsService pagePermissionsService, NavigationGroupService navigationGroupService, NavigationGroupPermissionsService navigationGroupPermissionsService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _pagesService = pagesService;
            _pagePermissionsService = pagePermissionsService;
            _navigationGroupService = navigationGroupService;
            _navigationGroupPermissionsService = navigationGroupPermissionsService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<CoreListResponse<GetNavigationGroupsQueryDTO>> Handle(GetNavigationGroupsQuery request, CancellationToken cancellationT)
        {
            var result = new CoreListResponse<GetNavigationGroupsQueryDTO>();

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

            var allNavGroups = _navigationGroupService.GetNavigationGroups().ToList();
            var allPages = _pagesService.GetPages().ToList();

            // Admin: return all groups
            if (userEntity.Is_System)
            {
                result.Data = allNavGroups.Select(g => new GetNavigationGroupsQueryDTO
                {
                    Id = g.Id,
                    ParentGroupId = g.ID_parent_group,
                    Name = g.Name,
                    IconUrl = g.Icon
                }).ToList();
                return result;
            }

            // Step 1: Get permitted page IDs
            var permittedPageIds = _pagePermissionsService.GetPagePermissions()
                .Where(p => p.ID_role != null && userRoleIds.Contains((int)p.ID_role))
                .Select(p => p.ID_page)
                .ToHashSet();

            // Step 2: Pages user has access to
            var userPages = allPages
                .Where(p => permittedPageIds.Contains(p.Id) && p.ID_group != null)
                .ToList();

            // Step 3: Collect groups from user-accessible pages
            var groupIdsToInclude = new HashSet<int>();
            var groupMap = allNavGroups.ToDictionary(g => g.Id, g => g);

            foreach (var page in userPages)
            {
                var currentGroupId = page.ID_group;

                while (currentGroupId != null && groupMap.TryGetValue((int)currentGroupId, out var group))
                {
                    if (!groupIdsToInclude.Add(group.Id)) break; // Already included

                    if (group.ID_parent_group.HasValue)
                        currentGroupId = group.ID_parent_group.Value;
                    else
                        break;
                }
            }

            // Step 4: Add groups the user has direct access to, and their parents
            var permittedNavGroupIds = _navigationGroupPermissionsService.GetNavigationGroupPermissions()
                .Where(p => p.ID_role != null && userRoleIds.Contains((int)p.ID_role))
                .Select(p => p.ID_navigation_group)
                .ToList();

            foreach (var groupId in permittedNavGroupIds)
            {
                var currentGroupId = groupId;

                while (groupMap.TryGetValue(currentGroupId, out var group))
                {
                    if (!groupIdsToInclude.Add(group.Id)) break; // Already included

                    if (group.ID_parent_group.HasValue)
                        currentGroupId = group.ID_parent_group.Value;
                    else
                        break;
                }
            }

            foreach (var item in permittedNavGroupIds)
            {
                var tmp = GetGroupSubgroups(item, new List<int>());
                foreach (var item1 in tmp)
                {
                    if (!groupIdsToInclude.Contains(item1))
                        groupIdsToInclude.Add(item1);
                }
            }

            // Step 4: Return those groups
            result.Data = allNavGroups
                .Where(g => groupIdsToInclude.Contains(g.Id))
                .Select(g => new GetNavigationGroupsQueryDTO
                {
                    Id = g.Id,
                    ParentGroupId = g.ID_parent_group,
                    Name = g.Name,
                    IconUrl = g.Icon
                }).ToList();

            return result;
        }

        private List<int> GetGroupSubgroups(int groupId, List<int> list) {
            list.Add(groupId);

            foreach (var item in _navigationGroupService.GetNavigationGroups())
            {
                if (item.ID_parent_group == groupId)
                {
                    if (_navigationGroupService.GetNavigationGroups().Any(g => g.ID_parent_group == item.Id))
                        GetGroupSubgroups(item.Id, list);
                    else
                        list.Add(item.Id);
                }
            }

            return list;
        }
    }
}
