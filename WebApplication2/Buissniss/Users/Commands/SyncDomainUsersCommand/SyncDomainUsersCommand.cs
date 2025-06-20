using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.SyncDomainUsersCommand
{
    public class SyncDomainUsersCommandParametersDataFields
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Domain { get; set; }
    }

    [CoreSecurityAction(UserTableActions.SYNC_DOMAIN_USERS)]
    public class SyncDomainUsersCommand : ESignRequest<ActionRequestExtraParam<SyncDomainUsersCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
