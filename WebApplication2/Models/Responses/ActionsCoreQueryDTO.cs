namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// Basic actions data
    /// </summary>
    public class ActionsCoreQueryDTO
    {
        public int RecordTypeId { get; set; }
        public string RecordTypeName { get; set; }
        public string RecordTypeCode { get; set; }
        public string RecordTypeDescription { get; set; }

        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string ActionCode { get; set; }
        public string ActionDescription { get; set; }
        public string BaseActionCode { get; set; }
        public string ImageUrl { get; set; }
        public string ImageColor { get; set; }
    }
}
