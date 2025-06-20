using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class NavigationGroupService
    {
        public IEnumerable<NavigationGroupClass> GetNavigationGroups()
        {
            return new List<NavigationGroupClass>
            {
                new NavigationGroupClass
                {
                    Id = 1,
                    Name = "s:applicationManagement",
                    IconUrl = "applicationManagement.svg",
                },
                new NavigationGroupClass
                {
                    Id = 2,
                    ParentGroupId = 1,
                    Name = "s:usersAndRoles",
                    IconUrl = "usersAndRoles.svg",
                },
                new NavigationGroupClass
                {
                    Id = 3,
                    ParentGroupId = 1,
                    Name = "s:usersAndRoles",
                    IconUrl = "usersAndRoles.svg",
                },
            };
        }
    }
}
