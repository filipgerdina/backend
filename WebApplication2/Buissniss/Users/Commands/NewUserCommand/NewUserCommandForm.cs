using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;
using WebApplication2.Services;

namespace WebApplication2.Business.User.Commands.NewUserCommand
{
    public static class NewUserCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetNewUserForm(UserService userService, RoleService roleService, CancellationToken cancellationToken)
        {
            var usernameLabel = "s:username";
            var passwordLabel = "s:password";
            var form = new ActionFormQueryDTO
            {
                Id = UserTableActions.NEW_USER,
                Title = "s:addUser",
                Controls = new List<ActionFormControl>
                {
                    new ActionFormControl
                    {
                        DataField =
                            nameof(NewUserCommandParametersDataFields.Username).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = usernameLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(NewUserCommandParametersDataFields.Password).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = passwordLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                },
            };

            return form;
        }
    }
}
