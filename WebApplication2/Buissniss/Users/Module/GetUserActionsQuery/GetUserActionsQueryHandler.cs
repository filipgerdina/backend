using MediatR;
using WebApplication2.Models.Responses;
using WebApplication2.Models.CodeList;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Users.Module.GetUserActionsQuery
{
    public class GetUserActionsQueryHandler : IRequestHandler<GetUserActionsQuery, CoreListResponse<ActionsQueryDTO>>
    {

        private readonly UserService userService;
        private readonly RoleService roleService;

        public GetUserActionsQueryHandler(UserService userService, RoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public async Task<CoreListResponse<ActionsQueryDTO>> Handle(GetUserActionsQuery request, CancellationToken cancellationToken)
        {
            var actions = new List<ActionsQueryDTO>() { };

            if (string.IsNullOrWhiteSpace(request.RecordTypeCode))
            {
                return new CoreListResponse<ActionsQueryDTO>()
                {
                    Data = actions,
                };
            }

            List<RoleClass> rolesOfUser = null;
            UserClass user = null;

            if(request.RecordId != null) {
                if (request.RecordTypeCode == UserRecordTypes.USER_ROLES)
                {
                    var userId = userService.GetUserByUserRole((int)request.RecordId).Id;
                    rolesOfUser = userService.GetActiveUserRoles(userId).ToList();
                }
                if (request.RecordTypeCode == UserRecordTypes.USERS)
                    user = userService.GetUsers().ToList().Find(u => u.Id == (int)request.RecordId);
            }


            actions.Add(new ActionsQueryDTO()
            {
                RecordTypeCode = UserRecordTypes.USERS,
                ActionId = 1,
                ActionName = "s:addUser",
                ActionCode = UserTableActions.NEW_USER,
                BaseActionCode = "NEW",
                ExtraParamsFormUrl = "POST;/com/module/forms?FormCode=NEW_USER",
                ActionProcedure = "POST;/users/users",
            });

            //actions.Add(new ActionsQueryDTO()
            //{
            //    RecordTypeCode = UserRecordTypes.USERS,
            //    ActionId = 2,
            //    ActionName = "s:editUser",
            //    ActionCode = UserTableActions.EDIT_USER,
            //    BaseActionCode = "EDIT",
            //    ExtraParamsFormUrl = "POST;/com/module/forms?FormCode=NEW_USER",
            //    ActionProcedure = "POST;/users/users/{id}",
            //});

            actions.Add(new ActionsQueryDTO()
            {
                RecordTypeCode = UserRecordTypes.USERS,
                ActionId = 5,
                ActionName = "s:importUsersFromDomain",
                ActionCode = UserTableActions.SYNC_DOMAIN_USERS,
                BaseActionCode = "SYNC",
                ExtraParamsFormUrl = "POST;/com/module/forms?FormCode=SYNC_DOMAIN_USERS",
                ActionProcedure = "POST;/users/syncDomainUsers",
            });

            if (user != null) {
                if (!user.Is_System) {
                    if (user.Is_Locked)
                    {
                        actions.Add(new ActionsQueryDTO()
                        {
                            RecordTypeCode = UserRecordTypes.USERS,
                            ActionId = 5,
                            ActionName = "s:unlockUser",
                            ActionCode = UserTableActions.UNLOCK_USER,
                            BaseActionCode = "UNLOCK",
                            ActionProcedure = "POST;/users/unlock/{id}",
                        });
                    }
                    else {
                        actions.Add(new ActionsQueryDTO()
                        {
                            RecordTypeCode = UserRecordTypes.USERS,
                            ActionId = 5,
                            ActionName = "s:lockUser",
                            ActionCode = UserTableActions.LOCK_USER,
                            BaseActionCode = "LOCK",
                            ActionProcedure = "POST;/users/lock/{id}",
                        });
                    }
                }
            }


            if (rolesOfUser == null || rolesOfUser.Count() < roleService.GetRoles().Count())
            {
                actions.Add(new ActionsQueryDTO()
                {
                    RecordTypeCode = UserRecordTypes.USER_ROLES,
                    ActionId = 3,
                    ActionName = "s:assignRoleToUser",
                    ActionCode = UserTableActions.ADD_ROLE_TO_USER,
                    BaseActionCode = "NEW",
                    ExtraParamsFormUrl = "POST;/com/module/forms?FormCode=ADD_ROLE",
                    ActionProcedure = "POST;/users/userRoles",
                });
            }

            if (rolesOfUser != null && rolesOfUser.Count() > 0)
            {
                actions.Add(new ActionsQueryDTO()
                {
                    RecordTypeCode = UserRecordTypes.USER_ROLES,
                    ActionId = 4,
                    ActionName = "s:removeRoleFromUser",
                    ActionCode = UserTableActions.REMOVE_ROLE_FROM_USER,
                    BaseActionCode = "DELETE",
                    ActionProcedure = "POST;/users/userRoles/{id}",
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
