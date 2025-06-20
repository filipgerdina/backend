using MediatR;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Roles.Module.GetRolesActionsFormQuery
{
    public class GetRolesActionFormQuery : CoreRequest<RolesActionFormQuery>, IRequest<CoreResponse<ActionFormQueryDTO>>
    { }


    public class RolesActionFormQuery : ActionFormQuery
    {
        [Required]
        public string FormCode { get; set; }
    }
}
