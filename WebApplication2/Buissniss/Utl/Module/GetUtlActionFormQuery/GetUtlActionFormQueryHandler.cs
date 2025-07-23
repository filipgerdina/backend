using MediatR;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses;
using System.Threading;
using WebApplication2.Models.CodeList;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.User.Commands.AddRoleToUserCommand;
using WebApplication2.Services;
using WebApplication2.Business.User.Commands.SyncDomainUsersCommand;
using WebApplication2.Business.Roles.Commands.AddRoleCommand;
using WebApplication2.Business.Roles.Commands.EditRoleCommand;
using WebApplication2.Business.Utl.Commands.EditApplicationSettingsCommand;

namespace WebApplication2.Utl.Module.GetUtlActionFormQuery
{
    public class GetUtlActionFormQueryHandler : IRequestHandler<GetUtlActionFormQuery, CoreResponse<ActionFormQueryDTO>>
    {

        private readonly ApplicationSettingsService appSettingsService;

        public GetUtlActionFormQueryHandler(ApplicationSettingsService appSettingsService)
        {
            this.appSettingsService = appSettingsService;
        }

        public async Task<CoreResponse<ActionFormQueryDTO>> Handle(GetUtlActionFormQuery request, CancellationToken cancellationToken)
        {
            var result = new CoreResponse<ActionFormQueryDTO>();
            var obj = new ActionFormQueryDTO();

            if (request.Data.FormCode == UtlTableActions.EDIT_APPLICATION_SETTINGS)
            {
                obj = await EditApplicationSettingsCommandForm.GetEditApplicationSettingsForm(appSettingsService, cancellationToken);
            }

            result.Data = obj;
            return result;
        }
    }
}
