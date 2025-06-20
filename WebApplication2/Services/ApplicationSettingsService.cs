using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ApplicationSettingsService
    {
        public ApplicationSettingsService()
        {
        }

        private List<SettingClass> _languageList = new List<SettingClass> {
            new() { Id = 1, Value = "en", DisplayValue="s:english"},
            new() { Id = 2, Value = "slo", DisplayValue="s:slovenian"},
        };

        private List<SettingClass> _dateTimeFormatList = new List<SettingClass> {
            new() { Id = 1, Value = "dd/mm/yyyy", DisplayValue="dd/mm/yyyy" },
            new() { Id = 2, Value = "mm/dd/yyyy", DisplayValue="mm/dd/yyyy"  },
        };

        private List<SettingClass> _decimalSeperatorList = new List<SettingClass> {
            new() { Id = 1, Value = "." , DisplayValue = "s:dot"},
            new() { Id = 2, Value = "," , DisplayValue = "s:comma"},
        };

        private List<ApplicationSettingsClass> applicationSettings = new List<ApplicationSettingsClass>
        {
            new() {
                Id = 1,
                IsSystem = true,
                LanguageId = 1,
                DecimalSeperatorId = 1,
                DateTimeFormatId = 1,
            }
        };

        public ApplicationSettingsClass GetApplicationSettings()
        {
            return applicationSettings.Find(appS => appS.IsSystem == true);
        }

        public List<ApplicationSettingsClass> GetAllSettings()
        {
            return applicationSettings;
        }

        public int SetApplicationSettings(EditApplicationSettingsClass settings) {

            if (settings.Id != null && applicationSettings.Find(a => a.Id == settings.Id).IsSystem == true)
            {
                if (settings.LanguageId == null || settings.DateTimeFormatId == null || settings.DecimalSeperatorId == null)
                {
                    return -1;
                }
            }

            if(settings.LanguageId != null)
            {
                if (!_languageList.Select(l => l.Id).ToList().Contains((int)settings.LanguageId))
                {
                    return -1;
                }
            }
            if (settings.DateTimeFormatId != null)
            {
                if (!_dateTimeFormatList.Select(l => l.Id).ToList().Contains((int)settings.DateTimeFormatId))
                {
                    return -1;
                }
            }
            if (settings.DecimalSeperatorId != null)
            {
                if (!_decimalSeperatorList.Select(l => l.Id).ToList().Contains((int)settings.DecimalSeperatorId))
                {
                    return -1;
                }
            }
            var editSettings = applicationSettings.Find(appS => appS.Id == settings.Id);

            if (editSettings == null) {
                editSettings = new ApplicationSettingsClass { Id = applicationSettings.Last().Id + 1 };
                applicationSettings.Add(editSettings);
            }

            editSettings.LanguageId = settings.LanguageId;
            editSettings.DecimalSeperatorId = settings.DecimalSeperatorId;
            editSettings.DateTimeFormatId = settings.DateTimeFormatId;

            return editSettings.Id;
        }

        public List<SettingClass> GetLanguages() {
            return _languageList;
        }

        public List<SettingClass> GetDateTimeFormats() {
            return _dateTimeFormatList;
        }

        public List<SettingClass> GetDecimalSeperators() {
            return _decimalSeperatorList;
        }
    }
}
