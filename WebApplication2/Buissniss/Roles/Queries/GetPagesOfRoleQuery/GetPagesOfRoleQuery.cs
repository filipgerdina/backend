using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetPagesOfRoleQuery
{
    public class GetPagesOfRoleQuery : QueryDataList, IRequest<CoreListResponse<GetPagesOfRoleQueryDTO>>
    {
        public int RoleId { get; set; }
    }
}
