using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class ApplicationSettingsService
    {
        private readonly AppDbContext _db;

        public ApplicationSettingsService(AppDbContext db)
        {
            _db = db;
        }

        public ApplicationSettingsClass GetApplicationSettings()
        {
            return _db.ApplicationSettings
                .Include(s => s.Language)
                .Include(s => s.DateTimeFormat)
                .Include(s => s.DecimalSeparator)
                .FirstOrDefault(appS => appS.System == true);
        }

        public List<ApplicationSettingsClass> GetAllSettings()
        {
            return _db.ApplicationSettings
                .Include(s => s.Language)
                .Include(s => s.DateTimeFormat)
                .Include(s => s.DecimalSeparator)
                .ToList();
        }

        public int SetApplicationSettings(EditApplicationSettingsClass settings)
        {
            // Validate existence of related settings
            if (settings.LanguageId != null &&
                !_db.Languages.Any(l => l.Id == settings.LanguageId))
                return -1;

            if (settings.DateTimeFormatId != null &&
                !_db.DateTimeFormats.Any(f => f.Id == settings.DateTimeFormatId))
                return -1;

            if (settings.DecimalSeperatorId != null &&
                !_db.DecimalSeparators.Any(d => d.Id == settings.DecimalSeperatorId))
                return -1;

            // If ID provided and matches a system config, edit it
            var editSettings = settings.Id != null
                ? _db.ApplicationSettings.FirstOrDefault(a => a.Id == settings.Id)
                : null;

            if (editSettings != null && editSettings.System == true)
            {
                if (settings.LanguageId == null || settings.DateTimeFormatId == null || settings.DecimalSeperatorId == null)
                    return -1;
            }

            if (editSettings == null)
            {
                editSettings = new ApplicationSettingsClass
                {
                    System = false,
                    Use_Strong_Password = settings.UseStrongPassword,
                    ID_language = settings.LanguageId,
                    ID_date_time_format = settings.DateTimeFormatId,
                    ID_separator = settings.DecimalSeperatorId
                };
                _db.ApplicationSettings.Add(editSettings);
            }
            else
            {
                editSettings.Use_Strong_Password = settings.UseStrongPassword;
                editSettings.ID_language = settings.LanguageId;
                editSettings.ID_date_time_format = settings.DateTimeFormatId;
                editSettings.ID_separator = settings.DecimalSeperatorId;
            }

            _db.SaveChanges();
            return editSettings.Id;
        }

        public List<LanguageSetting> GetLanguages()
        {
            return _db.Languages.ToList();
        }

        public List<DateTimeFormatSetting> GetDateTimeFormats()
        {
            return _db.DateTimeFormats.ToList();
        }

        public List<DecimalSeparatorSetting> GetDecimalSeparators()
        {
            return _db.DecimalSeparators.ToList();
        }
    }
}
