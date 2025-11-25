using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetRolesQuery
{
    public class GetRolesQuery : QueryDataList, IRequest<CoreListResponse<GetRolesQueryDTO>>
    {
        public int? skip { get; set; }
        public int? take { get; set; }
        public bool requireTotalCount { get; set; }
        public List<DxSortItem>? sort { get; set; }
        public object[]? filter { get; set; }
    }
}
