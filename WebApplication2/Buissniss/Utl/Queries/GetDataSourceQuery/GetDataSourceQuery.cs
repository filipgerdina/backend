using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetDataSourceQuery
{
    public class GetDataSourceQuery : QueryDataList, IRequest<CoreListResponse<GetDataSourceQueryDTO>>
    {

    }
}
