using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.Roles.Commands.RemoveNavigationGroupPerrmissionCommand
{
    public class RemoveDataSourcePerrmissionCommandParametersDataFields
    {
    }

    [CoreSecurityAction(UserTableActions.REMOVE_NAVIGATION_GROUP_PERMISSION)]
    public class RemoveNavigationGroupPerrmissionCommand : ESignRequest<ActionRequestExtraParam<RemoveDataSourcePerrmissionCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
