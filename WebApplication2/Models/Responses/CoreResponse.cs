using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.IResponse;

namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// Base class for returning data,
    /// </summary>
    public class CoreResponse<TData> : ICoreResponse<TData>
    {
        private readonly List<Message> messages;

        public CoreResponse()
        {
            this.messages = new List<Message>();
        }

        public TData Data { get; set; }

        public List<Message> Messages
        {
            get
            {
                return this.messages;
            }
        }

        public void AddMessage(Message message)
        {
            this.Messages.Add(message);
        }

        public void AddMessage(BaseException baseException)
        {
            this.Messages.Add(new Message()
            {
                Code = baseException.Code,
                Details = baseException.Details,
                Parameters = baseException.Parameters,
                Text = baseException.Text,
                Type = MessageType.Error,
                SubMessages = baseException.SubMessages,
            });
        }

        public void AddErrorMessage(string code, string defaultMessage = null)
        {
            this.Messages.Add(new Message()
            {
                Type = MessageType.Error,
                Code = code,
                Text = defaultMessage,
            });
        }

        public bool AnyError()
        {
            return this.Messages.Where(m => m.Type == MessageType.Error).Any();
        }

        [Obsolete("Use INotificationService.SuccessNotificationData and INotificationService.ErrorNotificationData")]
        public void Notify(string type, string subtype, object data, string username)
        {
        }

        [Obsolete("Use INotificationService.SuccessNotificationData and INotificationService.ErrorNotificationData")]
        public void Notify(string type, string subtype, object data)
        {
        }
    }
}
