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
using WebApplication2.Business.User.Commands.EditProfileCommand;
using WebApplication2.Business.User.Commands.ChangePasswordCommand;

namespace WebApplication2.Buissniss.Users.Module.GetUserActionFormQuery
{
    public class GetUserActionFormQueryHandler : IRequestHandler<GetUserActionFormQuery, CoreResponse<ActionFormQueryDTO>>
    {

        private readonly UserService userService;
        private readonly RoleService roleService;
        private readonly PagesService pagesService;
        private readonly ApplicationSettingsService appService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GetUserActionFormQueryHandler(UserService userService, RoleService roleService, PagesService pagesService, ApplicationSettingsService appService, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.pagesService = pagesService;
            this.appService = appService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CoreResponse<ActionFormQueryDTO>> Handle(GetUserActionFormQuery request, CancellationToken cancellationToken)
        {
            var result = new CoreResponse<ActionFormQueryDTO>();
            var obj = new ActionFormQueryDTO();

            if (request.Data.FormCode == UserTableActions.NEW_USER)
            {
                obj = await NewUserCommandForm.GetNewUserForm(userService, roleService, cancellationToken);
            }

            if (request.Data.FormCode == UserTableActions.EDIT_USER)
            {
                obj = await EditUserCommandForm.GetEditUserForm(request.Data, userService, cancellationToken);
            }

            if (request.Data.FormCode == UserTableActions.ADD_ROLE_TO_USER)
            {
                obj = await AddRoleToUserCommandForm.GetAddRoleToUserForm(request.Data, userService, roleService, cancellationToken);
            }         
            
            if (request.Data.FormCode == UserTableActions.SYNC_DOMAIN_USERS)
            {
                obj = await SyncDomainUsersCommandForm.GetSyncDomainUsersForm(userService, roleService, cancellationToken);
            }

            if (request.Data.FormCode == UserTableActions.EDIT_PROFILE)
            {
                obj = await EditProfileCommandForm.GetEditProfileCommandForm(userService, roleService, pagesService, appService, httpContextAccessor, cancellationToken);
            }

            if (request.Data.FormCode == UserTableActions.CHANGE_PASSWORD)
            {
                obj = await ChangePasswordCommandForm.GetChangePasswordForm(request.Data, userService, appService, httpContextAccessor, cancellationToken);
            }

            result.Data = obj;
            return result;
        }
    }
}
