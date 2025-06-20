using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Api;
using WebApplication2.Services;
using System.DirectoryServices.AccountManagement;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : CoreControllerBase
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthController(UserService userService, RoleService roleService, JwtTokenGenerator tokenGenerator, IMediator mediator)
            : base(mediator)
        {
            _userService = userService;
            _roleService = roleService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetUsers()
                .FirstOrDefault(u => u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase));

            if (user == null || user.IsLocked)
                return Unauthorized(new { message = "Invalid username or password" });

            if (_userService.IsDomain(request.Username))
            {
                var domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                var authenticated = await LdapDomainAuthenticator.AuthenticateAsync(request.Username, request.Password, domain);
                if (!authenticated)
                    return Unauthorized(new { message = "Invalid username or password" });
            }
            else
            {
                if (_userService.Authenticate(request.Username, request.Password) == null)
                    return Unauthorized(new { message = "Invalid username or password" });
            }

            var accessToken = _tokenGenerator.GenerateAccessToken(user);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            // Store hashed token in DB and invalidate previous tokens
            _userService.RemoveAllRefreshTokens(user); // ensure single-use
            _userService.AddRefreshToken(user, refreshToken);

            Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = refreshToken.Expires
            });

            return Ok(new LoginResponse
            {
                Token = accessToken,
                Roles = _userService.GetActiveUserRoles(user.Id).Select(r => r.Name).ToList(),
                Username = user.Username,
                HomePage = _userService.GetUserDefaultPage(user.Id).Path,
                LanguageId = _userService.GetSettings(user.Id).LanguageId
            });
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public ActionResult<RefreshTokenResponse> Refresh()
        {
            var oldToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(oldToken))
                return Unauthorized(new { message = "Missing refresh token" });

            var user = _userService.GetUserByRefreshToken(oldToken);
            if (user == null)
                return Unauthorized(new { message = "Invalid or expired refresh token" });

            // Invalidate the old token (rotate)
            _userService.RemoveRefreshToken(user, oldToken);

            var newAccessToken = _tokenGenerator.GenerateAccessToken(user);
            var newRefreshToken = _tokenGenerator.GenerateRefreshToken();
            _userService.AddRefreshToken(user, newRefreshToken);

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = newRefreshToken.Expires
            });

            return Ok(new LoginResponse
            {
                Token = newAccessToken,
                Roles = _userService.GetActiveUserRoles(user.Id).Select(r => r.Name).ToList(),
                Username = user.Username,
                HomePage = _userService.GetUserDefaultPage(user.Id).Path,
                LanguageId = _userService.GetSettings(user.Id).LanguageId
            });
        }

        [HttpPost("logout")]
        public ActionResult<ApiResponse> Logout()
        {
            var token = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Invalid token" });

            var user = _userService.GetUserByRefreshToken(token);
            if (user != null)
                _userService.RemoveRefreshToken(user, token);

            Response.Cookies.Append("refreshToken", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            return Ok(new ApiResponse { Message = "Logged out" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }
        public string Username { get; set; }
        public string HomePage { get; set; }
        public int? LanguageId { get; set; }
    }

    public class SyncUsersRequest
    { 
        public List<int> RoleIds { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Domain { get; set; }
    }

    public class SyncUsersResponse
    {
        public List<SyncedUsers> users { get; set; }
    }

    public class SyncedUsers
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string? Mail { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
    public class RefreshTokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }
    }
    public class ApiResponse
    {
        public string Message { get; set; }
    }
}
