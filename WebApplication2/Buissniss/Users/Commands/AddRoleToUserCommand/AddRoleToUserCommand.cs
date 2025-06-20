using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.AddRoleToUserCommand
{
    public class AddRoleToUserCommandParametersDataFields
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }

    [CoreSecurityAction(UserTableActions.ADD_ROLE_TO_USER)]
    public class AddRoleToUserCommand : ESignRequest<ActionRequestExtraParam<AddRoleToUserCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
