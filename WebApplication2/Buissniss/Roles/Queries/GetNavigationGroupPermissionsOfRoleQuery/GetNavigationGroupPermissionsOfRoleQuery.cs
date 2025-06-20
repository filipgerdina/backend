using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupPermissionsOfRoleQuery
{
    public class GetNavigationGroupPermissionsOfRoleQuery : QueryDataList, IRequest<CoreListResponse<GetNavigationGroupPermissionsOfRoleQueryDTO>>
    {
        public int RoleId { get; set; }
    }
}
