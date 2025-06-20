using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetPagePermissionsOfRoleQuery
{
    public class GetPagePermissionsOfRoleQueryDTO : CoreQueryEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
