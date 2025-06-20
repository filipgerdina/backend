namespace WebApplication2.Services
{
    public class UserRoleHelperService
    {

        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public UserRoleHelperService(UserService userService, RoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
    }
}
