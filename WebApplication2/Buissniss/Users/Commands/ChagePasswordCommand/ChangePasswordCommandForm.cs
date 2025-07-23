using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Services;

namespace WebApplication2.Business.User.Commands.ChangePasswordCommand
{
    public static class ChangePasswordCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetChangePasswordForm(ActionFormQuery parameters, UserService userService, ApplicationSettingsService appSettingsService, IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken)
        {
            var oldPasswordLabel = "s:oldPassword";
            var newPasswordLabel = "s:newPassword";
            var confirmNewPasswordLabel = "s:confirmNewPassword";
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var usernameClaim = user.FindFirst(ClaimTypes.Name)?.Value;

            var userClass = userService.GetUsers().ToList().Find(u => u.Username == usernameClaim);
            var form = new ActionFormQueryDTO
            {
                Id = UserTableActions.EDIT_USER,
                Title = "s:editUser",
                Controls = new List<ActionFormControl>
                {
                    new ActionFormControl
                    {
                        DataField =
                            nameof(ChangePasswordCommandParametersDataFields.Id).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Visible = false,
                        Required = true,
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(ChangePasswordCommandParametersDataFields.OldPassword).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = oldPasswordLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(ChangePasswordCommandParametersDataFields.NewPassword).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = newPasswordLabel,
                        Required = true,
                        Description = (bool)appSettingsService.GetApplicationSettings().Use_Strong_Password
                            ? "s:strongPasswordDescription"
                            : null,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(ChangePasswordCommandParametersDataFields.NewPasswordConfirmed).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = confirmNewPasswordLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                },
                DefaultValues = new Dictionary<string, object>
                {
                    { nameof(ChangePasswordCommandParametersDataFields.Id).FirstCharToLowerCase(), userClass.Id }
                },
            };

            return form;
        }
    }
}
