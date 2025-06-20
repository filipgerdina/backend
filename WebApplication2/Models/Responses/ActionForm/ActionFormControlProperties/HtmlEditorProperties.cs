using System.Collections.Generic;
using System.IO;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class HtmlEditorProperties
    {
        public bool? ReadOnly { get; set; }
        public int? Height { get; set; } = 300;
        public int? MinHeight { get; set; }
        public HtmlEditorPropertiesMention Mention { get; set; }
    }

    public class HtmlEditorPropertiesMention
    {
        public string DisplayExpr { get; set; }
        public string ValueExpr { get; set; }
        public string Marker { get; set; }
        public object[] DataFromValues { get; set; }
        public string EventName { get; set; }
    }
}
