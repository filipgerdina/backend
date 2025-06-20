namespace WebApplication2.Models.Responses
{
    public class AvailableExtendedDataQueryDTO
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public string ValueTypeCode { get; set; }
        public string ValueDataTypeCode { get; set; }

        public string GroupKey { get; set; }
        public string GroupDisplayName { get; set; }
    }
}
