using System.Collections.Generic;

namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// The class is used by the RecordIDsResponse class to hold the data - the results for the response.
    /// </summary>
    public class RecordIDsResponseData
    {
        public RecordIDsResponseData(long recordId)
        {
            this.RecordIds = new List<long>() { recordId };
        }

        public RecordIDsResponseData()
        {
            this.RecordIds = new List<long>();
        }

        public List<long> RecordIds { get; set; }

        public List<long> GetRecordIDs()
        {
            return this.RecordIds;
        }
    }
}
