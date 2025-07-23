using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class NavigationGroupPermissionsClass
    {
        [Key]
        public int Id { get; set; }
        public int ID_navigation_group { get; set; }
        public int ID_role { get; set; }
        public RoleClass Role { get; set; }
        public NavigationGroupClass Group { get; set; }
    }
}
