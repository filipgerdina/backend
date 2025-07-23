using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class PagePermissionsService
    {
        private readonly AppDbContext _db;

        public PagePermissionsService(AppDbContext db)
        {
            _db = db;
        }

        public PagePermissionClass GetPagePermission(int id)
        {
            return _db.PagePermissions.FirstOrDefault(p => p.Id == id);
        }

        public List<PagePermissionClass> GetPagePermissions()
        {
            return _db.PagePermissions.ToList();
        }

        public IEnumerable<PagePermissionClass> GetRolesPagePermissions(int roleId)
        {
            return _db.PagePermissions
                      .Where(p => p.ID_role == roleId)
                      .ToList();
        }

        public int AddPagePermission(PagePermissionClass pagePermission)
        {
            var exists = _db.PagePermissions
                            .Any(p => p.ID_role == pagePermission.ID_role &&
                                      p.ID_page == pagePermission.ID_page);
            if (exists)
                return -1;

            _db.PagePermissions.Add(pagePermission);
            _db.SaveChanges();

            return pagePermission.Id;
        }

        public int RemovePagePermission(int permissionId)
        {
            var pagePermission = _db.PagePermissions
                                    .FirstOrDefault(p => p.Id == permissionId);

            if (pagePermission == null)
                return -1;

            _db.PagePermissions.Remove(pagePermission);
            _db.SaveChanges();

            return pagePermission.Id;
        }
    }
}
