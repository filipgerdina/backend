using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ModuleService
    {
        public IEnumerable<ModuleClass> GetModules()
        {
            return new List<ModuleClass>
            {
                new() {
                    Id = 1,
                    PathToModue = "/userManagement/assets/remoteEntry.js",
                    ModuleName = "userManagement",
                },
                new() {
                    Id = 2,
                    PathToModue = "/rolesManagement/assets/remoteEntry.js",
                    ModuleName = "rolesManagement",
                },
                new() {
                    Id = 3,
                    PathToModue = "/permissionsManagement/assets/remoteEntry.js",
                    ModuleName = "permissionsManagement",
                },
            };
        }
    }
}
