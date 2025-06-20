using System.Text.Json;

namespace WebApplication2
{
    public class DataSourcePost
    {
        public string Name { get; set; }
        public JsonElement UrlParams { get; set; }
        public JsonElement QueryParams { get; set; }
        public JsonElement BodyParams { get; set; }
    }

    //public class UrlParams : Params { }

    //public class QueryParams : Params { }

    //public class BodyParams
    //{
    //    public object Data { get; set; }
    //}

    //public class Params
    //{
    //    public Dictionary<string, string> Parameters { get; set; } = new();
    //}
}
