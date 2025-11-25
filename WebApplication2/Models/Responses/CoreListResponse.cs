using System.Collections.Generic;
using WebApplication2.Models.IResponse;

namespace WebApplication2.Models.Responses
{
    public class CoreListResponse<TData> : ICoreResponse<List<TData>>
    {
        private readonly List<Message> messages;

        public CoreListResponse()
        {
            this.messages = new List<Message>();
        }

        public List<TData> Data { get; set; }

        public List<Message> Messages
        {
            get
            {
                return this.messages;
            }
        }

        public int? TotalCount { get; set; }
    }
}
