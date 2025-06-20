using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.Request
{
    public class ActionRequestOnNewExtraParam<TExtraParam>
    {
        [Required]
        public string ActionCode { get; set; }
        [Required]
        public string RecordTypeCode { get; set; }
        [Required]
        public TExtraParam ExtraParamsFormValues { get; set; }
    }
}
