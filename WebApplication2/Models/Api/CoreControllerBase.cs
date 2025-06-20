using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using WebApplication2.Models.IResponse;

namespace WebApplication2.Models.Api
{
    /// <summary>
    /// Mediator should stay private, SendRequest is used to forese response type with message response type, to unify data output.
    /// </summary>
    public class CoreControllerBase : ControllerBase
    {
        public readonly IMediator _mediator;

        public CoreControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            where TResponse : IMessageResponse
        {
            return _mediator.Send(request, cancellationToken);
        }
    }

}
