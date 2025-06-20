using WebApplication2.Models;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.Utl.Queries.GetApplicationSettingsQuery
{
    public class GetApplicationSettingsQueryDTO
    {
        public SettingsDTO Language { get; set; }
        public SettingsDTO DateFormat { get; set; }
        public SettingsDTO DecimalSeperator { get; set; }
    }

    public class SettingsDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string DisplayValue { get; set; }
    }
}
