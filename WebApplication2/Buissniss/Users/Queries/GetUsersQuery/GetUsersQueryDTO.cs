using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.User.Queries.GetUsersQuery
{
    public class GetUsersQueryDTO: CoreQueryEntityDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }
        public bool IsSystem { get; set; }
        public bool IsLocked { get; set; }
        public DateTime Added { get; set; }
        public string? Domain { get; set; }
    }
}
