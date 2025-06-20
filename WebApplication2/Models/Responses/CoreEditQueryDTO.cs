using System;
using WebApplication2.Models.Responses;

namespace WebApplication2.Models.Responses
{
    public class CoreEditQueryDTO : CoreQueryEntityDTO
    {
        public DateTime? UtcCheckOutTimestamp { get; set; }
        public int? CheckedOutUserId { get; set; }
        public string CheckedOutUserInfo { get; set; }
        public string ActionName { get; set; }
        public string BaseActionCode { get; set; }
    }
}
