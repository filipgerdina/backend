using WebApplication2.Models;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetModulesQuery
{
    public class GetModulesQueryDTO
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string PathToModule { get; set; }
    }
}
