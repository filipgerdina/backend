using System.ComponentModel.DataAnnotations;
using WebApplication2.Models.Request;

namespace WebApplication2.Models.Request
{
    public class ActionRequest : QueryDataById<int>
    {
        public string CurrentStateCode { get; set; }
        [Required]
        public string ActionCode { get; set; }
        [Required]
        public string RecordTypeCode { get; set; }
    }

    public class ActionRequestLong : QueryDataById<long>
    {
        public string CurrentStateCode { get; set; }
        [Required]
        public string ActionCode { get; set; }
        [Required]
        public string RecordTypeCode { get; set; }
    }
}
