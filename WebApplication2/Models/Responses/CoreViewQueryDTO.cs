using System;
using WebApplication2.Models.Responses;

namespace WebApplication2.Models.Responses
{
    public class CoreViewQueryDTO : CoreQueryEntityDTO
    {
        public bool? IsCheckedOut { get; set; }
        public DateTime? UtcCheckOutTimestamp { get; set; }
        public int? CheckedOutUserId { get; set; }
        public string CheckedOutUserInfo { get; set; }
    }
}
