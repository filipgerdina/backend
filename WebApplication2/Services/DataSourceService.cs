using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class DataSourceService
    {

        public DataSourceService()
        {
        }

        private List<DataSourceClass> dataSources = new List<DataSourceClass>
            {
                // UtlController
                new() {
                    Id = 1,
                    Name = "pages",
                    Method = "GET",
                    Path = "/utl/pages",
                },
                new() {
                    Id = 2,
                    Name = "translations",
                    Method = "GET",
                    Path = "/utl/translations",
                },
                new() {
                    Id = 3,
                    Name = "navigationGroups",
                    Method = "GET",
                    Path = "/utl/navigationGroups",
                },
                new() {
                    Id = 4,
                    Name = "applicationSettings",
                    Method = "GET",
                    Path = "/utl/applicationSettings",
                },
                new() {
                    Id = 5,
                    Name = "languages",
                    Method = "GET",
                    Path = "/utl/languages",
                },
                new() {
                    Id = 6,
                    Name = "dateTimeFormats",
                    Method = "GET",
                    Path = "/utl/dateTimeFormats",
                },
                new() {
                    Id = 7,
                    Name = "decimalSeperators",
                    Method = "GET",
                    Path = "/utl/decimalSeperators",
                },

                // UsersController
                new() {
                    Id = 8,
                    Name = "users",
                    Method = "GET",
                    Path = "/users/users",
                },
                new() {
                    Id = 9,
                    Name = "userRoles",
                    Method = "GET",
                    Path = "/users/userRoles",
                },
                new() {
                    Id = 10,
                    Name = "userActions",
                    Method = "GET",
                    Path = "/users/actions",
                },
                new() {
                    Id = 11,
                    Name = "userActionForm",
                    Method = "POST",
                    Path = "/users/forms",
                },

                // RolesController
                new() {
                    Id = 12,
                    Name = "roles",
                    Method = "GET",
                    Path = "/roles/roles",
                },
                new() {
                    Id = 13,
                    Name = "pagePermissions",
                    Method = "GET",
                    Path = "/roles/pagePermissions",
                },
                new() {
                    Id = 14,
                    Name = "rolesActions",
                    Method = "GET",
                    Path = "/roles/actions",
                },
                new() {
                    Id = 15,
                    Name = "rolesActionForm",
                    Method = "POST",
                    Path = "/roles/forms",
                },
                new() {
                    Id = 1,
                    Name = "dataSources",
                    Method = "GET",
                    Path = "/utl/dataSources",
                },
                new() {
                    Id = 13,
                    Name = "navigationGroupPermissions",
                    Method = "GET",
                    Path = "/roles/navigationGroupPermissions",
                },
            };
        public List<DataSourceClass> GetDataSources()
        {
            return dataSources;
        }
    }
}
