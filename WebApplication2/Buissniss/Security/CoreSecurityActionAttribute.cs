using System;

namespace WebApplication2.Buissniss.Security
{
    public class CoreRequestAttribute : Attribute
    {
        public CoreRequestAttribute(string code)
        {
            this.Code = code;
        }

        public CoreRequestAttribute()
        {
        }

        public string Code { get; set; }
        public string ModulCode { get; set; }
        public bool AllowAnonymous { get; set; }
        public bool DisableESign { get; set; }
        public bool DisableAuditTrail { get; set; }

    }

    public class CoreSecurityActionAttribute : CoreRequestAttribute
    {
        public CoreSecurityActionAttribute(string code)
            : base(code)
        {
        }

        public CoreSecurityActionAttribute()
        {
        }
    }
}
