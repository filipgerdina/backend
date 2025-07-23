using MediatR;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery
{
    public class GetRolesOfUserQueryHandler : IRequestHandler<GetRolesOfUserQuery, CoreListResponse<GetRolesOfUserQueryDTO>>
    {
        private readonly UserService _usersService;
        private readonly RoleService _roleService;
        private readonly PagesService _pagesService;

        public GetRolesOfUserQueryHandler(UserService usersService, RoleService roleService, PagesService pagesService)
        {
            _usersService = usersService;
            _roleService = roleService;
            _pagesService = pagesService;
        }

        public async Task<CoreListResponse<GetRolesOfUserQueryDTO>> Handle(GetRolesOfUserQuery request, CancellationToken cancellationT)
        {

            var result = new CoreListResponse<GetRolesOfUserQueryDTO>();

            var user = _usersService.GetUsers().ToList().Find(user => user.Id == request.Id);

            if (user == null)
                return result;

            result.Data = _usersService.GetActiveUserRoles(user.Id).ToList().ToList().Select(r => new GetRolesOfUserQueryDTO
            {
                Id = _usersService.GetUserRoleMapping(user.Id, r.Id).Id,
                RoleId = r.Id,
                Name = r.Name,
                DefaultPage = _pagesService.GetPages().ToList().FindAll(p => p.Id == r.ID_home_page).Count > 0 ? _pagesService.GetPages().ToList().FindAll(p => p.Id == r.ID_home_page).Select(p => p.Name).First() : null,
            }).ToList();

            //if (user != null && userRoles != null)
            //{
            //    result.Data = userRoles.Select(u => new GetRolesOfUserQueryDTO()
            //    {
            //        Id = u.Id,
            //        Id = u.Id,
            //        Name = u.Name,
            //        DefaultPage = _navigationGroupService.GetPages().ToList().FindAll(p => p.Id == _usersService.GetUserDefaultPage(user.Id).Id).Select(p => p.Name).First() ?? null,
            //    }).ToList();
            //}

            return result;
        }
    }
}
