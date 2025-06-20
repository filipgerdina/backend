using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Roles.Queries.GetPagesOfRoleQuery
{
    public class GetPagesOfRoleQueryHandler : IRequestHandler<GetPagesOfRoleQuery, CoreListResponse<GetPagesOfRoleQueryDTO>>
    {
        private readonly RoleService _roleSevice;
        private readonly PagesService _pagesService;
        private readonly PagePermissionsService _pagePermissionsService;

        public GetPagesOfRoleQueryHandler(RoleService roleService, PagesService pagesService, PagePermissionsService pagePermissionsService)
        {
            _roleSevice = roleService;
            _pagesService = pagesService;
            _pagePermissionsService = pagePermissionsService;
        }

        public async Task<CoreListResponse<GetPagesOfRoleQueryDTO>> Handle(GetPagesOfRoleQuery request, CancellationToken cancellationT) {

            var result = new CoreListResponse<GetPagesOfRoleQueryDTO>();

            var pageIds = _pagePermissionsService.GetRolesPagePermissions(request.RoleId).Select(pp => pp.PageId);
            result.Data = _pagesService.GetPages().ToList().FindAll(p => !pageIds.Contains(p.Id)).Select(u => new GetPagesOfRoleQueryDTO()
            { 
                Id = u.Id,
                Name = u.Name,
            }).ToList();

            return result;
        }
    }
}
