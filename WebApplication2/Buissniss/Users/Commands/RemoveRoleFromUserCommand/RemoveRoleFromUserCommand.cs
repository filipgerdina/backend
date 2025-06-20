using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.RemoveRoleFromUserCommand
{
    public class RemoveRoleFromUserCommandParametersDataFields
    {
        [Required]
        public int Id { get; set; }
    }

    [CoreSecurityAction(UserTableActions.REMOVE_ROLE_FROM_USER)]
    public class RemoveRoleFromUserCommand : ESignRequest<ActionRequestExtraParam<RemoveRoleFromUserCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
