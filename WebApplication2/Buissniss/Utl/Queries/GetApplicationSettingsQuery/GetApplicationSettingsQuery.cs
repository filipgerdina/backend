using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetApplicationSettingsQuery
{
    public class GetApplicationSettingsQuery : QueryDataList, IRequest<CoreResponse<GetApplicationSettingsQueryDTO>>
    {
    }
}
