using MediatR;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetApplicationSettingsQuery
{
    public class GetApplicationSettingsQueryHandler : IRequestHandler<GetApplicationSettingsQuery, CoreResponse<GetApplicationSettingsQueryDTO>>
    {
        private readonly ApplicationSettingsService _applicationSettingsService;

        public GetApplicationSettingsQueryHandler(ApplicationSettingsService applicationSettingsService)
        {
            _applicationSettingsService = applicationSettingsService;
        }

        public async Task<CoreResponse<GetApplicationSettingsQueryDTO>> Handle(GetApplicationSettingsQuery request, CancellationToken cancellationT)
        {

            var result = new CoreResponse<GetApplicationSettingsQueryDTO>();

            var appSettings = _applicationSettingsService.GetApplicationSettings();
            result.Data = new GetApplicationSettingsQueryDTO()
            {
                Language = _applicationSettingsService.GetLanguages().Select(l => new SettingsDTO { Id=l.Id, DisplayValue=l.Display_Value, Value=l.Value}).ToList().Find(u => u.Id == appSettings.ID_language),
                DateFormat = _applicationSettingsService.GetDateTimeFormats().Select(df => new SettingsDTO { Id = df.Id, DisplayValue = df.Display_Value, Value = df.Value }).ToList().Find(u => u.Id == appSettings.ID_date_time_format),
                DecimalSeperator = _applicationSettingsService.GetDecimalSeparators().Select(ds => new SettingsDTO { Id = ds.Id, DisplayValue = ds.Display_Value, Value = ds.Value }).ToList().Find(u => u.Id == appSettings.ID_separator),
            };
            return result;
        }
    }
}
