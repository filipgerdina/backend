using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Roles.Queries.GetRolesQuery
{
    public class GetRolesQueryDTO : CoreQueryEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? DefaultPage { get; set; }
        public int? DefaultPageId { get; set; }
    }
}
