using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Models.Api
{
    public class CoreRouteAttribute : RouteAttribute
    {
        private readonly string moduleCode;
        public CoreRouteAttribute(string moduleCode, string controllerName = "[controller]")
            : base($"{moduleCode}/{controllerName}")
        {
            this.moduleCode = moduleCode;
        }

        public string ModuleCode => this.moduleCode;
    }
}
