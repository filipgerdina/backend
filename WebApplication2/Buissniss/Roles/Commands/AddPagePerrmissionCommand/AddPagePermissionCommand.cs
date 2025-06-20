using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.Roles.Commands.AddPagePermissionCommand
{
    public class AddPagePermissionCommandParametersDataFields
    {
        public int? RoleId { get; set; }
        public int? PageId { get; set; }
    }

    [CoreSecurityAction(UserTableActions.ADD_PAGE_PERMISSION)]
    public class AddPagePermissionCommand : ESignRequest<ActionRequestExtraParam<AddPagePermissionCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
