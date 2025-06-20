using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;
using WebApplication2.Services;

namespace WebApplication2.Business.Roles.Commands.EditRoleCommand
{
    public static class EditRoleCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetEditRoleForm(ActionFormQuery parameters, RoleService roleService, PagesService pagesService, PagePermissionsService pagePermissionsService,  CancellationToken cancellationToken)
        {
            var roleLabel = "Role";
            var role = roleService.GetRoleById((int)parameters.RecordParams.First().RecordId);

            var form = new ActionFormQueryDTO
            {
                Id = UserTableActions.EDIT_ROLE,
                Title = "s:editRole",
                Controls = new List<ActionFormControl>
                {
                    new ActionFormControl
                    {
                        DataField =
                            nameof(EditRoleCommandParametersDataFields.Id).FirstCharToLowerCase(),
                        ValueDataType = "NUMBER",
                        ValueType = "SIMPLE",
                        Required = true,
                        Visible = false,
                        VisualizationType = VisualizationType.Textbox,
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(EditRoleCommandParametersDataFields.DefaultPageId).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Required = false,
                        Label = "s:defaultPage",
                        VisualizationType = VisualizationType.Dropdown,
                        Properties = new DropdownProperties
                        {
                            DisplayDataField = "name",
                            ValueDataField = "id",
                            Values = pagePermissionsService.GetRolesPagePermissions(role.Id).Select(pp => pagesService.GetPages().ToList().Find(p => p.Id == pp.PageId)).Select(dd => (object)dd).ToList(),
                        },
                    }
                },
                DefaultValues = new Dictionary<string, object>()
                {
                    {
                        nameof(EditRoleCommandParametersDataFields.Id).FirstCharToLowerCase(),
                        role.Id
                    },
                    {
                        nameof(EditRoleCommandParametersDataFields.DefaultPageId).FirstCharToLowerCase(),
                        role.DefaultPageId
                    },
                },
            };

            return form;
        }
    }
}
