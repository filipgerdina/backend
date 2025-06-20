using MediatR;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Buissniss.Users.Module.GetUserActionFormQuery
{
    public class GetUserActionFormQuery : CoreRequest<UserActionFormQuery>, IRequest<CoreResponse<ActionFormQueryDTO>>
    { }


    public class UserActionFormQuery : ActionFormQuery
    {
        [Required]
        public string FormCode { get; set; }
    }
}
