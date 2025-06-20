using System;

namespace WebApplication2.Models.Responses
{
    public class CoreQueryEntityDTO
    {
        public DateTime? UtcRecordTimestamp { get; set; }
        public int? UserId { get; set; }
        public string UserInfo { get; set; }

        public DateTime? UtcRecordTimestampUpd { get; set; }
        public int? UserIdUpd { get; set; }
        public string UserInfoUpd { get; set; }
    }
}
