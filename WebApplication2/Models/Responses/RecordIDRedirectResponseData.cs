using System.Collections.Generic;
using WebApplication2.Models.Responses;

namespace WebApplication2.Models.Responses
{
    /// <summary>
    /// The class is used by the RecordIDResponse class to hold the data - the results for the response.
    /// </summary>
    public class RecordIDRedirectResponseData
    {
        public RecordIDRedirectResponseData(long recordId, string redirectUrl, bool openNewTab)
        {
            this.RecordId = recordId;
            this.RedirectUrl = redirectUrl;
            this.OpenNewTab = openNewTab;
        }

        public RecordIDRedirectResponseData(long recordId, string redirectUrl)
            : this(recordId, redirectUrl, true)
        {
        }

        public RecordIDRedirectResponseData()
        {
        }

        public long RecordId { get; set; }
        public string RedirectUrl { get; set; }
        public bool OpenNewTab { get; set; } = true;

        public List<long> GetRecordIDs()
        {
            return new List<long>() { this.RecordId };
        }
    }
}
