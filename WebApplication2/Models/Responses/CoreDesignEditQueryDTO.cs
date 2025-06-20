using WebApplication2.Models.Responses;
namespace WebApplication2.Models.Responses
{
    public class CoreDesignEditQueryDTO : CoreQueryEntityDTO
    {
        public string ActionName { get; set; }
        public string BaseActionCode { get; set; }
    }
}
