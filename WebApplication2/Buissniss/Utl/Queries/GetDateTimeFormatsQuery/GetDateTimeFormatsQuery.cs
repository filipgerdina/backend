using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetDateTimeFormatsQuery
{
    public class GetDateTimeFormatsQuery : QueryDataList, IRequest<CoreListResponse<GetDateTimeFormatsQueryDTO>>
    {

    }
}
