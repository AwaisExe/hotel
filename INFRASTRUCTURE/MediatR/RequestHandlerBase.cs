using AutoMapper;
using INFRASTRUCTURE.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Principal;

namespace INFRASTRUCTURE.MediatR
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        protected RequestHandlerBase(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork)
        {
            Logger = loggerFactory.CreateLogger(GetType());
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        protected RequestHandlerBase(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork,IMapper mapper)
        {
            Logger = loggerFactory.CreateLogger(GetType());
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        protected RequestHandlerBase(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper, IPrincipal principal)
        {
            Logger = loggerFactory.CreateLogger(GetType());
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected RequestHandlerBase(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor context)
        {
            Logger = loggerFactory.CreateLogger(GetType());
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));        
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected ILogger Logger { get; }
        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get;  }
        protected IHttpContextAccessor Context { get; }
        protected IPrincipal Principal { get; }

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
