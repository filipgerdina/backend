using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class TabPropertiesTabs
    {
        public string Label { get; set; }
        public List<ActionFormControl> Controls { get; set; }
    }

    public class TabProperties
    {
        public List<TabPropertiesTabs> Tabs { get; set; }
    }
}
