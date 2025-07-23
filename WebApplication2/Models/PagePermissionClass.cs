using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class PagePermissionClass
    {
        [Key]
        public int Id { get; set; }
        public int ID_page { get; set; }
        public int ID_role { get; set; }
        public RoleClass Role { get; set; }
        public PageClass Page { get; set; }
    }
}
