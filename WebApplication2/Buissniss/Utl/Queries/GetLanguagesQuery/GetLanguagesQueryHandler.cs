using MediatR;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetLanguagesQuery
{
    public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, CoreListResponse<GetLanguagesQueryDTO>>
    {
        private readonly ApplicationSettingsService _applicationSettingsService;

        public GetLanguagesQueryHandler(ApplicationSettingsService applicationSettingsService)
        {
            _applicationSettingsService = applicationSettingsService;
        }


        public async Task<CoreListResponse<GetLanguagesQueryDTO>> Handle(GetLanguagesQuery request, CancellationToken cancellationT)
        {

            var result = new CoreListResponse<GetLanguagesQueryDTO>();

            result.Data = _applicationSettingsService.GetLanguages().Select(u => new GetLanguagesQueryDTO()
            {
                Id = u.Id,
                Value = u.Value,
                DisplayName = u.Display_Value
            }).ToList();
            return result;
        }
    }
}
