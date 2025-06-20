namespace WebApplication2.Models.Responses
{
    public class CoreProdHierarchyQueryDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? Sequence { get; set; }

        public int? ParentId { get; set; }
        public string ParentDisplayName { get; set; }
    }
}
