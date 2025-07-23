using MediatR;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Module.GetUtlActionsQuery
{
    public class GetUtlActionsQuery : ActionsQuery, IRequest<CoreListResponse<ActionsQueryDTO>>
    {
    }
}
