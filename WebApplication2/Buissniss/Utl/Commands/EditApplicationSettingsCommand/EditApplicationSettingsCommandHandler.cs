using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using System.Security.Cryptography;
using WebApplication2.Services;
using WebApplication2.Models;

namespace WebApplication2.Business.Utl.Commands.EditApplicationSettingsCommand
{
    public class EditApplicationSettingsCommandHandler : IRequestHandler<EditApplicationSettingsCommand, RecordIDResponse>
    {
        private readonly ApplicationSettingsService applicationSettingsService;

        public EditApplicationSettingsCommandHandler(ApplicationSettingsService applicationSettingsService)
        {
            this.applicationSettingsService = applicationSettingsService;
        }

        public async Task<RecordIDResponse> Handle(EditApplicationSettingsCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            var appSettings = new EditApplicationSettingsClass
            {
                Id = applicationSettingsService.GetApplicationSettings().Id,
                UseStrongPassword = request.Data.ExtraParamsFormValues.UseStrongPassword,
                LanguageId = request.Data.ExtraParamsFormValues.LanguageId,
                DateTimeFormatId = request.Data.ExtraParamsFormValues.DateTimeFormatId,
                DecimalSeperatorId = request.Data.ExtraParamsFormValues.DecimalSeperatorId,
            };

            response.SetId(applicationSettingsService.SetApplicationSettings(appSettings));
            return response;
        }
    }
}
