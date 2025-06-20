using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Roles.Queries.GetPagePermissionsOfRoleQuery
{
    public class GetPagePermissionsOfRoleQueryHandler : IRequestHandler<GetPagePermissionsOfRoleQuery, CoreListResponse<GetPagePermissionsOfRoleQueryDTO>>
    {
        private readonly PagePermissionsService _pagePermissionsService;
        private readonly PagesService _pagesService;

        public GetPagePermissionsOfRoleQueryHandler(PagePermissionsService pagePermissionsService, PagesService pagesService)
        {
            _pagePermissionsService = pagePermissionsService;
            _pagesService = pagesService;
        }

        public async Task<CoreListResponse<GetPagePermissionsOfRoleQueryDTO>> Handle(GetPagePermissionsOfRoleQuery request, CancellationToken cancellationT) {

            var result = new CoreListResponse<GetPagePermissionsOfRoleQueryDTO>();

            result.Data = _pagePermissionsService.GetRolesPagePermissions(request.RoleId).Select(pp => new GetPagePermissionsOfRoleQueryDTO()
            {
                Id = (int)pp.Id,
                Name = _pagesService.GetPages().ToList().Find(p => p.Id == pp.PageId).Name,
            }).ToList();

            return result;
        }
    }
}
