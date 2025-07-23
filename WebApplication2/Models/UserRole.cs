using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int ID_user { get; set; }
        public int ID_role { get; set; }
    }
}
