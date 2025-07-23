using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using System.Text;
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
using WebApplication2.Buissniss.Utl.Module.GetUtlActionsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetApplicationSettingsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetDataSourceQuery;
using WebApplication2.Buissniss.Utl.Queries.GetDateTimeFormatsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetDecimalSeperatorsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetLanguagesQuery;
using WebApplication2.Buissniss.Utl.Queries.GetModulesQuery;
using WebApplication2.Buissniss.Utl.Queries.GetNavigationGroupsQuery;
using WebApplication2.Buissniss.Utl.Queries.GetPagesQuery;
using WebApplication2.Buissniss.Utl.Queries.GetTranslationsQuery;
using WebApplication2.Business.User.Commands.ChangePasswordCommand;
using WebApplication2.Business.User.Commands.EditProfileCommand;
using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.Utl.Commands.EditApplicationSettingsCommand;
using WebApplication2.Hubs;
using WebApplication2.Models;
using WebApplication2.Models.Api;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Roles.Module.GetRolesActionsFormQuery;
using WebApplication2.Services;
using WebApplication2.Utl.Module.GetUtlActionFormQuery;

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
        [Route("modules")]
        public async Task<ActionResult<CoreListResponse<GetModulesQueryDTO>>> GetModules([FromQuery] GetModulesQuery getModuleQuery)
        {
            return await this.SendRequest(getModuleQuery);
        }

        [HttpGet]
        [Route("dataSources")]
        public async Task<ActionResult<CoreListResponse<GetDataSourceQueryDTO>>> GetDataSources([FromQuery] GetDataSourceQuery getDataSourcesQuery)
        {
            return await this.SendRequest(getDataSourcesQuery);
        }

        [HttpGet]
        [Route("translations")]
        public async Task<IActionResult> GetTranslations([FromQuery] GetTranslationsQuery getTranslationsQuery)
        {
            // Fetch the data (could be from DB or elsewhere)
            var result = await this.SendRequest(getTranslationsQuery);

            // Generate a hash or version string based on content (e.g., a timestamp or hash)
            var contentHash = GenerateETag(result);

            // Check if client already has the latest version
            var clientETag = Request.Headers["If-None-Match"].FirstOrDefault();
            if (clientETag == contentHash)
            {
                // Nothing changed
                return StatusCode(StatusCodes.Status304NotModified);
            }

            // Set ETag header
            Response.Headers["ETag"] = contentHash;

            // Optional: still control cache behavior
            Response.Headers["Cache-Control"] = "no-cache";

            return Ok(result);
        }

        private string GenerateETag(object response)
        {
            var serialized = JsonSerializer.Serialize(response);
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(serialized));
            return $"\"{Convert.ToBase64String(hashBytes)}\"";  // ETag must be quoted
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

        [HttpPost("applicationSettings")]
        public async Task<ActionResult<RecordIDResponse>> EditApplicationSettings(EditApplicationSettingsCommand editApplicationSettingsCommand)
        {
            return await this.SendRequest(editApplicationSettingsCommand);
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

        [HttpGet("actions")]
        public async Task<ActionResult<CoreListResponse<ActionsQueryDTO>>> GetUrlActions([FromQuery] GetUtlActionsQuery getUtlActionsQuery)
        {
            return await this.SendRequest(getUtlActionsQuery);
        }

        /// <summary>
        /// Return wanted action form.
        /// </summary>
        [HttpPost("forms")]
        public async Task<ActionResult<CoreResponse<ActionFormQueryDTO>>> GetUtlActionForm([FromQuery] string formCode, GetUtlActionFormQuery getUtlActionFormQuery)
        {
            getUtlActionFormQuery.Data.FormCode = formCode;
            return await this.SendRequest(getUtlActionFormQuery);
        }

        [Authorize]
        [HttpPost("resolveDataSource")]
        public async Task<IActionResult> Post([FromBody] DataSourcePost parameters)
        {
            var query = new Object();

            switch (parameters.Name)
            {
                case "pages":
                    return Ok(await _mediator.Send(new GetPagesQuery()));

                case "modules":
                    return Ok(await _mediator.Send(new GetModulesQuery()));
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


                case "changePassword":
                    if (parameters.BodyParams.ValueKind == JsonValueKind.Undefined || parameters.BodyParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    var dataJson3 = parameters.BodyParams.GetProperty("data");

                    var extraParams3 = dataJson3.GetProperty("extraParamsFormValues")
                        .Deserialize<ChangePasswordCommandParametersDataFields>(
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                    var data3 = new ActionRequestExtraParam<ChangePasswordCommandParametersDataFields>
                    {
                        ActionCode = dataJson3.GetProperty("actionCode").GetString(),
                        CurrentStateCode = "dfsfsd",
                        RecordTypeCode = dataJson3.GetProperty("recordTypeCode").GetString(),
                        ExtraParamsFormValues = extraParams3
                    };

                    var eSign3 = new ESign();

                    var command3 = new ChangePasswordCommand
                    {
                        Data = data3,
                        ESign = eSign3
                    };

                    return Ok(await _mediator.Send(command3));

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

                case "editApplicationSettings":
                    if (parameters.BodyParams.ValueKind == JsonValueKind.Undefined || parameters.BodyParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    var dataJson2 = parameters.BodyParams.GetProperty("data");

                    var extraParams2 = dataJson2.GetProperty("extraParamsFormValues")
                        .Deserialize<EditApplicationSettingsCommandParametersDataFields>(
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                    var data2 = new ActionRequestExtraParam<EditApplicationSettingsCommandParametersDataFields>
                    {
                        ActionCode = dataJson2.GetProperty("actionCode").GetString(),
                        CurrentStateCode = "dfsfsd",
                        RecordTypeCode = dataJson2.GetProperty("recordTypeCode").GetString(),
                        ExtraParamsFormValues = extraParams2
                    };

                    var eSign2 = new ESign();

                    var command2 = new EditApplicationSettingsCommand
                    {
                        Data = data2,
                        ESign = eSign2
                    };

                    return Ok(await _mediator.Send(command2));

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

                case "utlModuleActions":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });
                    query = parameters.QueryParams.Deserialize<GetUtlActionsQuery>();
                    return Ok(await _mediator.Send(query));

                case "utlModuleActionForms":
                    if (parameters.QueryParams.ValueKind == JsonValueKind.Undefined || parameters.QueryParams.ValueKind == JsonValueKind.Null)
                        return NotFound(new { message = $"Parameters for handler '{parameters.Name}' were undefined" });

                    var utlActionFormQuery = parameters.QueryParams.Deserialize<GetUtlActionFormQuery>(
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                    return Ok(await _mediator.Send(utlActionFormQuery));
            }

            return NotFound(new { message = $"No handler for '{parameters.Name}'." });
        }

        [HttpPost("processEndPointAction")]
        public async Task<IActionResult> Post([FromBody] ProcessEndPointActionPost parameters)
        {
            using var httpClient = new HttpClient();

            // Build the full URL with query parameters

            var url = parameters.Name;
            // Add URL parameters to request headers if needed (uncommon, clarify usage)
            if (parameters.UrlParams.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in parameters.UrlParams.EnumerateObject())
                {
                    url = url.Replace("{" + prop.Name + "}", prop.Value.ToString());
                }
            }

            var urlBuilder = new UriBuilder(url);
            if (parameters.QueryParams.ValueKind == JsonValueKind.Object)
            {
                var query = System.Web.HttpUtility.ParseQueryString(urlBuilder.Query);
                foreach (var prop in parameters.QueryParams.EnumerateObject())
                {
                    query[prop.Name] = prop.Value.ToString();
                }
                urlBuilder.Query = query.ToString();
            }

            // Prepare the request
            var request = new HttpRequestMessage
            {
                RequestUri = urlBuilder.Uri,
            };

            switch (parameters.Method) {
                case "GET":
                    request.Method = HttpMethod.Get;
                    break;
                case "PUT":
                    request.Method = HttpMethod.Put;
                    break;
                case "DELETE":
                    request.Method = HttpMethod.Delete;
                    break;
                default:
                    request.Method = HttpMethod.Post;
                    break;
            }
            // Add body content if present
            if (parameters.BodyParams.ValueKind != JsonValueKind.Undefined &&
                parameters.BodyParams.ValueKind != JsonValueKind.Null)
            {
                var jsonBody = parameters.BodyParams.GetRawText();
                request.Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
            }

            var accessToken = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Replace("Bearer ", ""));
            }



            // Execute the request
            var response = await httpClient.SendAsync(request);

            var responseBody = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, responseBody);
        }

    }
}
