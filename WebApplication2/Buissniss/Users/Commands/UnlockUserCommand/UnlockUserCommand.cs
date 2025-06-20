using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.UnlockUserCommand
{
    public class UnlockUserCommandParametersDataFields
    {
        [Required]
        public int Id { get; set; }
    }

    [CoreSecurityAction(UserTableActions.UNLOCK_USER)]
    public class UnlockUserCommand : ESignRequest<ActionRequestExtraParam<UnlockUserCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
