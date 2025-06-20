using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class HtmlViewerProperties
    {
        public int? Height { get; set; } = 300;
        public int? MinHeight { get; set; }
        public HtmlEditorPropertiesMention Mention { get; set; }
    }
}
