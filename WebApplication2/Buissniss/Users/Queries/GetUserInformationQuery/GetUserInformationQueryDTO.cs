using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.User.Queries.GetUserInformationQuery
{
    public class GetUserInformationQueryDTO : CoreQueryEntityDTO
    {
        public string Username { get; set; }
        public string? DisplayName { get; set; }
        public string? DomainUserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Added { get; set; }
        public List<string> RoleNames { get; set; }
        public string DefaultPagePath { get; set; }
        public int? LanguageId { get; set; }
        public int? DateTimeFormatId { get; set; }
        public int? DecimalSeperatorId { get; set; }
    }
}
