using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WebApplication2.Models
{
    public class PageClass
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public string Component_Name { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int? ID_module { get; set; }
        public int? ID_group { get; set; }
        public NavigationGroupClass? Group { get; set; }
        public ModuleClass? Module { get; set; }
    }
}
