using MediatR;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetDateTimeFormatsQuery
{
    public class GetDateTimeFormatsQueryHandler : IRequestHandler<GetDateTimeFormatsQuery, CoreListResponse<GetDateTimeFormatsQueryDTO>>
    {
        private readonly ApplicationSettingsService _applicationSettingsService;

        public GetDateTimeFormatsQueryHandler(ApplicationSettingsService applicationSettingsService)
        {
            _applicationSettingsService = applicationSettingsService;
        }


        public async Task<CoreListResponse<GetDateTimeFormatsQueryDTO>> Handle(GetDateTimeFormatsQuery request, CancellationToken cancellationT)
        {

            var result = new CoreListResponse<GetDateTimeFormatsQueryDTO>();

            result.Data = _applicationSettingsService.GetDateTimeFormats().Select(u => new GetDateTimeFormatsQueryDTO()
            {
                Id = u.Id,
                Value = u.Value,
                DisplayName = u.DisplayValue
            }).ToList();
            return result;
        }
    }
}
