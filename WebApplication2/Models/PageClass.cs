namespace WebApplication2.Models
{
    public class PageClass
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string PageComponent { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public ModuleClass Module{ get; set; }
        public NavigationGroupClass NavigationGroup { get; set; }
    }
}
