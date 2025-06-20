using MediatR;
using WebApplication2.Business.User.Commands.EditUserCommand;
using WebApplication2.Business.User.Commands.NewUserCommand;
using WebApplication2.Extensions;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;
using WebApplication2.Services;

namespace WebApplication2.Business.User.Commands.SyncDomainUsersCommand
{
    public static class SyncDomainUsersCommandForm
    {
        public static async Task<ActionFormQueryDTO> GetSyncDomainUsersForm(UserService userService, RoleService roleService, CancellationToken cancellationToken)
        {
            var usernameLabel = "s:username";
            var passwordLabel = "s:password";
            var domainLabel = "s:domain";

            var domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

            var form = new ActionFormQueryDTO
            {
                Id = UserTableActions.SYNC_DOMAIN_USERS,
                Title = "s:syncDomainUsers",
                Controls = new List<ActionFormControl>
                {
                    new ActionFormControl
                    {
                        DataField =
                            nameof(SyncDomainUsersCommandParametersDataFields.Username).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = usernameLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(SyncDomainUsersCommandParametersDataFields.Password).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = passwordLabel,
                        Required = true,
                        VisualizationType = VisualizationType.Textbox
                    },
                    new ActionFormControl
                    {
                        DataField =
                            nameof(SyncDomainUsersCommandParametersDataFields.Domain).FirstCharToLowerCase(),
                        ValueDataType = "STRING",
                        ValueType = "SIMPLE",
                        Label = domainLabel,
                        Required = false,
                        VisualizationType = VisualizationType.Textbox
                    },
                },
                DefaultValues = new Dictionary<string, object>()
                {
                    {
                        nameof(SyncDomainUsersCommandParametersDataFields.Domain).FirstCharToLowerCase(),
                        domain
                    }
                },
            };

            return form;
        }
    }
}
