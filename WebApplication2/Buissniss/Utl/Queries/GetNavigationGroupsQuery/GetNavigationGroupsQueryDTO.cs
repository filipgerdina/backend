using WebApplication2.Models;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetNavigationGroupsQuery
{
    public class GetNavigationGroupsQueryDTO
    {
        public int Id { get; set; }
        public int? ParentGroupId { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }
}
