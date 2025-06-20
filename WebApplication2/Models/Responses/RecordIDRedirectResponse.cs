using System.Collections.Generic;
using WebApplication2.Models.IResponse;
using WebApplication2.Models.Responses;

namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// The response is used for single record action (only one record is manipulated).
    /// The for endpoints configured in action procedure (except export actions - these actions use ESignResponse)
    /// </summary>
    public class RecordIDRedirectResponse : CoreResponse<RecordIDRedirectResponseData>, IRecordIDsResponseData
    {
        public RecordIDRedirectResponse(long recordId, string redirectUrl, bool openNewTab)
        {
            this.Data = new RecordIDRedirectResponseData(recordId, redirectUrl, openNewTab);
        }

        public RecordIDRedirectResponse(long recordId, string redirectUrl)
            : this(recordId, redirectUrl, true)
        {
        }

        public RecordIDRedirectResponse()
        {
            this.Data = new RecordIDRedirectResponseData();
        }

        public void SetId(long recordId)
        {
            this.Data.RecordId = recordId;
        }

        public List<long> GetRecordIDs()
        {
            return new List<long>() { this.Data.RecordId };
        }
    }
}
