using MediatR;
using WebApplication2.Models.IResponse;
using WebApplication2.Models.Request;

namespace WebApplication2.Models.Request
{
    public class ESignRequest<TData, TResponse> : CoreRequest<TData>, IRequest<TResponse>, IESignRequest
        where TResponse : IRecordIDsResponseData
    {
        public ESign ESign { get; set; }
    }
}
