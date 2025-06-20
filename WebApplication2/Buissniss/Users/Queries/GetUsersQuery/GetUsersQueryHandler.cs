using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.User.Queries.GetUsersQuery
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, CoreListResponse<GetUsersQueryDTO>>
    {
        private readonly UserService _usersService;

        public GetUsersQueryHandler(UserService usersService)
        {
            _usersService = usersService;
        }

        public async Task<CoreListResponse<GetUsersQueryDTO>> Handle(GetUsersQuery request, CancellationToken cancellationT) {

            var result = new CoreListResponse<GetUsersQueryDTO>();

            result.Data = _usersService.GetUsers().Select(u => new GetUsersQueryDTO()
            { 
                Id = u.Id,
                Username = u.Username,
                Added = u.Added,
                DisplayName = u.DisplayName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                IsLocked = u.IsLocked,
                IsSystem = u.IsSystem,
                Domain = (u is DomainUserClass) ? (u as DomainUserClass).Domain : null
            }).ToList();

            return result;
        }
    }
}
