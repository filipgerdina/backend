using System;

namespace WebApplication2.Models.Responses
{
    public class NotificationNotifyRequestDetailsDTO
    {
        public Guid TransactionGuid { get; set; }
        public int? Count { get; set; }
        public int? NumberOfRecords { get; set; }
        public string ProcessName { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public bool Finished { get; set; }
        public bool Refresh { get; set; } = true;
    }
}
