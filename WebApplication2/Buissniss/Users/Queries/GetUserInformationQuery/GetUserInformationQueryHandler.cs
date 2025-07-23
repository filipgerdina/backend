using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WebApplication2.Buissniss.User.Queries.GetUserInformationQuery;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

public class GetUserInformationQueryHandler : IRequestHandler<GetUserInformationQuery, CoreResponse<GetUserInformationQueryDTO>>
{
    private readonly UserService _usersService;
    private readonly ApplicationSettingsService _appSettingsService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserInformationQueryHandler(UserService usersService, ApplicationSettingsService appSettingsService, IHttpContextAccessor httpContextAccessor)
    {
        _usersService = usersService;
        _appSettingsService = appSettingsService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CoreResponse<GetUserInformationQueryDTO>> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
    {
        var result = new CoreResponse<GetUserInformationQueryDTO>();

        // Access ClaimsPrincipal from HttpContext
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            // Handle not authenticated case
            result.Messages.Add( new WebApplication2.Models.IResponse.Message
            {
                Text = "User not authenticated."
            });
            return result;
        }

        // Get claims
        var usernameClaim = user.FindFirst(ClaimTypes.Name)?.Value;
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Example: use usernameClaim to query user
        result.Data = _usersService.GetUsers()
            .Where(u => u.Username.Equals(usernameClaim, StringComparison.OrdinalIgnoreCase))
            .Select(u => new GetUserInformationQueryDTO
            {
                Username = u.Username,
                DisplayName = u.First_Name != null || u.Last_Name != null ? u.First_Name + " " + u.Last_Name : u.Display_Name,
                FirstName = u.First_Name,
                LastName = u.Last_Name,
                DomainUserName = (u is DomainUserClass) ? ((DomainUserClass)u).Domain + "/" + u.Username : null,
                Email = u.Email,
                Added = u.Added,
                RoleNames = _usersService.GetActiveUserRoles(u.Id).Select(r => r.Name).ToList(),
                DefaultPagePath = _usersService.GetUserDefaultPage(u.Id) != null ? _usersService.GetUserDefaultPage(u.Id).Path : null,
                LanguageId = _appSettingsService.GetAllSettings().Find(a => a.Id == u.ID_Setting)?.ID_language ?? null,
                DateTimeFormatId = _appSettingsService.GetAllSettings().Find(a => a.Id == u.ID_Setting)?.ID_date_time_format ?? null,
                DecimalSeperatorId = _appSettingsService.GetAllSettings().Find(a => a.Id == u.ID_Setting)?.ID_separator ?? null,
            })
            .FirstOrDefault();

        return result;
    }
}
