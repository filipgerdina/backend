using System.Collections.Generic;

namespace WebApplication2.Models.IResponse
{
    public interface IRecordIDsResponseData
    {
        public List<long> GetRecordIDs();
    }
}
