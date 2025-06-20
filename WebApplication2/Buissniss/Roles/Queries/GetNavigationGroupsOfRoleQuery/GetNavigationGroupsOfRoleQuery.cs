using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupsOfRoleQuery
{
    public class GetNavigationGroupsOfRoleQuery : QueryDataList, IRequest<CoreListResponse<GetNavigationGroupsOfRoleQueryDTO>>
    {
        public int RoleId { get; set; }
    }
}
