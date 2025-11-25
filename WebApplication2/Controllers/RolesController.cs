using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Api;
using MediatR;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses;
using WebApplication2.Buissniss.Users.Module.GetUserActionFormQuery;
using WebApplication2.Buissniss.Users.Module.GetUserActionsQuery;
using WebApplication2.Buissniss.Roles.Queries.GetRolesQuery;
using WebApplication2.Business.Roles.Commands.AddRoleCommand;
using WebApplication2.Buissniss.Roles.Module.GetRolesActionsQuery;
using WebApplication2.Roles.Module.GetRolesActionsFormQuery;
using WebApplication2.Buissniss.Roles.Queries.GetPagePermissionsOfRoleQuery;
using WebApplication2.Business.Roles.Commands.AddPagePermissionCommand;
using WebApplication2.Business.Roles.Commands.RemovePagePerrmissionCommand;
using WebApplication2.Business.User.Commands.LockUserCommand;
using WebApplication2.Business.Roles.Commands.EditRoleCommand;
using WebApplication2.Buissniss.Roles.Queries.GetPagesOfRoleQuery;
using WebApplication2.Business.Roles.Commands.RemoveNavigationGroupPerrmissionCommand;
using WebApplication2.Business.Roles.Commands.AddNavigationGroupPermissionCommand;
using WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupPermissionsOfRoleQuery;
using WebApplication2.Buissniss.Roles.Queries.GetRolesGroupQuery;
using WebApplication2.Models.Request;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("roles")]
    public class RolesController : CoreControllerBase
    {
        //private readonly UserService _usersService;
        //private readonly RoleService _roleService;

        public RolesController(IMediator mediator) : base(mediator)
        {
            //_usersService = usersService;
            //_roleService = roleService;
        }

        [HttpGet]
        [Route("roles")]
        public async Task<ActionResult<CoreListResponse<GetRolesQueryDTO>>> GetRoles([FromQuery] GetRolesQuery getRolesQuery)
        {
            return await this.SendRequest(getRolesQuery);
        }

        [HttpGet]
        [Route("roles/groups")]
        public async Task<ActionResult<CoreListResponse<GroupResponse>>> GetRolesGroups([FromQuery] GetRolesGroupQuery getRolesQuery)
        {
            return await this.SendRequest(getRolesQuery);
        }

        [HttpPost("roles")]
        public async Task<ActionResult<RecordIDResponse>> NewRole(NewRoleCommand newRoleCommand)
        {
            return await this.SendRequest(newRoleCommand);
        }


        [HttpPost("roles/{id}")]
        public async Task<ActionResult<RecordIDResponse>> EditRole(int id, EditRoleCommand editRoleCommand)
        {
            editRoleCommand.Data.SetId(id);
            return await this.SendRequest(editRoleCommand);
        }

        [HttpGet]
        [Route("roles/pages")]
        public async Task<ActionResult<CoreListResponse<GetPagesOfRoleQueryDTO>>> GetRolePages([FromQuery] GetPagesOfRoleQuery getPagesOfRoleQuery)
        {
            return await this.SendRequest(getPagesOfRoleQuery);
        }

        [HttpGet]
        [Route("roles/navigationGroups")]
        public async Task<ActionResult<CoreListResponse<GetNavigationGroupPermissionsOfRoleQueryDTO>>> GetRoleNavigationGroups([FromQuery] GetNavigationGroupPermissionsOfRoleQuery getNavigationGroupsOfRoleQuery)
        {
            return await this.SendRequest(getNavigationGroupsOfRoleQuery);
        }

        [HttpGet]
        [Route("pagePermissions")]
        public async Task<ActionResult<CoreListResponse<GetPagePermissionsOfRoleQueryDTO>>> GetPagePermissions([FromQuery] GetPagePermissionsOfRoleQuery getPagePermissionsQuery)
        {
            return await this.SendRequest(getPagePermissionsQuery);
        }

        [HttpPost("pagePermissions")]
        public async Task<ActionResult<RecordIDResponse>> AddPagePermission(AddPagePermissionCommand addPagePermissionCommand)
        {
            return await this.SendRequest(addPagePermissionCommand);
        }

        [HttpDelete("pagePermissions/{id}")]
        public async Task<ActionResult<RecordIDResponse>> RemovePagePermission(int id, RemovePagePerrmissionCommand removePagePermissionCommand)
        {
            removePagePermissionCommand.Data.SetId(id);
            return await this.SendRequest(removePagePermissionCommand);
        }

        [HttpGet]
        [Route("navigationGroupPermissions")]
        public async Task<ActionResult<CoreListResponse<GetNavigationGroupPermissionsOfRoleQueryDTO>>> GetNavigationGroupPermissions([FromQuery] GetNavigationGroupPermissionsOfRoleQuery getNavigationGroupPermissionsQuery)
        {
            return await this.SendRequest(getNavigationGroupPermissionsQuery);
        }

        [HttpPost("navigationGroupPermissions")]
        public async Task<ActionResult<RecordIDResponse>> AddNavigationGroupPermission(AddNavigationGroupPermissionCommand addNavigationGroupPermissionCommand)
        {
            return await this.SendRequest(addNavigationGroupPermissionCommand);
        }

        [HttpDelete("navigationGroupPermissions/{id}")]
        public async Task<ActionResult<RecordIDResponse>> RemoveNavigationGroupPermission(int id, RemoveNavigationGroupPerrmissionCommand removeNavigationGroupPermissionCommand)
        {
            removeNavigationGroupPermissionCommand.Data.SetId(id);
            return await this.SendRequest(removeNavigationGroupPermissionCommand);
        }


        [HttpGet("actions")]
        public async Task<ActionResult<CoreListResponse<ActionsQueryDTO>>> GetRolesActions([FromQuery] GetRolesActionsQuery getRolesActionsQuery)
        {
            return await this.SendRequest(getRolesActionsQuery);
        }

        /// <summary>
        /// Return wanted action form.
        /// </summary>
        [HttpPost("forms")]
        public async Task<ActionResult<CoreResponse<ActionFormQueryDTO>>> GetRolesActionForm([FromQuery] string formCode, GetRolesActionFormQuery getRolesActionFormQuery)
        {
            getRolesActionFormQuery.Data.FormCode = formCode;
            return await this.SendRequest(getRolesActionFormQuery);
        }
    }

}
