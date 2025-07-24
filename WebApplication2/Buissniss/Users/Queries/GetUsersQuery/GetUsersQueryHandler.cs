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
                DisplayName = u.First_Name != null && u.Last_Name != null ? u.First_Name + " " + u.Last_Name : null,
                FirstName = u.First_Name,
                LastName = u.Last_Name,
                Email = u.Email,
                IsLocked = u.Is_Locked,
                IsSystem = u.Is_System,
                Domain = u.Domain != null ? u.Domain : null
            }).ToList();

            return result;
        }
    }
}
