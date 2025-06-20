using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class NavigationGroupPermissionsService
    {
        private readonly UserService _userService;
        //private readonly NavigationGroupService _navigationGroupService;

        public NavigationGroupPermissionsService(UserService userService)
        {
            _userService = userService;
            //_navigationGroupService = navigationGroupService;
        }

        private List<NavigationGroupPermissionsClass> navigationGroupPermissions = new List<NavigationGroupPermissionsClass>() {
            new() {
                Id = 1,
                RoleId = 1,
                NavigationGroupId = 1,
            }
        };
        public NavigationGroupPermissionsClass GetNavigationGroupPermission(int id)
        {
            return navigationGroupPermissions.Find(p => p.Id == id);
        }

        public List<NavigationGroupPermissionsClass> GetNavigationGroupPermissions()
        {
            return navigationGroupPermissions;
        }

        public IEnumerable<NavigationGroupPermissionsClass> GetRolesNavigationGroupPermissions(int roleId) { 
            return navigationGroupPermissions.FindAll(p => p.RoleId == roleId);
        }

        public int AddNavigationGroupPermission(NavigationGroupPermissionsClass navigationGroupPermission)
        {
            if (this.navigationGroupPermissions.Any(p => p.RoleId == navigationGroupPermission.RoleId && p.NavigationGroupId == navigationGroupPermission.NavigationGroupId))
                return -1;
            navigationGroupPermission.Id = this.navigationGroupPermissions.Last().Id + 1;
            navigationGroupPermissions.Add(navigationGroupPermission);
            return (int)navigationGroupPermission.Id;
        }

        public int RemoveNavigationGroupPermission(int permissionId) {
            var navigationGroupPermission = GetNavigationGroupPermission(permissionId);

            if (navigationGroupPermission == null)
                return -1;

            navigationGroupPermissions.Remove(navigationGroupPermission);
            return (int)navigationGroupPermission.Id;
        }
    }
}
