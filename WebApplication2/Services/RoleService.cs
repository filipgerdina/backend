using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class RoleService
    {
        private readonly PagePermissionsService _pagePermissionsService;


        public RoleService(PagePermissionsService pagePermissionsService)
        {
            _pagePermissionsService = pagePermissionsService;
        }

        private static List<RoleClass> _roles = new()
        {
            new RoleClass { Id = 1, Name = "Admin", DefaultPageId = 1 },
            new RoleClass { Id = 2, Name = "ADF-dev" },
            //new RoleClass { RoleId = 4, Name = "ADF-ogl" },

        };

        public IEnumerable<RoleClass> GetRoles() => _roles;

        public RoleClass? GetRoleById(int id) => _roles.FirstOrDefault(r => r.Id == id);
        public RoleClass? GetRoleByName(string name) => _roles.FirstOrDefault(r => r.Name == name);

        public int AddRole(RoleClassEdit role)
        {
            if (_roles.Any(u => u.Name == role.Name))
                return -1;

            role.Id = _roles.Last().Id + 1;

            RoleClass addedRole = new RoleClass()
            {
                Id = role.Id,
                Name = role.Name,
                DefaultPageId = role.DefaultPageId,
            };

            addedRole.Added = DateTime.UtcNow;
            _roles.Add(addedRole);

            if (addedRole.DefaultPageId != null) {
                var pagePermission = new PagePermissionClass()
                {
                    PageId = addedRole.DefaultPageId,
                    RoleId = role.Id
                };
                _pagePermissionsService.AddPagePermission(pagePermission);
            }

            return role.Id;
        }

        public int EditRole(RoleClassEdit role)
        {
            var editRole = _roles.Find(u => u.Id == role.Id);
            if (editRole == null)
                return -1;

            editRole.DefaultPageId = role.DefaultPageId;

            return editRole.Id;
        }
    }

}
