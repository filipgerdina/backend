using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.Request
{
    public class ActionRequestExtraParamMultiple<TRecordParams, TExtraParam>
    {
        [Required]
        public List<TRecordParams> RecordParams { get; set; }
        [Required]
        public string ActionCode { get; set; }
        [Required]
        public string RecordTypeCode { get; set; }
        public TExtraParam ExtraParamsFormValues { get; set; }
    }
}
