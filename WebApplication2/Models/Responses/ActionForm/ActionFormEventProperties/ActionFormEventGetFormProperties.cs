using System.Collections.Generic;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormEventProperties
{
    public class ActionFormEventGetFormProperties
    {
        public string Url { get; set; }
        public List<string> ExtraParamsFormValueParameters { get; set; }
        public List<string> ExtraParamsPageValueParameters { get; set; }
    }
}
