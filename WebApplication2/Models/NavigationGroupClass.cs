using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class NavigationGroupClass
    {
        [Key]
        public int Id { get; set; }
        public int? ID_parent_group { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public NavigationGroupClass? ParentGroup { get; set; }
    }
}
