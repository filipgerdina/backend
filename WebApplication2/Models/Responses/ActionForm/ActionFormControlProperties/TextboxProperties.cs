using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class TextboxProperties
    {
        public string DefaultValueInfo { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public string Regex { get; set; }
        public string Unit { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? Disabled { get; set; }
        public ActionButtonProperties ActionButton { get; set; }
        public bool? Multiline { get; set; }
        public int? Height { get; set; }
        public bool TypingTriggersChange { get; set; }
    }
}
