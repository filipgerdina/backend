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
        private readonly ModuleService _moduleService;
        private readonly PagePermissionsService _pagePermissionsService;
        private readonly NavigationGroupService _navigationGroupService;
        private readonly NavigationGroupPermissionsService _navigationGroupPermissionsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPagesQueryHandler(UserService userService, PagesService pagesService, ModuleService moduleService, PagePermissionsService pagePermissionsService, NavigationGroupService navigationGroupService, NavigationGroupPermissionsService navigationGroupPermissionsService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _pagesService = pagesService;
            _moduleService = moduleService;
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
            if (userEntity.Is_System)
            {
                result.Data = allPages.FindAll(up => up.ID_module == null || _moduleService.GetModules().ToList().Select(m => m.Id).Contains((int)up.ID_module)).Select(p => new GetPagesQueryDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    PageComponent = p.Component_Name,
                    Path = p.Path,
                    IconUrl = p.Icon,
                    ModuleId = p.ID_module,
                    NavigationGroupId = p.ID_group
                }).ToList();
                return result;
            }

            // Step 1: Get permitted page IDs
            var permittedPageIds = _pagePermissionsService.GetPagePermissions()
                .Where(p => p.ID_role != null && userRoleIds.Contains((int)p.ID_role))
                .Select(p => p.ID_page)
                .Distinct()
                .ToHashSet();

            // Step 2: Get directly permitted nav group IDs
            var directGroupIds = _navigationGroupPermissionsService.GetNavigationGroupPermissions()
                .Where(p => p.ID_role != null && userRoleIds.Contains((int)p.ID_role))
                .Select(p => p.ID_navigation_group)
                .Distinct()
                .ToHashSet();

            // Step 3: Recursively collect parent group IDs
            HashSet<int> GetAllParentGroups(IEnumerable<int> groupIds)
            {
                var allowed = new HashSet<int>(groupIds);
                var parentMap = allNavGroups.ToDictionary(g => g.Id, g => g.ID_parent_group);

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

            var allowedGroupIds = new HashSet<int>();

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
                    (p.ID_group != null && p.ID_group != null && allowedGroupIds.Contains((int)p.ID_group))
                )
                .ToList();

            // Step 5: Map to DTO
            result.Data = userPages.FindAll(up => up.ID_module == null || _moduleService.GetModules().ToList().Select(m => m.Id).Contains((int)up.ID_module)).Select(p => new GetPagesQueryDTO
            {
                Id = p.Id,
                Name = p.Name,
                PageComponent = p.Component_Name,
                Path = p.Path,
                IconUrl = p.Icon,
                ModuleId = p.ID_module,
                NavigationGroupId = p.ID_group
            }).ToList();

            return result;
        }
        private List<int> GetGroupSubgroups(int groupId, List<int> list)
        {
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
