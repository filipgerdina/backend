namespace WebApplication2.Models.Responses.ActionForm.ActionFormEventProperties
{
#pragma warning disable SA1300 // MX code list
    public enum ActionFormEventPropertiesActionPropertiesFillTextType
    {
        append,
        replace,
    }
#pragma warning restore SA1300 // MX code list

    public class ActionFormEventPropertiesActionProperties
    {
        public string FromPath { get; set; }
        public ActionFormEventPropertiesActionPropertiesFillTextType? FillTextType { get; set; }
    }

    public class ActionFormEventActionProperties
    {
        public string Trigger { get; set; }
        public ActionFormEventPropertiesActionProperties ActionProperties { get; set; }
    }
}
