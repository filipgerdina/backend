using MediatR;
using WebApplication2.Models.Responses;
using WebApplication2.Models.CodeList;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Module.GetUtlActionsQuery
{
    public class GetRolesActionsQueryHandler : IRequestHandler<GetUtlActionsQuery, CoreListResponse<ActionsQueryDTO>>
    {


        public GetRolesActionsQueryHandler()
        {

        }

        public async Task<CoreListResponse<ActionsQueryDTO>> Handle(GetUtlActionsQuery request, CancellationToken cancellationToken)
        {
            var actions = new List<ActionsQueryDTO>() { };

            if (string.IsNullOrWhiteSpace(request.RecordTypeCode))
            {
                return new CoreListResponse<ActionsQueryDTO>()
                {
                    Data = actions,
                };
            }

            var filteredActions = actions.FindAll(action => action.RecordTypeCode == request.RecordTypeCode);
            return new CoreListResponse<ActionsQueryDTO>()
            {
                Data = filteredActions,
            };
        }
    }
}
