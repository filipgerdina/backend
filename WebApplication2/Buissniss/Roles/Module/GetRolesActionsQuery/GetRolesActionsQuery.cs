using MediatR;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Module.GetRolesActionsQuery
{
    public class GetRolesActionsQuery : ActionsQuery, IRequest<CoreListResponse<ActionsQueryDTO>>
    {
    }
}
