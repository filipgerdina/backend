using System;
using System.Collections.Generic;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.IResponse;

namespace WebApplication2.Models.Exceptions
{
    public class BaseException : Exception
    {
        private Dictionary<string, string> parameters;

        public BaseException(string code, string message)
            : base(message)
        {
            this.Code = code;
            this.Text = message;
        }

        public BaseException(string code, string message, string details)
            : this(code, message)
        {
            this.Details = details;
        }

        public BaseException(string code)
            : this(code, string.Empty)
        {
            this.Code = code;
        }

        public BaseException(string code, Dictionary<string, string> parameters)
            : this(code)
        {
            this.Code = code;
            this.parameters = parameters;
        }

        public BaseException(string code, List<Message> subMessages, Dictionary<string, string> parameters)
            : this(code, parameters)
        {
            this.SubMessages = subMessages;
        }

        public bool Handeled { get; set; } = false;
        public string Code { get; set; }
        public string Text { get; set; }
        public string Details { get; set; }

        public override string Message
        {
            get
            {
                return this.Code + (string.IsNullOrEmpty(this.Text) ? string.Empty : "; " + this.Text);
            }
        }

        public Dictionary<string, string> Parameters
        {
            get
            {
                return this.parameters;
            }
            set
            {
                this.parameters = value;
            }
        }

        public List<Message> SubMessages { get; set; }
    }
}
