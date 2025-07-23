using MediatR;
using System.Security.Claims;
using WebApplication2.Buissniss.Utl.Queries.GetApplicationSettingsQuery;
using WebApplication2.Business.User.Commands.AddRoleToUserCommand;
using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;
using WebApplication2.Services;

namespace WebApplication2.Business.Utl.Commands.EditApplicationSettingsCommand
{
    public static class EditApplicationSettingsCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetEditApplicationSettingsForm(ApplicationSettingsService appSettingsService, CancellationToken cancellationToken)
        {
            var useStrongPasswordField = nameof(EditApplicationSettingsCommandParametersDataFields.UseStrongPassword).FirstCharToLowerCase();
            var languageIdField = nameof(EditApplicationSettingsCommandParametersDataFields.LanguageId).FirstCharToLowerCase();
            var dateTimeFormatIdField = nameof(EditApplicationSettingsCommandParametersDataFields.DateTimeFormatId).FirstCharToLowerCase();
            var decimalSeperatorIdField = nameof(EditApplicationSettingsCommandParametersDataFields.DecimalSeperatorId).FirstCharToLowerCase();

            var useStrongPasswordLabel = "s:useStrongPassword";
            var languageIdLabel = "s:language";
            var dateTimeFormatIdLabel = "s:dateTimeFormat";
            var decimalSeperatorIdLabel = "s:decimalSeperator";

            var form = new ActionFormQueryDTO
            {
                Id = UserTableActions.EDIT_PROFILE,
                Title = "",
                Controls = new List<ActionFormControl>
                {
                    new ActionFormControl
                    {
                        DataField = useStrongPasswordField,
                        ValueDataType = "BOOLEAN",
                        ValueType = "SIMPLE",
                        Label = useStrongPasswordLabel,
                        Required = false,
                        VisualizationType = VisualizationType.Checkbox,

                    },
                    new ActionFormControl
                    {
                        DataField = languageIdField,
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Label = languageIdLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Dropdown,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "displayValue",
                            ValueDataField = "id",
                            Values = appSettingsService.GetLanguages()
                                .Select(l => new { Id = l.Id, DisplayValue = l.Display_Value, Value = l.Value })
                                .Cast<object>()
                                .ToList(),

                        },

                    },
                    new ActionFormControl
                    {
                        DataField = dateTimeFormatIdField,
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Label = dateTimeFormatIdLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Dropdown,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "displayValue",
                            ValueDataField = "id",
                            Values = appSettingsService.GetDateTimeFormats()
                                .Select(l => new { Id = l.Id, DisplayValue = l.Display_Value, Value = l.Value })
                                .Cast<object>()
                                .ToList(),
                        },

                    },
                    new ActionFormControl
                    {
                        DataField = decimalSeperatorIdField,
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Label = decimalSeperatorIdLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Dropdown,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "displayValue",
                            ValueDataField = "id",
                            Values = appSettingsService.GetDecimalSeparators()
                                .Select(l => new { Id = l.Id, DisplayValue = l.Display_Value, Value = l.Value })
                                .Cast<object>()
                                .ToList(),
                        },

                    },
                },
                DefaultValues = new Dictionary<string, object>()
                {
                    {
                        useStrongPasswordField,
                        appSettingsService.GetApplicationSettings().Use_Strong_Password
                    },
                    {
                        languageIdField,
                        appSettingsService.GetApplicationSettings()?.ID_language ?? null
                    },
                    {
                        dateTimeFormatIdField,
                        appSettingsService.GetApplicationSettings()?.ID_date_time_format ?? null
                    },
                    {
                        decimalSeperatorIdField,
                        appSettingsService.GetApplicationSettings()?.ID_separator ?? null
                    },
                },
            };

            return form;
        }
    }
}
