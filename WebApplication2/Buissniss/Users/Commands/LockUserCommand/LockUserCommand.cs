using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.LockUserCommand
{
    public class LockUserCommandParametersDataFields
    {
        [Required]
        public int Id { get; set; }
    }

    [CoreSecurityAction(UserTableActions.LOCK_USER)]
    public class LockUserCommand : ESignRequest<ActionRequestExtraParam<LockUserCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
