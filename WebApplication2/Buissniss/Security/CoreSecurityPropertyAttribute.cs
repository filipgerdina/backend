using System;

namespace WebApplication2.Buissniss.Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CoreSecurityPropertyAttribute : Attribute
    {
        public CoreSecurityPropertyAttribute(string property)
        {
            this.SecureProperty = property;
        }

        public string SecureProperty { get; set; }
    }
}
