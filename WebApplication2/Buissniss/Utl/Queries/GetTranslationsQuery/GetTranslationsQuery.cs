using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetTranslationsQuery
{
    public class GetTranslationsQuery: QueryDataList, IRequest<CoreListResponse<GetTranslationsQueryDTO>>
    {
        public int? langId { get; set; }
    }
}
