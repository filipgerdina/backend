using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class RoleService
    {
        private readonly AppDbContext _db;
        private readonly PagePermissionsService _pagePermissionsService;

        public RoleService(AppDbContext db, PagePermissionsService pagePermissionsService)
        {
            _db = db;
            _pagePermissionsService = pagePermissionsService;
        }

        public IEnumerable<RoleClass> GetRoles() =>
            _db.Roles.ToList();

        public RoleClass? GetRoleById(int id) =>
            _db.Roles.FirstOrDefault(r => r.Id == id);

        public RoleClass? GetRoleByName(string name) =>
            _db.Roles.FirstOrDefault(r => r.Name == name);

        public int AddRole(RoleClassEdit role)
        {
            if (_db.Roles.Any(r => r.Name == role.Name))
                return -1;

            var newRole = new RoleClass
            {
                Name = role.Name,
                ID_home_page = role.DefaultPageId,
                Added = DateTime.UtcNow
            };

            _db.Roles.Add(newRole);
            _db.SaveChanges();

            if (newRole.ID_home_page != null)
            {
                var pagePermission = new PagePermissionClass
                {
                    ID_page = (int)newRole.ID_home_page,
                    ID_role = newRole.Id
                };
                _pagePermissionsService.AddPagePermission(pagePermission);
            }

            return newRole.Id;
        }

        public int EditRole(RoleClassEdit role)
        {
            var editRole = _db.Roles.FirstOrDefault(r => r.Id == role.Id);
            if (editRole == null)
                return -1;

            editRole.ID_home_page = role.DefaultPageId;
            _db.SaveChanges();

            return editRole.Id;
        }
    }
}
