using System.Collections.Generic;
using WebApplication2.Models.IResponse;

namespace WebApplication2.Models.IResponse
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
        public string Details { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public List<Message> SubMessages { get; set; }
    }
}
