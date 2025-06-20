using System.Collections.Generic;
using WebApplication2.Models.IResponse;

namespace WebApplication2.Models.IResponse
{
    /// <summary>
    /// Najbolj osnovni interface za obliko rezultata iz APIja.
    /// ICoreResponse je izpeljan iz njega.
    /// ICoreResponse implementirajo osnovni klasi za response:
    /// CoreResponse za akcije:
    ///     -ESignResponse
    ///     -RecordIDResponse
    ///     -RecordIDsResponse
    /// CoreListResponse za query
    /// </summary>
    public interface IMessageResponse
    {
        public List<Message> Messages { get; }
    }
}
