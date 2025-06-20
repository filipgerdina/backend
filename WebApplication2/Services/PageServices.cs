using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class PagesService
    {
        private readonly ModuleService _moduleService;
        private readonly NavigationGroupService _navigationGroupService;

        public PagesService(ModuleService moduleService, NavigationGroupService navigationGroupService)
        {
            _moduleService = moduleService;
            _navigationGroupService = navigationGroupService;
        }

        public IEnumerable<PageClass> GetPages()
        {
            return new List<PageClass>
            {
                new PageClass
                {
                    Id = 1,
                    Path = "/userManagement/users",
                    PageComponent = "./UsersManagement",
                    Name = "s:usersManagement",
                    IconUrl = "usersManagement.svg",
                    Module = _moduleService.GetModules().ToList().Find(m => m.Id == 1),
                    NavigationGroup = _navigationGroupService.GetNavigationGroups().ToList().Find(ng => ng.Id == 2),
                },
                new PageClass
                {
                    Id = 2,
                    Path = "/rolesManagement/roles",
                    PageComponent = "./RolesManagement",
                    Name = "s:rolesManagement",
                    IconUrl = "rolesManagement.svg",
                    Module = _moduleService.GetModules().ToList().Find(m => m.Id == 2),
                    NavigationGroup = _navigationGroupService.GetNavigationGroups().ToList().Find(ng => ng.Id == 2),
                },
                new PageClass
                {
                    Id = 3,
                    Path = "/profile",
                    PageComponent = "./Profile",
                    Name = "s:profile",
                    IconUrl = "rolesManagement.svg",
                    Module = _moduleService.GetModules().ToList().Find(m => m.Id == 1),
                },
            };
        }
    }
}
