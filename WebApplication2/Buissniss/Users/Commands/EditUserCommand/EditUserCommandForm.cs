using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;

namespace WebApplication2.Business.User.Commands.EditUserCommand
{
    public static class EditUserCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetEditUserForm(ActionFormQuery parameters, UserService userService, CancellationToken cancellationToken)
        {
            var usernameLabel = "s:username";
            var passwordLabel = "s:password";

            var user = userService.GetUsers().ToList().Find(user => user.Id == parameters.RecordParams.First().RecordId);
            var form = new ActionFormQueryDTO
            {
                Id = UserTableActions.EDIT_USER,
                Title = "s:editUser",
                Controls = new List<ActionFormControl>
                {
                    new ActionFormControl
                    {
                        DataField =
                            nameof(EditUserCommandParametersDataFields.Id).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = passwordLabel,
                        Visible = false,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(EditUserCommandParametersDataFields.Username).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = usernameLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                },
                DefaultValues = new Dictionary<string, object>()
                {
                    {
                        nameof(EditUserCommandParametersDataFields.Id).FirstCharToLowerCase(),
                        user.Id
                    },
                                        {
                        nameof(EditUserCommandParametersDataFields.Username).FirstCharToLowerCase(),
                        user.Username
                    },
                },
            };

            return form;
        }
    }
}
