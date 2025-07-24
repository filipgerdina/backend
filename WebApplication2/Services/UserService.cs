using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WebApplication2.Models;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Services;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly RoleService _roleService;
    private readonly PagesService _pagesService;
    private readonly ApplicationSettingsService _appSettingsService;
    private readonly AppDbContext _db;

    private static readonly Regex StrongPasswordRegex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public UserService(AppDbContext db, RoleService roleService, PagesService pagesService, ApplicationSettingsService appSettingsService)
    {
        _db = db;
        _roleService = roleService;
        _pagesService = pagesService;
        _appSettingsService = appSettingsService;
    }

    private static List<RefreshToken> refreshTokens = new List<RefreshToken>();
    public IEnumerable<UserClass> GetUsers() => _db.Users.ToList();

    public UserClass GetUserById(int userId) => _db.Users.FirstOrDefault(u => u.Id == userId);

    public IEnumerable<RoleClass> GetActiveUserRoles(int userId) =>
        _db.UserRoles.Where(ur => ur.ID_user == userId)
                     .Select(ur => _roleService.GetRoleById(ur.ID_role));

    public IEnumerable<UserClass> GetUsersOfRole(int roleId) =>
        _db.UserRoles.Where(ur => ur.ID_role == roleId)
                     .Select(ur => GetUserById(ur.ID_user));

    public PageClass GetUserDefaultPage(int userId)
    {
        var defaultPage = _pagesService.GetPages().ToList().Find(ps => 3 == ps.Id);
        var defaultPages = GetActiveUserRoles(userId).ToList()
            .Select(role => _pagesService.GetPages().ToList().Find(ps => role.ID_home_page == ps.Id)).ToList();

        if (defaultPages.Any(page => page != null))
            defaultPage = defaultPages.Find(page => page != null);

        return defaultPage;
    }

    public UserRole GetUserRoleMapping(int userId, int roleId) =>
        _db.UserRoles.FirstOrDefault(ur => ur.ID_user == userId && ur.ID_role == roleId);

    public int ChangePassword(int userId, string password)
    {
        var user = GetUserById(userId);
        if (user == null) return -1;

        if (_appSettingsService.GetApplicationSettings().Use_Strong_Password is true)
        {
            if (!StrongPasswordRegex.IsMatch(password))
                throw new BaseException(UserApplicationMessages.UNMEET_PASSWORD_REQUIREMENTS);
        }

        user.Password = PasswordHelper.HashPassword(password);
        _db.SaveChanges();
        return user.Id;
    }

    public int LockUser(int userId)
    {
        var user = GetUserById(userId);
        if (user == null) return -1;
        user.Is_Locked = true;
        _db.SaveChanges();
        return 1;
    }

    public int UnlockUser(int userId)
    {
        var user = GetUserById(userId);
        if (user == null) return -1;
        user.Is_Locked = false;
        _db.SaveChanges();
        return 1;
    }

    public int AddUser(UserClassEdit user)
    {
        if (_db.Users.Any(u => u.Username == user.Username)) return -1;

        var newUser = new UserClass
        {
            Username = user.Username,
            Email = user.Email,
            Display_Name = user.DisplayName,
            Password = user.Password != null ? PasswordHelper.HashPassword(user.Password) : null,
            First_Name = user.FirstName,
            Last_Name = user.LastName,
            Added = DateTime.UtcNow,
            Domain = user.Domain
        };

        _db.Users.Add(newUser);
        _db.SaveChanges();
        return newUser.Id;
    }

    public int EditUser(UserClassEdit user)
    {
        if (_db.Users.Any(u => u.Username == user.Username && u.Id != user.Id)) return -1;
        var editUser = GetUserById(user.Id);
        if (editUser == null) return -1;

        editUser.Username = user.Username ?? editUser.Username;
        _db.SaveChanges();
        return user.Id;
    }

    public int EditUserProfile(UserClassEdit user)
    {
        var editUser = _db.Users.FirstOrDefault(u => u.Username == user.Username);
        if (editUser == null) return -1;

        if (editUser.ID_Setting != null || user.LanguageId != null || user.DateTimeFormatId != null || user.DecimalSeperatorId != null)
        {
            var settings = new EditApplicationSettingsClass { Id = editUser.ID_Setting, LanguageId = user.LanguageId, DateTimeFormatId = user.DateTimeFormatId, DecimalSeperatorId = user.DecimalSeperatorId };
            var settingsId = _appSettingsService.SetApplicationSettings(settings);
            if (settingsId == -1) return -1;
            editUser.ID_Setting = settingsId;
        }

        editUser.First_Name = user.FirstName;
        editUser.Last_Name = user.LastName;
        editUser.Email = user.Email;
        _db.SaveChanges();
        return user.Id;
    }

    public int AddRoleToUser(UserClassEdit user)
    {
        if (user.RoleId == null) return -1;

        var exists = _db.UserRoles.Any(m => m.ID_role == user.RoleId && m.ID_user == user.Id);
        if (exists) return -1;

        var newUserRole = new UserRole
        {
            ID_user = user.Id,
            ID_role = user.RoleId.Value
        };

        _db.UserRoles.Add(newUserRole);
        _db.SaveChanges();
        return newUserRole.Id;
    }

    public int RemoveRoleFromUser(int mappingId)
    {
        var userRole = _db.UserRoles.FirstOrDefault(m => m.Id == mappingId);
        if (userRole == null) return -1;
        _db.UserRoles.Remove(userRole);
        _db.SaveChanges();
        return userRole.Id;
    }

    public ApplicationSettingsClass GetSettings(int userId)
    {
        var user = GetUserById(userId);
        return (user == null || user.ID_Setting == null)
            ? _appSettingsService.GetApplicationSettings()
            : _appSettingsService.GetAllSettings().Find(s => s.Id == user.ID_Setting);
    }

    public UserClass? Authenticate(string username, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == username);
        return user == null ? null : (PasswordHelper.VerifyPassword(password, user.Password) ? user : null);
    }

    public bool IsDomain(string username)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        return user != null ? user.Domain != null : false;
    }

    public UserClass? GetUserByRefreshToken(string token)
    {
        return _db.Users
            .AsEnumerable()
            .FirstOrDefault(u => refreshTokens.Any(rt => rt.Token == token && !rt.IsExpired && rt.UserId == u.Id));
    }


    public void AddRefreshToken(UserClass user, RefreshToken token)
    {
        refreshTokens.Add(new RefreshToken
        {
            Token = token.Token,
            Expires = token.Expires,
            UserId = user.Id
        });
    }

    public void RemoveRefreshToken(UserClass user, string token)
    {
        refreshTokens.RemoveAll(rt => rt.Token == token);
    }

    public void RemoveAllRefreshTokens(UserClass user)
    {
        refreshTokens.RemoveAll(rt => rt.UserId == user.Id);
    }

    public List<RoleClass> GetRolesForUser(UserClass user)
    {
        return _db.UserRoles.Where(ur => ur.ID_user == user.Id)
                            .Select(ur => _roleService.GetRoleById(ur.ID_role))
                            .ToList();
    }

    public UserClass GetUserByUserRole(int userRoleId)
    {
        var userRole = _db.UserRoles.FirstOrDefault(ur => ur.Id == userRoleId);
        if (userRole == null) return null;

        return _db.Users.FirstOrDefault(u => u.Id == userRole.ID_user);
    }

}
