using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using System.Security.Cryptography;
using WebApplication2.Services;

namespace WebApplication2.Business.User.Commands.RemoveRoleFromUserCommand
{
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, RecordIDResponse>
    {
        private readonly UserService userService;
        private readonly RoleService roleService;

        public RemoveRoleFromUserCommandHandler(UserService userService, RoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public async Task<RecordIDResponse> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            var userId = userService.RemoveRoleFromUser(request.Data.GetId());

            if (userId == -1)
                throw new BaseException(UserApplicationMessages.USER_ALREADY_HAS_THIS_ROLE);

            response.SetId(userId);
            return response;
        }
    }
}
