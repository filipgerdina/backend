using System.Collections.Generic;
using WebApplication2.Models.IResponse;
using WebApplication2.Models.Responses;

namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// ESignResponse response can hold custom defined data, e sign response also holds record ids which are used for creating e signature after action is executed.
    /// ESignResponse is usually used on Export actions, where data is returned, and e sign is possible.
    /// </summary>
    public class ESignResponse<TResponse> : CoreResponse<TResponse>, IRecordIDsResponseData
    {
        public ESignResponse()
        {
            this.RecordIDs = new List<long>();
        }

        private List<long> RecordIDs { get; set; }

        public void SetRecordID(long recordId)
        {
            this.RecordIDs.Add(recordId);
        }

        public List<long> GetRecordIDs()
        {
            return this.RecordIDs;
        }
    }
}
