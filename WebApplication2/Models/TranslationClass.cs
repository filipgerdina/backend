namespace WebApplication2.Models
{
    public class TranslationClass
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class TranslationKeyClass
    {
        public int Id { get; set; }
        public string Key { get; set; }
    }
}
