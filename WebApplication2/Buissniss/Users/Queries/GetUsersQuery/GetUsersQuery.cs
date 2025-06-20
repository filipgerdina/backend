using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.User.Queries.GetUsersQuery
{
    public class GetUsersQuery: QueryDataList, IRequest<CoreListResponse<GetUsersQueryDTO>>
    {

    }
}
