using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetDecimalSeperatorsQuery
{
    public class GetDecimalSeperatorsQuery : QueryDataList, IRequest<CoreListResponse<GetDecimalSeperatorsQueryDTO>>
    {

    }
}
