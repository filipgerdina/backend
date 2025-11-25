using MediatR;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetRolesGroupQuery
{
    public class GetRolesGroupQuery : IRequest<CoreListResponse<GroupResponse>>
    {
        public object[]? filter { get; set; }
        public List<GroupItem>? group { get; set; }
        public List<DxSortItem>? sort { get; set; }   // row sort + optional group sort (see handler)
        public int? skip { get; set; }                // group-level paging
        public int? take { get; set; }
    }
}
