using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.Request
{
    public class ActionRequestMultiple<TRecordParams>
    {
        [Required]
        public List<TRecordParams> RecordParams { get; set; }
        [Required]
        public string ActionCode { get; set; }
        [Required]
        public string RecordTypeCode { get; set; }
    }
}
