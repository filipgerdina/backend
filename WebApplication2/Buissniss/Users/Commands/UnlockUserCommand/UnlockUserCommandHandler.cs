using MediatR;
using Microsoft.AspNetCore.SignalR;
using WebApplication2.Hubs;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.UnlockUserCommand
{
    public class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand, RecordIDResponse>
    {
        private readonly UserService userService;

        public UnlockUserCommandHandler(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<RecordIDResponse> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            // Generate response message
            var response = new RecordIDResponse();

            response.SetId(userService.UnlockUser(request.Data.GetId()));
            return response;
        }
    }
}
