using WebApplication2.Models.IResponse;
namespace WebApplication2.Models.IResponse
{
    public interface ICoreResponse<TData> : IMessageResponse
    {
        public TData Data { get; set; }
    }
}
