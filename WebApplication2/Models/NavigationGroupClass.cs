namespace WebApplication2.Models
{
    public class NavigationGroupClass
    {
        public int Id { get; set; }
        public int? ParentGroupId { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }
}
