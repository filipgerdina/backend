namespace WebApplication2.Models
{
    public class ApplicationSettingsClass
    {
        public int Id { get; set; }
        public bool? System { get; set; }
        public bool? Use_Strong_Password { get; set; }
        public int? ID_language { get; set; }
        public int? ID_separator { get; set; }
        public int? ID_date_time_format { get; set; }
        public LanguageSetting? Language { get; set; }
        public DateTimeFormatSetting? DateTimeFormat { get; set; }
        public DecimalSeparatorSetting? DecimalSeparator { get; set; }
    }

    public class EditApplicationSettingsClass
    {
        public int? Id { get; set; }
        public bool? UseStrongPassword { get; set; }
        public int? LanguageId { get; set; }
        public int? DecimalSeperatorId { get; set; }
        public int? DateTimeFormatId { get; set; }
    }
    public class SettingClass
    { 
        public int Id { get; set; }
        public string Value { get; set; }
        public string Display_Value { get; set; }
    }

    public class LanguageSetting : SettingClass { }

    public class DateTimeFormatSetting : SettingClass { }

    public class DecimalSeparatorSetting : SettingClass { }
}
