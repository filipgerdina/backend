using System;
using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class DropdownMultipleProperties
    {
        [Obsolete]
        public int? Min { get; set; }
        [Obsolete]
        public int? Max { get; set; }
        public string Unit { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? Disabled { get; set; }
        public ActionButtonProperties ActionButton { get; set; }
        public int? Height { get; set; }
        public string GroupingDataField { get; set; }
        public string ValueDataField { get; set; }
        public string DisplayDataField { get; set; }
        public string ParentValueDataField { get; set; }
        public string IconColorDataField { get; set; }
        public string IconUrlDataField { get; set; }
        public string IconDescriptionDataField { get; set; }
        public string IsDisabledDataField { get; set; }
        public bool DontSelectChildrenWithParent { get; set; }
        public bool ConfirmSelection { get; set; }
        public List<object> Values { get; set; }
        public bool Expanded { get; set; }
        public DefaultValueProperties DefaultValue { get; set; }
        public TableProperties TableProperties { get; set; }
        public int? MaxDisplayed { get; set; }
    }
}
