using MediatR;
using Microsoft.AspNetCore.SignalR;
using WebApplication2.Hubs;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.LockUserCommand
{
    public class LockUserSignalRClass 
    { 
        public string Username { get; set; }
    }

    public class LockUserCommandHandler : IRequestHandler<LockUserCommand, RecordIDResponse>
    {
        private readonly UserService userService;
        private readonly IHubContext<PagesHub> hubContext;

        public LockUserCommandHandler(UserService userService, IHubContext<PagesHub> hubContext)
        {
            this.userService = userService;
            this.hubContext = hubContext;
        }

        public async Task<RecordIDResponse> Handle(LockUserCommand request, CancellationToken cancellationToken)
        {
            // Generate response message
            var response = new RecordIDResponse();

            response.SetId(userService.LockUser(request.Data.GetId()));
            await hubContext.Clients.All.SendAsync("LockUser", new LockUserSignalRClass() { Username = userService.GetUsers().ToList().FindAll(u => u.Id == request.Data.GetId()).Select(u => u.Username).First() });
            return response;
        }
    }
}
