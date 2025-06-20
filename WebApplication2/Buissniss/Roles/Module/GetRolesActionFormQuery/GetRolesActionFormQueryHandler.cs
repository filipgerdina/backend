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

namespace WebApplication2.Roles.Module.GetRolesActionsFormQuery
{
    public class GetRolesActionFormQueryHandler : IRequestHandler<GetRolesActionFormQuery, CoreResponse<ActionFormQueryDTO>>
    {

        private readonly UserService userService;
        private readonly RoleService roleService;
        private readonly PagesService pagesService;
        private readonly PagePermissionsService pagePermissionsService;

        public GetRolesActionFormQueryHandler(UserService userService, RoleService roleService, PagesService pagesService, PagePermissionsService pagePermissionsService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.pagesService = pagesService;
            this.pagePermissionsService = pagePermissionsService;
        }

        public async Task<CoreResponse<ActionFormQueryDTO>> Handle(GetRolesActionFormQuery request, CancellationToken cancellationToken)
        {
            var result = new CoreResponse<ActionFormQueryDTO>();
            var obj = new ActionFormQueryDTO();

            if (request.Data.FormCode == UserTableActions.EDIT_ROLE)
            {
                obj = await EditRoleCommandForm.GetEditRoleForm(request.Data, roleService, pagesService, pagePermissionsService, cancellationToken);
            }

            result.Data = obj;
            return result;
        }
    }
}
