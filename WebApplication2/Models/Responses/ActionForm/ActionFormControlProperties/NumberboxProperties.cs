using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class NumberboxProperties
    {
        public bool ValidateValue { get; set; } = true;
        public string DefaultValueInfo { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
        public string Unit { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? Disabled { get; set; }
        public int? DecimalPlaces { get; set; }
        public ActionButtonProperties ActionButton { get; set; }
        public bool TypingTriggersChange { get; set; }
    }
}
