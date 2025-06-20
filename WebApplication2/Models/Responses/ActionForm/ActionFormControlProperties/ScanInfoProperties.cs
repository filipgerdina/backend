using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class ScanInfoProperties
    {
        public int ColumnCount { get; set; }
        public List<ActionFormControl> Controls { get; set; }
        public bool MarginOnLeft { get; set; }
        public string MarginOnLeftDataField { get; set; }
        public List<ActionFormEvent> Events { get; set; }
    }
}
