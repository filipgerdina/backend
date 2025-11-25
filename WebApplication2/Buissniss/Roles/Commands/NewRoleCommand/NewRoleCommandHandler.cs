using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using System.Security.Cryptography;
using WebApplication2.Services;

namespace WebApplication2.Business.Roles.Commands.AddRoleCommand
{
    public class NewRoleCommandHandler : IRequestHandler<NewRoleCommand, RecordIDResponse>
    {
        private readonly UserService userService;
        private readonly RoleService roleService;

        public NewRoleCommandHandler(UserService userService, RoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public async Task<RecordIDResponse> Handle(NewRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            RoleClassEdit roleClass = new RoleClassEdit()
            {
                Name = request.Data.Name,
                DefaultPageId = request.Data.DefaultPageId,
            };
            var roleId = roleService.AddRole(roleClass);

            if (roleId == -1)
            {
                response.AddMessage(new BaseException(UserApplicationMessages.ROLE_NAME_NOT_UNIQUE));
                return response;
            }

            response.SetId(roleId);
            return response;
        }
    }
}
