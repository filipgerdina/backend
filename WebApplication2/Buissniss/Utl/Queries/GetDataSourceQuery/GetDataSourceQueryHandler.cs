using MediatR;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetDataSourceQuery
{
    public class GetDataSourceQueryHandler : IRequestHandler<GetDataSourceQuery, CoreListResponse<GetDataSourceQueryDTO>>
    {
        private readonly DataSourceService _dataSourceService;

        public GetDataSourceQueryHandler(DataSourceService dataSourceService)
        {
            _dataSourceService = dataSourceService;
        }

        public async Task<CoreListResponse<GetDataSourceQueryDTO>> Handle(GetDataSourceQuery request, CancellationToken cancellationT)
        {

            var result = new CoreListResponse<GetDataSourceQueryDTO>();

            result.Data = _dataSourceService.GetDataSources().Select(u => new GetDataSourceQueryDTO()
            {
                Id = u.Id,
                Name = u.Name,
            }).ToList();
            return result;
        }
    }
}
