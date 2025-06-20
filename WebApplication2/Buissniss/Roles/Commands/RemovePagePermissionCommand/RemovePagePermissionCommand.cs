using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.Roles.Commands.RemovePagePerrmissionCommand
{
    public class RemovePagePerrmissionCommandParametersDataFields
    {
    }

    [CoreSecurityAction(UserTableActions.REMOVE_PAGE_PERMISSION)]
    public class RemovePagePerrmissionCommand : ESignRequest<ActionRequestExtraParam<RemovePagePerrmissionCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
