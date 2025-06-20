using MediatR;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Users.Module.GetUserActionsQuery
{
    public class GetUserActionsQuery : ActionsQuery, IRequest<CoreListResponse<ActionsQueryDTO>>
    {
    }
}
