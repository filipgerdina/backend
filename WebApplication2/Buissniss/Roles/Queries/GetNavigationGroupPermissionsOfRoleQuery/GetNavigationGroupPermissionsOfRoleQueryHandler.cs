using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupPermissionsOfRoleQuery
{
    public class GetNavigationGroupPermissionsOfRoleQueryHandler : IRequestHandler<GetNavigationGroupPermissionsOfRoleQuery, CoreListResponse<GetNavigationGroupPermissionsOfRoleQueryDTO>>
    {
        private readonly NavigationGroupPermissionsService _navigationGroupPermissionsService;
        private readonly NavigationGroupService _navigationGroupService;

        public GetNavigationGroupPermissionsOfRoleQueryHandler(NavigationGroupPermissionsService navigationGroupPermissionsService, NavigationGroupService navigationGroupService)
        {
            _navigationGroupPermissionsService = navigationGroupPermissionsService;
            _navigationGroupService = navigationGroupService;
        }

        public async Task<CoreListResponse<GetNavigationGroupPermissionsOfRoleQueryDTO>> Handle(GetNavigationGroupPermissionsOfRoleQuery request, CancellationToken cancellationT) {

            var result = new CoreListResponse<GetNavigationGroupPermissionsOfRoleQueryDTO>();

            result.Data = _navigationGroupPermissionsService.GetRolesNavigationGroupPermissions(request.RoleId).Select(pp => new GetNavigationGroupPermissionsOfRoleQueryDTO()
            {
                Id = (int)pp.Id,
                Name = _navigationGroupService.GetNavigationGroups().ToList().Find(p => p.Id == pp.ID_navigation_group).Name,
            }).ToList();

            return result;
        }
    }
}
