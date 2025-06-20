using System;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class DateboxProperties
    {
        public DateTime? Min { get; set; }
        public DateTime? Max { get; set; }
        public string Unit { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? Disabled { get; set; }
        public ActionButtonProperties ActionButton { get; set; }
        public bool? IsStartTime { get; set; }
        public bool? IsEndTime { get; set; }
        public bool? OnlyDate { get; set; }
        public DateBoxPropertiesDefaultValue DefaultValue { get; set; }
    }

    public class DateBoxPropertiesDefaultValue
    {
        public bool? UseTimeOffset { get; set; }
        public bool? DateStartsAtMidnight { get; set; }
        public int? DaysFromNow { get; set; }
        public int? HoursFromNow { get; set; }
        public int? MinutesFromNow { get; set; }
        public int? SecondsFromNow { get; set; }
        public string QueryString { get; set; }
    }
}
