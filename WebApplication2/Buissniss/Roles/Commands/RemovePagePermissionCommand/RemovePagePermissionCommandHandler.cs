using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using WebApplication2.Services;
using WebApplication2.Models;

namespace WebApplication2.Business.Roles.Commands.RemovePagePerrmissionCommand
{
    public class RemovePagePerrmissionCommandHandler : IRequestHandler<RemovePagePerrmissionCommand, RecordIDResponse>
    {
        private readonly PagePermissionsService pagePermissionsService;

        public RemovePagePerrmissionCommandHandler(PagePermissionsService pagePermissionsService)
        {
            this.pagePermissionsService = pagePermissionsService;
        }

        public async Task<RecordIDResponse> Handle(RemovePagePerrmissionCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            var permissionId = pagePermissionsService.RemovePagePermission(request.Data.GetId());

            if (permissionId == -1)
                throw new BaseException(UserApplicationMessages.MAPPING_EXISTS);

            response.SetId(permissionId);
            return response;
        }
    }
}
