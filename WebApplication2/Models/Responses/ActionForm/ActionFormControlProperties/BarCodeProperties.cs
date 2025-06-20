using System.Collections.Generic;
using WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties;

namespace WebApplication2.Models.Responses.ActionForm.ActionFormControlProperties
{
    public class BarCodeProperties : ScanInfoProperties
    {
        public bool HasRemoveButton { get; set; }

        public bool AllowsManualInput { get; set; }

        public int? Min { get; set; }
        public int? Max { get; set; }
        public string Regex { get; set; }
        public bool OpenCameraByDefault { get; set; }
    }
}
