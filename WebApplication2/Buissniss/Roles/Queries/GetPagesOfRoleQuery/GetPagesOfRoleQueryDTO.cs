using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetPagesOfRoleQuery
{
    public class GetPagesOfRoleQueryDTO : CoreQueryEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
