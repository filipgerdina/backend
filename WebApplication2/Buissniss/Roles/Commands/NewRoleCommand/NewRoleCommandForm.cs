using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;
using WebApplication2.Services;

namespace WebApplication2.Business.Roles.Commands.AddRoleCommand
{
    public static class NewRoleCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetNewRoleForm(ActionFormQuery parameters, PagesService pagesService,  CancellationToken cancellationToken)
        {
            var roleLabel = "Role";

            var form = new ActionFormQueryDTO
            {
                Id = UserTableActions.NEW_ROLE,
                Title = "s:newRole",
                Controls = new List<ActionFormControl>
                {
                    new ActionFormControl
                    {
                        DataField =
                            nameof(NewRoleCommandParametersDataFields.Name).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Required = true,
                        Label = "s:name",
                        VisualizationType = VisualizationType.Textbox,
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(NewRoleCommandParametersDataFields.DefaultPageId).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Required = false,
                        Label = "s:defaultPage",
                        VisualizationType = VisualizationType.Dropdown,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "name",
                            ValueDataField = "id",
                            Values = pagesService.GetPages().Select(dd => (object)dd).ToList(),
                        },
                    }
                },  
            };

            return form;
        }
    }
}
