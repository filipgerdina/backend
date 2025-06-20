using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class GroupProperties
    {
        public List<ActionFormControl> Controls { get; set; }

        public bool CompactForm { get; set; }
    }
}
