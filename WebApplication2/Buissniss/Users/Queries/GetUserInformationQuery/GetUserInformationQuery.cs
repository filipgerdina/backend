using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.User.Queries.GetUserInformationQuery
{
    public class GetUserInformationQuery : QueryDataList, IRequest<CoreResponse<GetUserInformationQueryDTO>>
    {
    }
}
