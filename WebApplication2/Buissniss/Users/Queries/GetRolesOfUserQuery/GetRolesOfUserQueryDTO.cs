using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery
{
    public class GetRolesOfUserQueryDTO : CoreQueryEntityDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string? DefaultPage { get; set; }
    }
}
