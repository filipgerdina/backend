using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.ChangePasswordCommand
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, RecordIDResponse>
    {
        private readonly UserService userService;

        public ChangePasswordCommandHandler(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<RecordIDResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // Generate response message
            var response = new RecordIDResponse();

            var user = userService.GetUserById(request.Data.ExtraParamsFormValues.Id);

            if (userService.Authenticate(user.Username, request.Data.ExtraParamsFormValues.OldPassword) == null) {
                response.AddMessage(new BaseException(UserApplicationMessages.INCORRECT_PASSWORD));
                return response;
            }

            if (!request.Data.ExtraParamsFormValues.NewPassword.Equals(request.Data.ExtraParamsFormValues.NewPasswordConfirmed)) {
                response.AddMessage(new BaseException(UserApplicationMessages.PASSWORDS_ARE_NOT_SAME));
                return response;
            }

            try
            {
                var userId = userService.ChangePassword(user.Id, request.Data.ExtraParamsFormValues.NewPassword);
                if (userId == -1)
                    throw new BaseException(UserApplicationMessages.INCORRECT_PASSWORD);


                response.SetId(userId);
                return response;
            }
            catch (BaseException exp) {
                response.AddMessage(exp);
                return response;
            }
        }
    }
}
