namespace WebApplication2.Models.Request
{
    public class ActionsQuery
    {
        public string RecordTypeCode { get; set; }
        public int? RecordId { get; set; }
        public string CurrentStateCode { get; set; }
        public bool AllActions { get; set; } = true;
    }

    public class ActionsQueryLong
    {
        public string RecordTypeCode { get; set; }
        public long? RecordId { get; set; }
        public string CurrentStateCode { get; set; }
        public bool AllActions { get; set; } = true;
    }
}
