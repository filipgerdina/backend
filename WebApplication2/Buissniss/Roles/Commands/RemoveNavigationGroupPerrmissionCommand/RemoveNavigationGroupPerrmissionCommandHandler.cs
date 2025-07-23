using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using WebApplication2.Services;
using WebApplication2.Models;

namespace WebApplication2.Business.Roles.Commands.RemoveNavigationGroupPerrmissionCommand
{
    public class RemoveNavigationGroupPerrmissionCommandHandler : IRequestHandler<RemoveNavigationGroupPerrmissionCommand, RecordIDResponse>
    {
        private readonly NavigationGroupPermissionsService navigationGroupPermissionsService;

        public RemoveNavigationGroupPerrmissionCommandHandler(NavigationGroupPermissionsService navigationGroupPermissionsService)
        {
            this.navigationGroupPermissionsService = navigationGroupPermissionsService;
        }

        public async Task<RecordIDResponse> Handle(RemoveNavigationGroupPerrmissionCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            var permissionId = navigationGroupPermissionsService.RemoveNavigationGroupPermission(request.Data.GetId());

            if (permissionId == -1)
            {
                response.AddMessage(new BaseException(UserApplicationMessages.MAPPING_EXISTS));
                return response;
            }

            response.SetId(permissionId);
            return response;
        }
    }
}
