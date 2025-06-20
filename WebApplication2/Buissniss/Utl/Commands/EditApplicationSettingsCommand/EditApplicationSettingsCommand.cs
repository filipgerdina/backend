using System.ComponentModel.DataAnnotations;
using WebApplication2.Buissniss.Security;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses;

namespace WebApplication2.Business.Utl.Commands.EditApplicationSettingsCommand
{
    public class EditApplicationSettingsCommandParametersDataFields
    {
        public int LanguageId { get; set; }
        public int DateTimeFormatId { get; set; }
        public int DecimalSeperatorId { get; set; }
    }

    public class EditApplicationSettingsCommand : ESignRequest<ActionRequestExtraParam<EditApplicationSettingsCommandParametersDataFields>, RecordIDResponse>
    {

    }
}
