using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class PagePermissionsService
    {

        private List<PagePermissionClass> pagePermissions = new List<PagePermissionClass>() {
            new() {
                Id = 1,
                RoleId = 1,
                PageId = 1,
            },
            new() {
                Id = 2,
                RoleId = 1,
                PageId = 2,
            },
            new() {
                Id = 3,
                RoleId = 1,
                PageId = 3,
            },
            new() {
                Id = 4,
                RoleId = 2,
                PageId = 3,
            }
        };
        public PagePermissionClass GetPagePermission(int id)
        {
            return pagePermissions.Find(p => p.Id == id);
        }

        public List<PagePermissionClass> GetPagePermissions()
        {
            return pagePermissions;
        }

        public IEnumerable<PagePermissionClass> GetRolesPagePermissions(int roleId) { 
            return pagePermissions.FindAll(p => p.RoleId == roleId);
        }

        public int AddPagePermission(PagePermissionClass pagePermission)
        {
            if (pagePermissions.Any(p => p.RoleId == pagePermission.RoleId && p.PageId == pagePermission.PageId))
                return -1;
            pagePermission.Id = pagePermissions.Last().Id + 1;
            pagePermissions.Add(pagePermission);
            return (int)pagePermission.Id;
        }

        public int RemovePagePermission(int permissionId) {
            var pagePermission = GetPagePermission(permissionId);

            if (pagePermission == null)
                return -1;

            pagePermissions.Remove(pagePermission);
            return (int)pagePermission.Id;
        }
    }
}
