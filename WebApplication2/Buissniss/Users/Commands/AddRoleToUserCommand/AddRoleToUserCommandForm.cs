using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;
using WebApplication2.Services;

namespace WebApplication2.Business.User.Commands.AddRoleToUserCommand
{
    public static class AddRoleToUserCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetAddRoleToUserForm(ActionFormQuery parameters, UserService userService, RoleService roleService, CancellationToken cancellationToken)
        {
            var roleLabel = "s:role";

            var user = userService.GetUserByUserRole((int)parameters.RecordParams.First().RecordId);

            if (user == null && parameters.UserId != null)
                user = userService.GetUserById((int)parameters.UserId);

            if (user == null)
                throw new BaseException("User Not Found");

            var form = new ActionFormQueryDTO
            {
                Id = UserTableActions.ADD_ROLE_TO_USER,
                Title = "s:assignRoleToUser",
                Controls = new List<ActionFormControl>
                {
                    new ActionFormControl
                    {
                        DataField =
                            nameof(EditUserCommandParametersDataFields.Id).FirstCharToLowerCase(),
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Visible = false,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(AddRoleToUserCommandParametersDataFields.RoleId).FirstCharToLowerCase(),
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Label = roleLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Dropdown,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "name",
                            ValueDataField = "id",
                            Values = roleService.GetRoles().ToList().FindAll(r => !userService.GetActiveUserRoles(user.Id).Select(role => role.Id).Contains(r.Id)).Select(dd => (object)dd).ToList(),
                        },
                        
                    },
                },
                DefaultValues = new Dictionary<string, object>()
                {
                    {
                        nameof(EditUserCommandParametersDataFields.Id).FirstCharToLowerCase(),
                        user.Id
                    },
                },  
            };

            return form;
        }
    }
}
