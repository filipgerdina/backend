using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class CheckboxProperties
    {
        public string Unit { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? Disabled { get; set; }
        public ActionButtonProperties ActionButton { get; set; }
    }
}
