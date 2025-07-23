using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using System.Security.Cryptography;

namespace WebApplication2.Business.User.Commands.NewUserCommand
{
    public class NewUserCommandHandler : IRequestHandler<NewUserCommand, RecordIDResponse>
    {
        private readonly UserService userService;

        public NewUserCommandHandler(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<RecordIDResponse> Handle(NewUserCommand request, CancellationToken cancellationToken)
        {
            // Generate response message
            var response = new RecordIDResponse();


            var user = new UserClassEdit
            {
                Username = request.Data?.ExtraParamsFormValues.Username,
                Password = request.Data.ExtraParamsFormValues.Password
            };

            var userId = userService.AddUser(user);

            if(userId == -1)
            {
                response.AddMessage(new BaseException(UserApplicationMessages.USERNAME_NOT_UNIQUE));
                return response;
            }

            response.SetId(userId);
            return response;
        }
    }
}
