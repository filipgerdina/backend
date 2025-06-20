using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetNavigationGroupsOfRoleQuery
{
    public class GetNavigationGroupsOfRoleQueryDTO : CoreQueryEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
