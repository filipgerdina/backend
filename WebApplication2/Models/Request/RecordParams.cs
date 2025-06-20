using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.Request
{
    public class RecordParams
    {
        [Required]
        public int? RecordId { get; set; }
        public string CurrentStateCode { get; set; }
    }

    public class RecordParamsLong
    {
        [Required]
        public long RecordId { get; set; }
        public string CurrentStateCode { get; set; }
    }
}
