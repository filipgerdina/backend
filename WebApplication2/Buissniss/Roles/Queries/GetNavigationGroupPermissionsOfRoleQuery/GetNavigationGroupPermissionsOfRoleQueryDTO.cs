using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupPermissionsOfRoleQuery
{
    public class GetNavigationGroupPermissionsOfRoleQueryDTO : CoreQueryEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
