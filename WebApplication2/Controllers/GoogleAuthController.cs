using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth/google")]
public class GoogleIdController : ControllerBase
{
    private readonly UserService _users;
    private readonly JwtTokenGenerator _jwt;
    private readonly ILogger<GoogleIdController> _log;
    private readonly string _googleClientId;

    public GoogleIdController(UserService users, JwtTokenGenerator jwt, ILogger<GoogleIdController> log, IConfiguration cfg)
    {
        _users = users; _jwt = jwt; _log = log;
        _googleClientId = cfg["GoogleOAuth:ClientId"]!;
    }

    public record IdLoginRequest(string id_token);

    [HttpPost("id-login")]
    [AllowAnonymous]
    public async Task<IActionResult> IdLogin([FromBody] IdLoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.id_token))
            return BadRequest(new { message = "Missing id_token" });

        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(
                req.id_token,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleClientId } // must match your Client ID
                });
        }
        catch (Exception ex)
        {
            _log.LogWarning(ex, "Invalid Google ID token");
            return Unauthorized(new { message = "Invalid Google token" });
        }

        // Map/create local user (no password needed)
        var user = _users.GetUserById(1);

        if (user.Is_Locked) return Unauthorized(new { message = "s:accountIsLocked" });

        // Your own app session: short-lived access token + HttpOnly refresh cookie (optional)
        var accessToken = _jwt.GenerateAccessToken(user);
        var refresh = _jwt.GenerateRefreshToken();            // optional if you do app refresh
        _users.RemoveAllRefreshTokens(user);
        _users.AddRefreshToken(user, refresh);

        Response.Cookies.Append("refreshToken", refresh.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = refresh.Expires
        });

        return Ok(new
        {
            token = accessToken,
            roles = _users.GetActiveUserRoles(user.Id).Select(r => r.Name).ToList(),
            username = user.Username,
            homePage = _users.GetUserDefaultPage(user.Id).Path,
            isDomainUser = false,
            languageId = _users.GetSettings(user.Id)?.ID_language
        });
    }
}


public sealed class GoogleOAuthOptions
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string TokenEndpoint { get; set; } = "https://oauth2.googleapis.com/token";
}

public sealed class LoginRequestGoogle
{
    public string Code { get; set; } = default!;
}

public sealed class LoginResponse2
{
    public string Token { get; set; } = default!;
    public List<string> Roles { get; set; } = new();
    public string Username { get; set; } = default!;
    public string HomePage { get; set; } = "/";
    public bool IsDomainUser { get; set; }
    public int? LanguageId { get; set; }
}

public sealed class GoogleTokenResponse
{
    public string access_token { get; set; } = default!;
    public int expires_in { get; set; }
    public string scope { get; set; } = default!;
    public string token_type { get; set; } = default!;
    public string id_token { get; set; } = default!;
    public string? refresh_token { get; set; } // present on first consent / when prompt=consent
}
 