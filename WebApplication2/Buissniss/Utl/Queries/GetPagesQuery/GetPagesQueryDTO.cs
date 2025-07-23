using WebApplication2.Models;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetPagesQuery
{
    public class GetPagesQueryDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string PageComponent { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public int? ModuleId { get; set; }
        public int? NavigationGroupId { get; set; }
    }
}
