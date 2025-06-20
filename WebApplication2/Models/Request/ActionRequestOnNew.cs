using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.Request
{
    public class ActionRequestOnNew
    {
        [Required]
        public string ActionCode { get; set; }
        [Required]
        public string RecordTypeCode { get; set; }
    }
}
