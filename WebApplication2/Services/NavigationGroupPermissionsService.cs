using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class NavigationGroupPermissionsService
    {
        private readonly AppDbContext _db;
        private readonly UserService _userService;

        public NavigationGroupPermissionsService(AppDbContext db, UserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public NavigationGroupPermissionsClass GetNavigationGroupPermission(int id)
        {
            return _db.NavigationGroupPermissions.FirstOrDefault(p => p.Id == id);
        }

        public List<NavigationGroupPermissionsClass> GetNavigationGroupPermissions()
        {
            return _db.NavigationGroupPermissions.ToList();
        }

        public IEnumerable<NavigationGroupPermissionsClass> GetRolesNavigationGroupPermissions(int roleId)
        {
            return _db.NavigationGroupPermissions.Where(p => p.ID_role == roleId).ToList();
        }

        public int AddNavigationGroupPermission(NavigationGroupPermissionsClass navigationGroupPermission)
        {
            var exists = _db.NavigationGroupPermissions
                .Any(p => p.ID_role == navigationGroupPermission.ID_role &&
                          p.ID_navigation_group == navigationGroupPermission.ID_navigation_group);

            if (exists)
                return -1;

            _db.NavigationGroupPermissions.Add(navigationGroupPermission);
            _db.SaveChanges();

            return navigationGroupPermission.Id;
        }

        public int RemoveNavigationGroupPermission(int permissionId)
        {
            var permission = _db.NavigationGroupPermissions.FirstOrDefault(p => p.Id == permissionId);
            if (permission == null)
                return -1;

            _db.NavigationGroupPermissions.Remove(permission);
            _db.SaveChanges();

            return permission.Id;
        }
    }
}
