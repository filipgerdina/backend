using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.EditUserCommand
{
    public class EditUserCommandParametersDataFields
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public List<int>? RolesIds { get; set; }
    }

    [CoreSecurityAction(UserTableActions.EDIT_USER)]
    public class EditUserCommand : ESignRequest<ActionRequestExtraParam<EditUserCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
