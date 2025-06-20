using System.Collections.Generic;

namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// The class is used by the RecordIDResponse class to hold the data - the results for the response.
    /// </summary>
    public class RecordIDResponseData
    {
        public RecordIDResponseData(long recordId)
        {
            this.RecordId = recordId;
        }

        public RecordIDResponseData()
        {
        }

        public long RecordId { get; set; }

        public List<long> GetRecordIDs()
        {
            return new List<long>() { this.RecordId };
        }
    }
}
