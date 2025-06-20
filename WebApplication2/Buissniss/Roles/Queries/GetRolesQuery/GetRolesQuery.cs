using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetRolesQuery
{
    public class GetRolesQuery : QueryDataList, IRequest<CoreListResponse<GetRolesQueryDTO>>
    {

    }
}
