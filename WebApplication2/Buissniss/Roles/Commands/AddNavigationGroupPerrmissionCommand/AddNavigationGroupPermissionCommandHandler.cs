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

            var navigationGroupPermissionClass = new NavigationGroupPermissionsClass()
            {
                RoleId = request.Data.ExtraParamsFormValues.RoleId,
                NavigationGroupId = request.Data.ExtraParamsFormValues.NavigationGroupId,
            };
            var permissionId = navigationGroupPermissionsService.AddNavigationGroupPermission(navigationGroupPermissionClass);

            if (permissionId == -1)
                throw new BaseException(UserApplicationMessages.MAPPING_EXISTS);

            response.SetId(permissionId);
            return response;
        }
    }
}
