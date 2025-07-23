using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetModulesQuery
{
    public class GetModulesQuery : QueryDataList, IRequest<CoreListResponse<GetModulesQueryDTO>>
    {
    }
}
