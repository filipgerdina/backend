using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormEventProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormEventProperties
{
    public class ActionFormEventPropertiesUrlParameters
    {
        public ActionFormEventPropertiesUrlParametersType? Type { get; set; }
        public string Name { get; set; }
        public string FromPath { get; set; }
    }

    public class ActionFormEventApiCallProperties
    {
        public string Url { get; set; }
        public List<ActionFormEventPropertiesUrlParameters> UrlParameters { get; set; }
        public List<string> TranslateFields { get; set; }
        public bool RefreshOnDemand { get; set; }
    }
}
