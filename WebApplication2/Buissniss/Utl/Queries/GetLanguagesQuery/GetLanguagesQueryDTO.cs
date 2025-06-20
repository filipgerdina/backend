using WebApplication2.Models;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetLanguagesQuery
{
    public class GetLanguagesQueryDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
    }
}
