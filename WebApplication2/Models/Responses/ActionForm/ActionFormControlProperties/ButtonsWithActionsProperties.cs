using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class ButtonsWithActionsProperties
    {
        public List<ButtonGroup> ButtonGroups { get; set; }

        public bool DisableIfNoPermissions { get; set; } // not yet implemented on front-end
    }

    public class ButtonGroup
    {
        public Actions Actions { get; set; }

        public string EventNameForRetrievingActions { get; set; }

        public string EventNameForExecutingActions { get; set; }

        public string EventNameForActionSuccess { get; set; }

        public string EventNameForActionError { get; set; }
    }

    public class Actions
    {
        public string AppModuleCode { get; set; }
        public string RecordTypeCode { get; set; }
        public List<string> BaseActionCodesToFilterOut { get; set; }
        public List<string> ActionCodesToFilterOut { get; set; }
        public List<string> BaseActionCodesToShow { get; set; }
        public List<string> ActionCodesToShow { get; set; }
    }
}
