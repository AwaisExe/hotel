using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        public IMediator _Mediator;
        protected ApiControllerBase(IMediator mediator)
        {
            _Mediator = mediator;
        }
    }
}
