using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ModuleClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Module_Path { get; set; }
    }
}
