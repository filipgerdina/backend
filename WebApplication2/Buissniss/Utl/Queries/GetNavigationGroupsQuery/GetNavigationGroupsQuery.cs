using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetNavigationGroupsQuery
{
    public class GetNavigationGroupsQuery : QueryDataList, IRequest<CoreListResponse<GetNavigationGroupsQueryDTO>>
    {

    }
}
