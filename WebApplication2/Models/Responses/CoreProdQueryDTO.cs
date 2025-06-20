namespace WebApplication2.Models.Responses
{
    public class CoreProdQueryDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public int? GroupId { get; set; }
        public string GroupDisplayName { get; set; }
    }

    public class CoreProdQueryLongDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public int? GroupId { get; set; }
        public string GroupDisplayName { get; set; }
    }
}
