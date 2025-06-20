using MediatR;
using WebApplication2.Models;
using WebApplication2.Models.DataStore;
using WebApplication2.Models.Responses;

namespace WebApplication2.Buissniss.User.Queries.GetRolesOfUserQuery
{
    public class GetRolesOfUserQuery: QueryDataList, IRequest<CoreListResponse<GetRolesOfUserQueryDTO>>
    {
        public int Id { get; set; }
    }
}
