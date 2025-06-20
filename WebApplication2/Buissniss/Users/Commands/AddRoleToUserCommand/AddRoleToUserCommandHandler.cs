using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using System.Security.Cryptography;
using WebApplication2.Services;

namespace WebApplication2.Business.User.Commands.AddRoleToUserCommand
{
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, RecordIDResponse>
    {
        private readonly UserService userService;
        private readonly RoleService roleService;

        public AddRoleToUserCommandHandler(UserService userService, RoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public async Task<RecordIDResponse> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            var user = new UserClassEdit
            {
                Id = request.Data.ExtraParamsFormValues.UserId,
                RoleId = request.Data.ExtraParamsFormValues.RoleId,
            };

            var userId = userService.AddRoleToUser(user);

            if (userId == -1)
                throw new BaseException(UserApplicationMessages.USER_ALREADY_HAS_THIS_ROLE);

            response.SetId(userId);
            return response;
        }
    }
}
