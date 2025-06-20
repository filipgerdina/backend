using System.Text.Json.Serialization;

namespace WebApplication2.Models.Request
{
    public class DynamicFiltersQuery
    {
        [JsonIgnore]
        public string FilterCode { get; set; }
    }
}
