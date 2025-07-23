using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Roles.Queries.GetRolesQuery
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, CoreListResponse<GetRolesQueryDTO>>
    {
        private readonly RoleService _roleSevice;
        private readonly PagesService _pagesService;

        public GetRolesQueryHandler(RoleService roleService, PagesService pagesService)
        {
            _roleSevice = roleService;
            _pagesService = pagesService;
        }

        public async Task<CoreListResponse<GetRolesQueryDTO>> Handle(GetRolesQuery request, CancellationToken cancellationT) {

            var result = new CoreListResponse<GetRolesQueryDTO>();

            result.Data = _roleSevice.GetRoles().Select(u => new GetRolesQueryDTO()
            { 
                Id = u.Id,
                Name = u.Name,
                DefaultPage = _pagesService.GetPages().ToList().FindAll(p => p.Id == u.ID_home_page).Count != 0 ? _pagesService.GetPages().ToList().FindAll(p => p.Id == u.ID_home_page).Select(p => p.Name).First() : null,
            }).ToList();

            return result;
        }
    }
}
