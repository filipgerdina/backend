namespace WebApplication2.Models
{
    public class ApplicationSettingsClass
    {
        public int Id { get; set; }
        public bool? IsSystem { get; set; }
        public int? LanguageId { get; set; }
        public int? DecimalSeperatorId { get; set; }
        public int? DateTimeFormatId { get; set; }
    }

    public class EditApplicationSettingsClass
    {
        public int? Id { get; set; }
        public int? LanguageId { get; set; }
        public int? DecimalSeperatorId { get; set; }
        public int? DateTimeFormatId { get; set; }
    }
    public class SettingClass
    { 
        public int Id { get; set; }
        public string Value { get; set; }
        public string DisplayValue { get; set; }
    }
}
