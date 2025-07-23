using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using WebApplication2.Services;
using WebApplication2.Models;

namespace WebApplication2.Business.Roles.Commands.AddNavigationGroupPermissionCommand
{
    public class AddNavigationGroupPermissionCommandHandler : IRequestHandler<AddNavigationGroupPermissionCommand, RecordIDResponse>
    {
        private readonly NavigationGroupPermissionsService navigationGroupPermissionsService;

        public AddNavigationGroupPermissionCommandHandler(NavigationGroupPermissionsService navigationGroupPermissionsService)
        {
            this.navigationGroupPermissionsService = navigationGroupPermissionsService;
        }

        public async Task<RecordIDResponse> Handle(AddNavigationGroupPermissionCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            if (request.Data.ExtraParamsFormValues.RoleId != null && request.Data.ExtraParamsFormValues.NavigationGroupId != null) {
                var navigationGroupPermissionClass = new NavigationGroupPermissionsClass()
                {
                    ID_role = (int)request.Data.ExtraParamsFormValues.RoleId,
                    ID_navigation_group = (int)request.Data.ExtraParamsFormValues.NavigationGroupId,
                };
                var permissionId = navigationGroupPermissionsService.AddNavigationGroupPermission(navigationGroupPermissionClass);

                if (permissionId == -1)
                {
                    response.AddMessage(new BaseException(UserApplicationMessages.MAPPING_EXISTS));
                    return response;
                }

                response.SetId(permissionId);
            }
            return response;
        }
    }
}
