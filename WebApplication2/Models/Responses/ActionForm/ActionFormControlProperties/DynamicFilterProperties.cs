using System;
using System.Collections.Generic;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class DynamicFilterProperties
    {
        public string JsonEndpoint { get; set; }
        public string ElementCssClass { get; set; } = "col-md-4 col-sm-6 col-xs-12";
        public string ElementTimeSelectionCssClass { get; set; } = "col-xs-12";
        public int MaxHeight { get; set; }
        public bool TypingTriggersChange { get; set; } = true;
        public bool ShowRefreshButton { get; set; } = false;
        public bool ColorRefreshButtonOnInit { get; set; } = true;
        public DynamicFilterPropertiesStyle Style { get; set; }
    }

    public class DynamicFilterPropertiesStyle
    {
        public int PaddingTop { get; set; }
        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }
        public int PaddingBottom { get; set; }
        public int MarginTop { get; set; }
        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginBottom { get; set; }
    }
}
