using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models.IResponse;
using WebApplication2.Models.Responses;

namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// The response is used with multiple record actions (multiple records are manipulated with the single action trigger) (batch editing, batch status change, batch insert...).
    /// For endpoints that are configured in action procedure multiple
    /// </summary>
    public class RecordIDsResponse : CoreResponse<RecordIDsResponseData>, IRecordIDsResponseData
    {
        public RecordIDsResponse(long recordId)
        {
            this.Data = new RecordIDsResponseData(recordId);
        }

        public RecordIDsResponse()
        {
            this.Data = new RecordIDsResponseData();
        }

        public void SetId(long recordId)
        {
            this.Data.RecordIds = new List<long>() { recordId };
        }

        public void AddId(long recordId)
        {
            this.Data.RecordIds.Add(recordId);
        }

        public void AddId(List<int> recordIds)
        {
            this.Data.RecordIds.AddRange(recordIds.Select(i => (long)i).ToList());
        }

        public void AddId(List<long> recordIds)
        {
            this.Data.RecordIds.AddRange(recordIds);
        }

        public List<long> GetRecordIDs()
        {
            return this.Data.RecordIds;
        }
    }
}
