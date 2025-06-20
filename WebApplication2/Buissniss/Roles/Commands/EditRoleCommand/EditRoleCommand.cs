using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.Roles.Commands.EditRoleCommand
{
    public class EditRoleCommandParametersDataFields
    {
        public int Id { get; set; }
        public int DefaultPageId { get; set; }
    }

    [CoreSecurityAction(UserTableActions.NEW_ROLE)]
    public class EditRoleCommand : ESignRequest<ActionRequestExtraParam<EditRoleCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
