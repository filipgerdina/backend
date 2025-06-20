using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.User.Commands.EditProfileCommand
{
    public class EditProfileCommandParametersDataFields
    {
        public string Username { get; set; }
        public string? DomainUsername { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DefaultPageId { get; set; }
        public int? LanguageId { get; set; }
        public int? DateTimeFormatId { get; set; }
        public int? DecimalSeperatorId { get; set; }
    }

    [CoreSecurityAction(UserTableActions.EDIT_PROFILE)]
    public class EditProfileCommand : ESignRequest<ActionRequestExtraParam<EditProfileCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
