using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.NewUserCommand
{
    public class NewUserCommandParametersDataFields
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    [CoreSecurityAction(UserTableActions.NEW_USER)]
    public class NewUserCommand : ESignRequest<ActionRequestExtraParam<NewUserCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
