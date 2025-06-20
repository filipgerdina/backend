using Microsoft.AspNetCore.Mvc;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Services;
using WebApplication2.Models.Api;
using MediatR;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.User.Commands.AddRoleToUserCommand;
using WebApplication2.Business.User.Commands.RemoveRoleFromUserCommand;
using WebApplication2.Business.User.Commands.SyncDomainUsersCommand;
using WebApplication2.Business.User.Commands.LockUserCommand;
using WebApplication2.Business.User.Commands.UnlockUserCommand;
using WebApplication2.Buissniss.User.Queries.GetUserInformationQuery;
using WebApplication2.Buissniss.Users.Module.GetUserActionFormQuery;
using WebApplication2.Buissniss.Users.Module.GetUserActionsQuery;
using WebApplication2.Business.User.Commands.EditProfileCommand;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : CoreControllerBase
    {
        //private readonly UserService _usersService;
        //private readonly RoleService _roleService;

        public UsersController(IMediator mediator) : base(mediator)
        {
            //_usersService = usersService;
            //_roleService = roleService;
        }

        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<CoreListResponse<GetUsersQueryDTO>>> GetUsers([FromQuery] GetUsersQuery getUsersQuery) 
        {
            return await this.SendRequest(getUsersQuery);
        }

        [HttpPost]
        [Route("userInformation")]
        public async Task<ActionResult<CoreResponse<GetUserInformationQueryDTO>>> GetUserInformation([FromQuery] GetUserInformationQuery getUserInformationQuery)
        {
            return await this.SendRequest(getUserInformationQuery);
        }

        [HttpPost("users")]
        public async Task<ActionResult<RecordIDResponse>> NewUser(NewUserCommand newUserCommand)
        {
            return await this.SendRequest(newUserCommand);
        }

        [HttpPost("users/{id}")]
        public async Task<ActionResult<RecordIDResponse>> EditUser(int id, EditUserCommand editUserCommand)
        {
            editUserCommand.Data.ExtraParamsFormValues.Id = id;
            return await this.SendRequest(editUserCommand);
        }

        [HttpPost("lock/{id}")]
        public async Task<ActionResult<RecordIDResponse>> LockUser(int id, LockUserCommand lockUserCommand)
        {
            lockUserCommand.Data.SetId(id);
            return await this.SendRequest(lockUserCommand);
        }

        [HttpPost("unlock/{id}")]
        public async Task<ActionResult<RecordIDResponse>> UnlockUser(int id, UnlockUserCommand unlockUserCommand)
        {
            unlockUserCommand.Data.SetId(id);
            return await this.SendRequest(unlockUserCommand);
        }

        [HttpPost("userRoles")]
        public async Task<ActionResult<RecordIDResponse>> AddRoleToUser(AddRoleToUserCommand addRoleToUserCommand)
        {
            return await this.SendRequest(addRoleToUserCommand);
        }

        [HttpPost("userRoles/{id}")]
        public async Task<ActionResult<RecordIDResponse>> RemoveRoleFromUser(int id, RemoveRoleFromUserCommand removeRoleFromUserCommand)
        {
            removeRoleFromUserCommand.Data.SetId(id);
            return await this.SendRequest(removeRoleFromUserCommand);
        }

        [HttpPost("editProfile")]
        public async Task<ActionResult<RecordIDResponse>> EditProfile(EditProfileCommand editProfileCommand)
        {
            return await this.SendRequest(editProfileCommand);
        }

        [HttpGet]
        [Route("userRoles")]
        public async Task<ActionResult<CoreListResponse<GetRolesOfUserQueryDTO>>> GetRolesOfUserQuery([FromQuery] GetRolesOfUserQuery getRolesOfUser)
        {
            return await this.SendRequest(getRolesOfUser);
        }

        [HttpPost("syncDomainUsers")]
        public async Task<ActionResult<RecordIDResponse>> SyncDomainUsers(SyncDomainUsersCommand syncDomainUsersCommand)
        {
            return await this.SendRequest(syncDomainUsersCommand);
        }
        

        [HttpGet("actions")]
        public async Task<ActionResult<CoreListResponse<ActionsQueryDTO>>> GetUserActions([FromQuery] GetUserActionsQuery getUserActionsQuery)
        {
            return await this.SendRequest(getUserActionsQuery);
        }

        /// <summary>
        /// Return wanted action form.
        /// </summary>
        [HttpPost("forms")]
        public async Task<ActionResult<CoreResponse<ActionFormQueryDTO>>> GetUserActionForm([FromQuery] string formCode, GetUserActionFormQuery getUserActionFormQuery)
        {
            getUserActionFormQuery.Data.FormCode = formCode;
            return await this.SendRequest(getUserActionFormQuery);
        }
    }

}
