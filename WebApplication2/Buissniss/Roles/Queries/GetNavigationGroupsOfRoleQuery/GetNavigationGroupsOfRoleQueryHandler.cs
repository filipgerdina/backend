using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupsOfRoleQuery
{
    public class GetNavigationGroupsOfRoleQueryHandler : IRequestHandler<GetNavigationGroupsOfRoleQuery, CoreListResponse<GetNavigationGroupsOfRoleQueryDTO>>
    {
        private readonly RoleService _roleSevice;
        private readonly NavigationGroupService _navigationGroupService;
        private readonly NavigationGroupPermissionsService _navigationGroupPermissionsService;

        public GetNavigationGroupsOfRoleQueryHandler(RoleService roleService, NavigationGroupService navigationGroupService, NavigationGroupPermissionsService navigationGroupPermissionsService)
        {
            _roleSevice = roleService;
            _navigationGroupService = navigationGroupService;
            _navigationGroupPermissionsService = navigationGroupPermissionsService;
        }

        public async Task<CoreListResponse<GetNavigationGroupsOfRoleQueryDTO>> Handle(GetNavigationGroupsOfRoleQuery request, CancellationToken cancellationT) {

            var result = new CoreListResponse<GetNavigationGroupsOfRoleQueryDTO>();

            var navigationGroupIds = _navigationGroupPermissionsService.GetRolesNavigationGroupPermissions(request.RoleId).Select(pp => pp.NavigationGroupId);
            result.Data = _navigationGroupService.GetNavigationGroups().ToList().FindAll(p => !navigationGroupIds.Contains(p.Id)).Select(u => new GetNavigationGroupsOfRoleQueryDTO()
            { 
                Id = u.Id,
                Name = u.Name,
            }).ToList();

            return result;
        }
    }
}
