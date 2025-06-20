using System.Data;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Models;
using WebApplication2.Services;

public class UserService
{
    private readonly RoleService _roleService;
    private readonly PagesService _pagesService;
    private readonly ApplicationSettingsService _appSettingsService;

    private static List<UserClass> _users = new()
    {
        new UserClass { Id = 1, Username = "admin", PasswordHash = PasswordHelper.HashPassword("admin123"), IsSystem = true, Added = DateTime.UtcNow },
    };

    private static List<UserRole> _userRoles = new()
    {
        new UserRole { Id = 1, UserId = 1, RoleId = 1, Status = Status.Active }, 
        //new UserRole { RoleId = 2, UserId = 2, RoleId = 2, Status = Status.Active }  
    };

    public UserService(RoleService roleService, PagesService pagesService, ApplicationSettingsService appSettingsService)
    {
        _roleService = roleService;
        _pagesService = pagesService;
        _appSettingsService = appSettingsService;
    }

    public IEnumerable<UserClass> GetUsers() => _users;

    public UserClass GetUserById(int userId) => _users.Find(u => u.Id == userId);


    public IEnumerable<RoleClass> GetActiveUserRoles(int userId) => _userRoles.FindAll(ur => ur.UserId == userId && ur.Status == Status.Active).Select(ur => _roleService.GetRoleById(ur.RoleId));

    public IEnumerable<UserClass> GetUsersOfRole(int roleId) => _userRoles.FindAll(ur => ur.RoleId == roleId && ur.Status == Status.Active).Select(ur => GetUserById(ur.UserId));

    public PageClass GetUserDefaultPage(int userId) {
        var defaultPage = _pagesService.GetPages().ToList().Find(ps => 3 == ps.Id);
        var defaultPages = GetActiveUserRoles(userId).ToList().Select(role => _pagesService.GetPages().ToList().Find(ps => role.DefaultPageId == ps.Id)).ToList();

        if (defaultPages.Any(page => page != null))
        {
            defaultPage = defaultPages.Find(page => page != null);
        }

        return defaultPage;
    }


    public UserClass GetUserFromUserRole(int mappingId) {
        var userId = _userRoles.Find(ur => ur.Id == mappingId).UserId;
        return _users.Find(u => u.Id == userId);
    }

    public UserClass GetUserByUserRole(int userRoleId) => _userRoles.FindAll(ur => ur.Id == userRoleId).Select(ur => _users.Find(u => ur.UserId == u.Id)).First();

    public UserRole GetUserRoleMapping(int userId, int roleId)
    {
        return _userRoles.Find(ur => ur.UserId == userId && ur.RoleId == roleId);
    }

    public int LockUser(int userId) {
        var user = _users.Find(ur => ur.Id == userId);
        user.IsLocked = true;
        return 1;
    }

    public int UnlockUser(int userId)
    {
        var user = _users.Find(ur => ur.Id == userId);
        user.IsLocked = false;
        return 1;
    }

    public int AddUser(UserClassEdit user)
    {
        if (_users.Any(u => u.Username == user.Username))
            return -1;

        user.Id = _users.Last().Id + 1;

        UserClass addedUser;
        if (user is DomainUserClassEdit)
        {
            addedUser = new DomainUserClass
            {
                Username = user.Username,
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Domain = (user as DomainUserClassEdit).Domain,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
        else {
            addedUser = new UserClass
            {
                Username = user.Username,
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                PasswordHash = user.Password != null ? PasswordHelper.HashPassword(user.Password) : null,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

        }

        addedUser.Added = DateTime.UtcNow;
        _users.Add(addedUser);

        return user.Id;
    }

    public int EditUser(UserClassEdit user)
    {
        if (_users.Any(u => u.Username == user.Username && u.Id != user.Id))
            return -1;

        var editUser = _users.Find(u => u.Id == user.Id);

        if (user.Username != null)
            editUser.Username = user.Username;

        return user.Id;
    }

    public int EditUserProfile(UserClassEdit user)
    {
        var editUser = _users.Find(u => u.Username.Equals(user.Username));

        if (editUser == null)
            return -1;


        if (editUser.SettingsId != null || user.LanguageId != null || user.DateTimeFormatId != null || user.DecimalSeperatorId != null) {
            var settings = new EditApplicationSettingsClass() { Id = editUser.SettingsId, LanguageId = user.LanguageId, DateTimeFormatId = user.DateTimeFormatId, DecimalSeperatorId = user.DecimalSeperatorId };

            var settingsId = _appSettingsService.SetApplicationSettings(settings);
            if (settingsId == -1)
                return -1;

            editUser.SettingsId = settingsId;
        }

        editUser.FirstName = user.FirstName;
        editUser.LastName = user.LastName;
        editUser.Email = user.Email;
        return user.Id;
    }

    public int AddRoleToUser(UserClassEdit user)
    {

        if (user.RoleId == null)
            return -1;

        var userRole = _userRoles.Find(m => m.RoleId == user.RoleId && m.UserId == user.Id);

        if (userRole != null)
        {
            userRole.Status = Status.Active;
            return userRole.Id;
        }

        _userRoles.Add(
            new UserRole
            {
                Id = _userRoles.Last().Id + 1,
                UserId = user.Id,
                RoleId = (int)user.RoleId,
                Status = Status.Active
            }
        );
        return _userRoles.Last().Id;

    }

    public int RemoveRoleFromUser(int mappingId)
    {
        if (mappingId == null)
            return -1;

        var userRole = _userRoles.Find(m => m.Id == mappingId);

        if (userRole != null)
        {
            userRole.Status = Status.NonActive;
            return userRole.Id;
        }
        else
            return -1;
    }

    public ApplicationSettingsClass GetSettings(int userId) {
        var user = _users.Find(u => u.Id == userId);
        if (user == null || user.SettingsId == null)
            return _appSettingsService.GetApplicationSettings();
        else
            return _appSettingsService.GetAllSettings().Find(s => s.Id == user.SettingsId);
    }

    public UserClass? Authenticate(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        if (user == null) return null;
        return PasswordHelper.VerifyPassword(password, user.PasswordHash) ? user : null;
    }

    public bool IsDomain(string username) {
        var user = _users.FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()));

        if (user is DomainUserClass)
            return true;
        return false;
    }

    public void AddRefreshToken(UserClass user, RefreshToken token)
    {
        user.RefreshTokens.Add(token);
    }
    public void RemoveRefreshToken(UserClass user, string token)
    {
        user.RefreshTokens.RemoveAll(rt => rt.Token == token);
    }

    public void RemoveAllRefreshTokens(UserClass user)
    {
        user.RefreshTokens.RemoveAll(r => true);
    }
    public UserClass? GetUserByRefreshToken(string token)
    {
        return _users.FirstOrDefault(u => u.RefreshTokens.Any(rt => rt.Token == token && !rt.IsExpired));
    }

    public List<RoleClass> GetRolesForUser(UserClass user)
    {
        return _userRoles
            .Where(ur => ur.UserId == user.Id && ur.Status == Status.Active)
            .Select(ur => _roleService.GetRoleById(ur.RoleId))
            .ToList();
    }
}