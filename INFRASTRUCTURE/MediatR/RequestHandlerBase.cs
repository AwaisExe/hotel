using AutoMapper;
using INFRASTRUCTURE.Interface;
using MediatR;
using System.Diagnostics;

namespace INFRASTRUCTURE.MediatR
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        protected RequestHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var watch = Stopwatch.StartNew();
                var response = await ProcessAsync(request, cancellationToken).ConfigureAwait(false);
                watch.Stop();
                return response;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        protected abstract Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken);
    }
}
