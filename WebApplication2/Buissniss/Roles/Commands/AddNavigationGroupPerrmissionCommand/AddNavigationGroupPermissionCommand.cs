using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.Roles.Commands.AddNavigationGroupPermissionCommand
{
    public class AddNavigationGroupPermissionCommandParametersDataFields
    {
        public int? RoleId { get; set; }
        public int? NavigationGroupId { get; set; }
    }

    [CoreSecurityAction(UserTableActions.ADD_NAVIGATION_GROUP_PERMISSION)]
    public class AddNavigationGroupPermissionCommand : ESignRequest<ActionRequestExtraParam<AddNavigationGroupPermissionCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
