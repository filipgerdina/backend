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

            PagePermissionClass pagePermissionClass = new PagePermissionClass()
            {
                RoleId = request.Data.ExtraParamsFormValues.RoleId,
                PageId = request.Data.ExtraParamsFormValues.PageId,
            };
            var permissionId = pagePermissionsService.AddPagePermission(pagePermissionClass);

            if (permissionId == -1)
                throw new BaseException(UserApplicationMessages.MAPPING_EXISTS);

            response.SetId(permissionId);
            return response;
        }
    }
}
