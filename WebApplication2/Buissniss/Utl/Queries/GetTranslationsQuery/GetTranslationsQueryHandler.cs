using MediatR;
using WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Models;
using WebApplication2.Models.Responses;
using WebApplication2.Services;

namespace WebApplication2.Buissniss.Utl.Queries.GetTranslationsQuery
{
    public class GetTranslationsQueryHandler : IRequestHandler<GetTranslationsQuery, CoreListResponse<GetTranslationsQueryDTO>>
    {
        private readonly TranslationService _tranlsationService;

        public GetTranslationsQueryHandler(TranslationService tranlsationService)
        {
            _tranlsationService = tranlsationService;
        }

        public async Task<CoreListResponse<GetTranslationsQueryDTO>> Handle(GetTranslationsQuery request, CancellationToken cancellationT)
        {

            var result = new CoreListResponse<GetTranslationsQueryDTO>();

            var langId = request.langId ?? 1;
            result.Data = _tranlsationService.GetTranslations(langId).Select(u => new GetTranslationsQueryDTO()
            {
                Id = u.Id,
                Key = u.Key,
                Value = u.Value
            }).ToList();
            return result;
        }
    }
}
