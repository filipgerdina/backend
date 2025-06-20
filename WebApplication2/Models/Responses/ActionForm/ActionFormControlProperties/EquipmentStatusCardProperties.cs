namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class EquipmentStatusCardProperties
    {
        public bool IsMultiple { get; set; }
        public bool HasLinkForUrl { get; set; } = true;
        public bool LoadPanelOnBody { get; set; }
        public bool Visible { get; set; } = true;
        public string EquipmentCode { get; set; }
    }
}
