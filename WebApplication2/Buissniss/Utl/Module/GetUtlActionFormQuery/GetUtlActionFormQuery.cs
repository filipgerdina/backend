using MediatR;
using WebApplication2.Models.Request;
using WebApplication2.Models.Responses.ActionForm;
using WebApplication2.Models.Responses;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Utl.Module.GetUtlActionFormQuery
{
    public class GetUtlActionFormQuery : CoreRequest<UtlActionFormQuery>, IRequest<CoreResponse<ActionFormQueryDTO>>
    { }


    public class UtlActionFormQuery : ActionFormQuery
    {
        [Required]
        public string FormCode { get; set; }
    }
}
