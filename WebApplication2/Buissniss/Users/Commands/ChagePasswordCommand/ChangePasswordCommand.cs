using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.ChangePasswordCommand
{
    public class ChangePasswordCommandParametersDataFields
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string NewPasswordConfirmed { get; set; }
    }

    [CoreSecurityAction(UserTableActions.CHANGE_PASSWORD)]
    public class ChangePasswordCommand : ESignRequest<ActionRequestExtraParam<ChangePasswordCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
