using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.EditUserCommand
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, RecordIDResponse>
    {
        private readonly UserService userService;

        public EditUserCommandHandler(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<RecordIDResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            // Generate response message
            var response = new RecordIDResponse();


            var user = new UserClassEdit
            {
                Id = request.Data.ExtraParamsFormValues.Id,
                Username = request.Data?.ExtraParamsFormValues.Username,
            };

            var userId = userService.EditUser(user);

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
