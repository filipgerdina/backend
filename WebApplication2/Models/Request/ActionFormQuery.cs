using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApplication2.Models.Request;

namespace WebApplication2.Models.Request
{
    public class ActionFormQuery
    {
        public string ActionCode { get; set; }
        public string RecordTypeCode { get; set; }
        public List<RecordParams> RecordParams { get; set; }
        [JsonIgnore]
        public string FormCode { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public System.Text.Json.JsonElement ExtraParamsPageValues { get; set; }
        public System.Text.Json.JsonElement ExtraParamsFormValues { get; set; }
    }
}
