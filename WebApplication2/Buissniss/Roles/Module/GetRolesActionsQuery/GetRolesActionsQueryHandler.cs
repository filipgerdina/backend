using MediatR;
using WebApplication2.Models.Responses;
using WebApplication2.Models.CodeList;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Roles.Module.GetRolesActionsQuery
{
    public class GetRolesActionsQueryHandler : IRequestHandler<GetRolesActionsQuery, CoreListResponse<ActionsQueryDTO>>
    {

        private readonly UserService userService;
        private readonly RoleService roleService;
        private readonly PagesService pagesService;
        private readonly PagePermissionsService pagePermissionsService;
        private readonly DataSourceService dataSourceService;
        private readonly NavigationGroupPermissionsService navigationGroupPermissionsService;
        private readonly NavigationGroupService navigationGroupService;

        public GetRolesActionsQueryHandler(UserService userService, RoleService roleService, PagesService pagesService, PagePermissionsService pagePermissionsService, DataSourceService dataSourceService, NavigationGroupPermissionsService navigationGroupPermissionsService, NavigationGroupService navigationGroupService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.pagesService = pagesService;
            this.pagePermissionsService = pagePermissionsService;
            this.dataSourceService = dataSourceService;
            this.navigationGroupPermissionsService = navigationGroupPermissionsService;
            this.navigationGroupService = navigationGroupService;
        }

        public async Task<CoreListResponse<ActionsQueryDTO>> Handle(GetRolesActionsQuery request, CancellationToken cancellationToken)
        {
            var actions = new List<ActionsQueryDTO>() { };

            if (string.IsNullOrWhiteSpace(request.RecordTypeCode))
            {
                return new CoreListResponse<ActionsQueryDTO>()
                {
                    Data = actions,
                };
            }

            actions.Add(new ActionsQueryDTO()
            {
                RecordTypeCode = UserRecordTypes.ROLES,
                ActionId = 5,
                ActionName = "s:newRole",
                ActionCode = UserTableActions.NEW_ROLE,
                BaseActionCode = "NEW",
                ActionProcedure = "POST;/roles/roles",
            });

            actions.Add(new ActionsQueryDTO()
            {
                RecordTypeCode = UserRecordTypes.ROLES,
                ActionId = 5,
                ActionName = "s:editRole",
                ActionCode = UserTableActions.EDIT_ROLE,
                BaseActionCode = "Edit",
                ExtraParamsFormUrl = "POST;/com/module/forms?FormCode=EDIT_ROLE",
                ActionProcedure = "POST;/roles/roles/{id}",
            });
            if (request.RecordTypeCode.Equals(UserRecordTypes.ROLE_PAGE_PERMISSIONS) && (request.RecordId == null || pagePermissionsService.GetRolesPagePermissions((int)pagePermissionsService.GetPagePermission((int)request.RecordId).RoleId).Count() != pagesService.GetPages().Count()))
            {
                actions.Add(new ActionsQueryDTO()
                {
                    RecordTypeCode = UserRecordTypes.ROLE_PAGE_PERMISSIONS,
                    ActionId = 5,
                    ActionName = "s:addPagePermission",
                    ActionCode = UserTableActions.ADD_PAGE_PERMISSION,
                    BaseActionCode = "NEW",
                    ActionProcedure = "POST;/roles/pagePermissions",
                });
            }

            if (request.RecordId != null)
            {
                actions.Add(new ActionsQueryDTO()
                {
                    RecordTypeCode = UserRecordTypes.ROLE_PAGE_PERMISSIONS,
                    ActionId = 5,
                    ActionName = "s:removePagePermission",
                    ActionCode = UserTableActions.REMOVE_PAGE_PERMISSION,
                    BaseActionCode = "DELETE",
                    ActionProcedure = "DELETE;/roles/pagePermissions/{id}",
                });
            }

            if (request.RecordTypeCode.Equals(UserRecordTypes.ROLE_NAVIGATION_GROUP_PERMISSIONS) && (request.RecordId == null || navigationGroupPermissionsService.GetRolesNavigationGroupPermissions((int)navigationGroupPermissionsService.GetNavigationGroupPermission((int)request.RecordId).RoleId).Count() != navigationGroupService.GetNavigationGroups().Count())) {
                actions.Add(new ActionsQueryDTO()
                {
                    RecordTypeCode = UserRecordTypes.ROLE_NAVIGATION_GROUP_PERMISSIONS,
                    ActionId = 5,
                    ActionName = "s:addNavigationGroupPermission",
                    ActionCode = UserTableActions.ADD_NAVIGATION_GROUP_PERMISSION,
                    BaseActionCode = "NEW",
                    ActionProcedure = "POST;/roles/navigationGroupPermissions",
                });
            }

            if (request.RecordId != null) {
                actions.Add(new ActionsQueryDTO()
                {
                    RecordTypeCode = UserRecordTypes.ROLE_NAVIGATION_GROUP_PERMISSIONS,
                    ActionId = 5,
                    ActionName = "s:removeNavigationGroupPermission",
                    ActionCode = UserTableActions.REMOVE_NAVIGATION_GROUP_PERMISSION,
                    BaseActionCode = "DELETE",
                    ActionProcedure = "DELETE;/roles/navigationGroupPermissions/{id}",
                });
            }

            var filteredActions = actions.FindAll(action => action.RecordTypeCode == request.RecordTypeCode);
            return new CoreListResponse<ActionsQueryDTO>()
            {
                Data = filteredActions,
            };
        }
    }
}
