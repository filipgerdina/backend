using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using WebApplication2.Buissniss.Roles.Module.GetRolesActionsQuery;
using WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupPermissionsOfRoleQuery;
using WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupsOfRoleQuery;
using WebApplication2.Buissniss.Roles.Queries.GetPagePermissionsOfRoleQuery;
using WebApplication2.Buissniss.Roles.Queries.GetPagesOfRoleQuery;
using WebApplication2.Buissniss.Roles.Queries.GetRolesQuery;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUserInformationQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Buissniss.Users.Module.GetUserActionFormQuery;
using WebApplication2.Buissniss.Users.Module.GetUserActionsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetApplicationSettingsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetDataSourceQuery;
using WebApplication2.Buissniss.Utl.Queries.GetDateTimeFormatsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetDecimalSeperatorsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetLanguagesQuery;
using WebApplication2.Buissniss.Utl.Queries.GetNavigationGroupsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetPagesQuery;
using WebApplication2.Buissniss.Utl.Queries.GetTranslationsQuery;
using WebApplication2.Business.User.Commands.EditProfileCommand;
using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Hubs;
using WebApplication2.Models;
using WebApplication2.Models.Api;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;
using WebApplication2.Roles.Module.GetRolesActionsFormQuery;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("utl")]
    public class UtlController : CoreControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly PagesService _pagesService;
        private readonly NavigationGroupService _navigationGroupService;
        private readonly IHubContext<PagesHub> _hubContext;

        public UtlController(
            UserService userService,
            JwtTokenGenerator tokenGenerator,
            PagesService pagesService,
            NavigationGroupService navigationGroupService,
            IMediator mediator,
            IHubContext<PagesHub> hubContext
        ) : base(mediator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
            _pagesService = pagesService;
            _navigationGroupService = navigationGroupService;
            _hubContext = hubContext;
        }


        [HttpGet]
        [Route("pages")]
        public async Task<ActionResult<CoreListResponse<GetPagesQueryDTO>>> GetPages([FromQuery] GetPagesQuery getPagesQuery)
        {
            return await this.SendRequest(getPagesQuery);
        }

        [HttpGet]
        [Route("dataSources")]
        public async Task<ActionResult<CoreListResponse<GetDataSourceQueryDTO>>> GetDataSources([FromQuery] GetDataSourceQuery getDataSourcesQuery)
        {
            return await this.SendRequest(getDataSourcesQuery);
        }

        [HttpGet]
        [Route("translations")]
        public async Task<ActionResult<CoreListResponse<GetTranslationsQueryDTO>>> GetTranslations([FromQuery] GetTranslationsQuery getTranslationsQuery)
        {
            return await this.SendRequest(getTranslationsQuery);
        }

        [HttpGet]
        [Route("navigationGroups")]
        public async Task<ActionResult<CoreListResponse<GetNavigationGroupsQueryDTO>>> GetNavigationGroups([FromQuery] GetNavigationGroupsQuery getNavigationGroupsQuery)
        {
            return await this.SendRequest(getNavigationGroupsQuery);
        }

        [HttpGet]
        [Route("applicationSettings")]
        public async Task<ActionResult<CoreResponse<GetApplicationSettingsQueryDTO>>> GetApplicationSettings([FromQuery] GetApplicationSettingsQuery getApplicationSettingsQuery)
        {
            return await this.SendRequest(getApplicationSettingsQuery);
        }

        [HttpGet]
        [Route("languages")]
        public async Task<ActionResult<CoreListResponse<GetLanguagesQueryDTO>>> GetLanguages([FromQuery] GetLanguagesQuery getLanguagesQuery)
        {
            return await this.SendRequest(getLanguagesQuery);
        }

        [HttpGet]
        [Route("dateTimeFormats")]
        public async Task<ActionResult<CoreListResponse<GetDateTimeFormatsQueryDTO>>> GetDateTimeFormats([FromQuery] GetDateTimeFormatsQuery getDateTimeFormatsQuery)
        {
            return await this.SendRequest(getDateTimeFormatsQuery);
        }

        [HttpGet]
        [Route("decimalSeperators")]
        public async Task<ActionResult<CoreListResponse<GetDecimalSeperatorsQueryDTO>>> GetDecimalSeperators([FromQuery] GetDecimalSeperatorsQuery getDecimalSeperatorsQuery)
        {
            return await this.SendRequest(getDecimalSeperatorsQuery);
        }

        /// <summary>
        /// Triggers a refresh signal for all connected clients (SignalR).
        /// </summary>

        [HttpPost("pages/refresh")]
        public async Task<IActionResult> BroadcastPagesRefresh()
        {
            // Optionally you can send the new page list here.
            await _hubContext.Clients.All.SendAsync("RefreshPages");
            return Ok(new { message = "Refresh signal sent." });
        }

        [Authorize]
        [HttpPost(Name = "PostDataSource")]
        public async Task<IActionResult> Post([FromBody] DataSourcePost parameters)
        {
            var query = new Object();

            switch (parameters.Name)
            {
                case "pages":
                    return Ok(await _mediator.Send(new GetPagesQuery()));

                case "dataSources":
                    return Ok(await _mediator.Send(new GetDataSourceQuery()));

                case "navigationGroups":
                    return Ok(await _mediator.Send(new GetNavigationGroupsQuery()));

                case "users":
                    return Ok(await _mediator.Send(new GetUsersQuery()));

                case "roles":
                    return Ok(await _mediator.Send(new GetRolesQuery()));

                case "userRoles":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    query = parameters.QueryParams.Deserialize<GetRolesOfUserQuery>();
                    return Ok(await _mediator.Send(query));

                case "pagePermissions":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    query = parameters.QueryParams.Deserialize<GetPagePermissionsOfRoleQuery>();
                    return Ok(await _mediator.Send(query));

                case "navigationGroupPermissions":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    query = parameters.QueryParams.Deserialize<GetNavigationGroupPermissionsOfRoleQuery>();
                    return Ok(await _mediator.Send(query));

                case "rolePages":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    query = parameters.QueryParams.Deserialize<GetPagesOfRoleQuery>();
                    return Ok(await _mediator.Send(query));

                case "roleNavigationGroups":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    query = parameters.QueryParams.Deserialize<GetNavigationGroupsOfRoleQuery>();
                    return Ok(await _mediator.Send(query));


                case "editProfile":
                    if (parameters.BodyParams.ValueKind == JsonValueKind.Undefined || parameters.BodyParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    var dataJson = parameters.BodyParams.GetProperty("data");

                    var extraParams = dataJson.GetProperty("extraParamsFormValues")
                        .Deserialize<EditProfileCommandParametersDataFields>(
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                        });

                    var data = new ActionRequestExtraParam<EditProfileCommandParametersDataFields>
                    {
                        ActionCode = dataJson.GetProperty("actionCode").GetString(),
                        CurrentStateCode = "dfsfsd",
                        RecordTypeCode = dataJson.GetProperty("recordTypeCode").GetString(),
                        ExtraParamsFormValues = extraParams
                    };

                    var eSign = new ESign();

                    var command = new EditProfileCommand
                    {
                        Data = data,
                        ESign = eSign
                    };

                    return Ok(await _mediator.Send(command));

                case "userInformation":
                    return Ok(await _mediator.Send(new GetUserInformationQuery()));
                case "userModuleActions":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    query = parameters.QueryParams.Deserialize<GetUserActionsQuery>();
                    return Ok(await _mediator.Send(query));

                case "userModuleActionForms":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });

                    var userActionFormQuery = parameters.QueryParams.Deserialize<GetUserActionFormQuery>(
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                    return Ok(await _mediator.Send(userActionFormQuery));

                case "rolesModuleActions":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    query = parameters.QueryParams.Deserialize<GetRolesActionsQuery>();
                    return Ok(await _mediator.Send(query));

                case "rolesModuleActionForms":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });

                    var rolesActionFormQuery = parameters.QueryParams.Deserialize<GetRolesActionFormQuery>(
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                    return Ok(await _mediator.Send(rolesActionFormQuery));
            }

            return NotFound(new { message = $"No handler for '{parameters.Name}'." });
        }
    }
}
