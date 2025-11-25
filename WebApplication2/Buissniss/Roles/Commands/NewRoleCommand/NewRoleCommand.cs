using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.Roles.Commands.AddRoleCommand
{
    public class NewRoleCommandParametersDataFields
    {
        public string Name { get; set; }
        public int? DefaultPageId { get; set; }
    }

    [CoreSecurityAction(UserTableActions.NEW_ROLE)]
    public class NewRoleCommand : ESignRequest<NewRoleCommandParametersDataFields, RecordIDResponse>
    {

    }
}
