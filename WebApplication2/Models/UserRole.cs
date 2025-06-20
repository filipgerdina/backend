namespace WebApplication2.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public Status Status { get; set; }
    }

    public enum Status { 
        Active = 0,
        NonActive = 1,
    }
}
