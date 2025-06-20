using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetPagePermissionsOfRoleQuery
{
    public class GetPagePermissionsOfRoleQuery : QueryDataList, IRequest<CoreListResponse<GetPagePermissionsOfRoleQueryDTO>>
    {
        public int RoleId { get; set; }
    }
}
