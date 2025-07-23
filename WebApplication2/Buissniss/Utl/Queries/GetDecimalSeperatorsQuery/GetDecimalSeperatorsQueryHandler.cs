using MediatR;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetDecimalSeperatorsQuery
{
    public class GetDecimalSeperatorsQueryHandler : IRequestHandler<GetDecimalSeperatorsQuery, CoreListResponse<GetDecimalSeperatorsQueryDTO>>
    {
        private readonly ApplicationSettingsService _applicationSettingsService;

        public GetDecimalSeperatorsQueryHandler(ApplicationSettingsService applicationSettingsService)
        {
            _applicationSettingsService = applicationSettingsService;
        }


        public async Task<CoreListResponse<GetDecimalSeperatorsQueryDTO>> Handle(GetDecimalSeperatorsQuery request, CancellationToken cancellationT)
        {

            var result = new CoreListResponse<GetDecimalSeperatorsQueryDTO>();

            result.Data = _applicationSettingsService.GetDecimalSeparators().Select(u => new GetDecimalSeperatorsQueryDTO()
            {
                Id = u.Id,
                Value = u.Value,
                DisplayName = u.Display_Value
            }).ToList();
            return result;
        }
    }
}
