using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using WebApplication2.Services;
using WebApplication2.Models;

namespace WebApplication2.Business.Roles.Commands.AddPagePermissionCommand
{
    public class AddPagePermissionCommandHandler : IRequestHandler<AddPagePermissionCommand, RecordIDResponse>
    {
        private readonly PagePermissionsService pagePermissionsService;

        public AddPagePermissionCommandHandler(PagePermissionsService pagePermissionsService)
        {
            this.pagePermissionsService = pagePermissionsService;
        }

        public async Task<RecordIDResponse> Handle(AddPagePermissionCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            if (request.Data.ExtraParamsFormValues.RoleId != null && request.Data.ExtraParamsFormValues.PageId != null) {
                PagePermissionClass pagePermissionClass = new PagePermissionClass()
                {
                    ID_role = (int)request.Data.ExtraParamsFormValues.RoleId,
                    ID_page = (int)request.Data.ExtraParamsFormValues.PageId,
                };
                var permissionId = pagePermissionsService.AddPagePermission(pagePermissionClass);

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
