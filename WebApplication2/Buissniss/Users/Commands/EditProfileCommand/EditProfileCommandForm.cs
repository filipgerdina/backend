using MediatR;
using System.Security.Claims;
using WebApplication2.Business.User.Commands.AddRoleToUserCommand;
using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;
using WebApplication2.Services;

namespace WebApplication2.Business.User.Commands.EditProfileCommand
{
    public static class EditProfileCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetEditProfileCommandForm(UserService userService, RoleService roleService, PagesService pageService, ApplicationSettingsService appSettingsService, IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken)
        {

            var user = httpContextAccessor.HttpContext?.User;

            if (user == null/* || !user.Identity.IsAuthenticated*/)
            {
                return null;
            }

            var usernameClaim = user.FindFirst(ClaimTypes.Name)?.Value;

            var userClass = userService.GetUsers().ToList().Find(u => u.Username == usernameClaim);

            var usernameField = nameof(EditProfileCommandParametersDataFields.Username).FirstCharToLowerCase();
            var domainUsernameField = nameof(EditProfileCommandParametersDataFields.DomainUsername).FirstCharToLowerCase();
            var emailField = nameof(EditProfileCommandParametersDataFields.Email).FirstCharToLowerCase();
            var firstNameField = nameof(EditProfileCommandParametersDataFields.FirstName).FirstCharToLowerCase();
            var lastNameField = nameof(EditProfileCommandParametersDataFields.LastName).FirstCharToLowerCase();
            var defaultPageIdField = nameof(EditProfileCommandParametersDataFields.DefaultPageId).FirstCharToLowerCase();
            var languageIdField = nameof(EditProfileCommandParametersDataFields.LanguageId).FirstCharToLowerCase();
            var dateTimeFormatIdField = nameof(EditProfileCommandParametersDataFields.DateTimeFormatId).FirstCharToLowerCase();
            var decimalSeperatorIdField = nameof(EditProfileCommandParametersDataFields.DecimalSeperatorId).FirstCharToLowerCase();

            var usernameLabel = "s:username";
            var domainUsernameLabel = "s:domainUsername";
            var emailLabel = "s:email";
            var firstNameLabel = "s:firstName";
            var lastNameLabel = "s:lastName";
            var defaultPageIdLabel = "s:defaultPage";
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
                        DataField = usernameField,
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = usernameLabel,
                        Required = true,
                        Properties = new TextboxProperties
                        {
                            ReadOnly = true
                        },
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField = domainUsernameField,
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = domainUsernameLabel,
                        Required = true,
                        Properties = new TextboxProperties
                        {
                            ReadOnly = true
                        },
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField = emailField,
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = emailLabel,
                        Properties = new TextboxProperties
                        {
                            Regex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"
                        },
                        Required = false,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField = firstNameField,
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = firstNameLabel,
                        Required = false,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField = lastNameField,
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = lastNameLabel,
                        Required = false,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField = defaultPageIdField,
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Label = defaultPageIdLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Dropdown,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "name",
                            ValueDataField = "id",
                            Values = pageService.GetPages().ToList().Select(dd => (object)dd).ToList(),
                            Disabled = true,
                        },

                    },
                    new ActionFormControl
                    {
                        DataField = languageIdField,
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Label = languageIdLabel,
                        Required = false,
                        VisualizationType = VisualizationType.Dropdown,
                        Description = appSettingsService.GetLanguages().Find(l => l.Id == appSettingsService.GetApplicationSettings().LanguageId).DisplayValue,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "displayValue",
                            ValueDataField = "id",
                            Values = appSettingsService.GetLanguages().ToList().Select(dd => (object)dd).ToList(),
                        },

                    },
                    new ActionFormControl
                    {
                        DataField = dateTimeFormatIdField,
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Label = dateTimeFormatIdLabel,
                        Required = false,
                        VisualizationType = VisualizationType.Dropdown,
                        Description = appSettingsService.GetDateTimeFormats().Find(l => l.Id == appSettingsService.GetApplicationSettings().DateTimeFormatId).DisplayValue,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "displayValue",
                            ValueDataField = "id",
                            Values = appSettingsService.GetDateTimeFormats().ToList().Select(dd => (object)dd).ToList(),
                        },

                    },
                    new ActionFormControl
                    {
                        DataField = decimalSeperatorIdField,
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Label = decimalSeperatorIdLabel,
                        Required = false,
                        VisualizationType = VisualizationType.Dropdown,
                        Description = appSettingsService.GetDecimalSeperators().Find(l => l.Id == appSettingsService.GetApplicationSettings().DecimalSeperatorId).DisplayValue,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "displayValue",
                            ValueDataField = "id",
                            Values = appSettingsService.GetDecimalSeperators().ToList().Select(dd => (object)dd).ToList(),
                        },

                    },
                },
                DefaultValues = new Dictionary<string, object>()
                {
                    {
                        usernameField,
                        userClass.Username ?? null
                    },
                    {
                        domainUsernameField,
                        (userClass is DomainUserClass) ? ((DomainUserClass)userClass).Domain + "/" + userClass.Username : null
                    },
                    {
                        emailField,
                        userClass.Email ?? null
                    },
                    {
                        firstNameField,
                        userClass.FirstName ?? null
                    },
                    {
                        lastNameField,
                        userClass.LastName ?? null
                    },
                    {
                        defaultPageIdField,
                        userService.GetUserDefaultPage(userClass.Id).Id
                    },
                    {
                        languageIdField,
                        appSettingsService.GetAllSettings().Find(a => a.Id == userClass.SettingsId)?.LanguageId ?? null
                    },
                    {
                        dateTimeFormatIdField,
                        appSettingsService.GetAllSettings().Find(a => a.Id == userClass.SettingsId)?.DateTimeFormatId ?? null
                    },
                    {
                        decimalSeperatorIdField,
                        appSettingsService.GetAllSettings().Find(a => a.Id == userClass.SettingsId)?.DecimalSeperatorId ?? null
                    },
                },
            };

            return form;
        }
    }
}
