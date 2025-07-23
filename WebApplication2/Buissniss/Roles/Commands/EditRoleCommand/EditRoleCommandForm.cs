using MediatR;
using System.Linq;
using WebApplication2.Buissniss.Roles.Queries.GetPagesOfRoleQuery;
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
        public static async Task<ActionFormQueryDTO> GetEditRoleForm(ActionFormQuery parameters, RoleService roleService, PagesService pagesService, PagePermissionsService pagePermissionsService, NavigationGroupService navigationGroupService, NavigationGroupPermissionsService navigationGroupPermissionsService,  CancellationToken cancellationToken)
        {
            var roleLabel = "Role";
            var role = roleService.GetRoleById((int)parameters.RecordParams.First().RecordId);
            var navGroupIds = new List<int>();

            var mainNavGroups = navigationGroupPermissionsService.GetRolesNavigationGroupPermissions((int)parameters.RecordParams.First().RecordId);

            foreach (var item in mainNavGroups)
            {
                navGroupIds.Add((int)item.ID_navigation_group);
                navGroupIds = GetNavigationGroupsChildren((int)item.ID_navigation_group, navGroupIds, navigationGroupService);
            }
            var pageIds = pagePermissionsService.GetRolesPagePermissions((int)parameters.RecordParams.First().RecordId).Select(pp => pp.ID_page);

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
                            Values = pagesService.GetPages().ToList().FindAll(p => pageIds.Contains(p.Id) || p.ID_group != null && navGroupIds.Contains((int)p.ID_group)).Select(dd => (object)new { name = dd.Name, id = dd.Id }).ToList(),
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
                        role.ID_home_page
                    },
                },
            };

            return form;
        }

        private static List<int> GetNavigationGroupsChildren(int navGroupId, List<int> list, NavigationGroupService _navigationGroupService)
        {
            foreach (var item in _navigationGroupService.GetNavigationGroups())
            {
                if (item.ID_parent_group == navGroupId)
                {
                    list.Add(item.Id);
                    list = GetNavigationGroupsChildren(item.Id, list, _navigationGroupService);
                }
            }
            return list;
        }
    }
}
