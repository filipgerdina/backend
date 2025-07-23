using MediatR;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetModulesQuery
{
    public class GetModulesQueryHandler : IRequestHandler<GetModulesQuery, CoreListResponse<GetModulesQueryDTO>>
    {
        private readonly ModuleService _moduleService;

        public GetModulesQueryHandler(ModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        public async Task<CoreListResponse<GetModulesQueryDTO>> Handle(GetModulesQuery request, CancellationToken cancellationT)
        {

            var result = new CoreListResponse<GetModulesQueryDTO>();

            result.Data = _moduleService.GetModules().Select(u => new GetModulesQueryDTO()
            {
                Id = u.Id,
                ModuleName = u.Name,
                PathToModule = u.Module_Path
            }).ToList();
            return result;
        }
    }
}
