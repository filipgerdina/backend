using MediatR;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.User.Queries.GetUsersQuery
{
    public class GetUsersQuery : IRequest<CoreListResponse<GetUsersQueryDTO>>
    {
        public int? skip { get; set; }
        public int? take { get; set; }
        public bool requireTotalCount { get; set; }

        // dxDataGrid: [{ selector: "username", desc: false }, ...]
        public List<SortItem>? sort { get; set; }

        // dxDataGrid: ["field","contains","text"] with nesting like [cond,"or",cond,...]
        // Bind as object[] so model binding (or manual deserialization) can handle arrays and nesting.
        public object[]? filter { get; set; }
    }

    public class SortItem
    {
        public string selector { get; set; } = string.Empty;
        public bool desc { get; set; }
    }
}
