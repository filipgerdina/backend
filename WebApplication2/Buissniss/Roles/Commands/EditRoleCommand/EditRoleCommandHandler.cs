using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using System.Security.Cryptography;
using WebApplication2.Services;

namespace WebApplication2.Business.Roles.Commands.EditRoleCommand
{
    public class NewRoleCommandHandler : IRequestHandler<EditRoleCommand, RecordIDResponse>
    {
        private readonly UserService userService;
        private readonly RoleService roleService;

        public NewRoleCommandHandler(UserService userService, RoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public async Task<RecordIDResponse> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            RoleClassEdit roleClass = new RoleClassEdit()
            {
                Id = request.Data.ExtraParamsFormValues.Id,
                DefaultPageId = request.Data.ExtraParamsFormValues.DefaultPageId,
            };
            var roleId = roleService.EditRole(roleClass);

            if (roleId == -1)
                throw new BaseException(UserApplicationMessages.ROLE_NAME_NOT_UNIQUE);

            response.SetId(roleId);
            return response;
        }
    }
}
