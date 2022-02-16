using INFRASTRUCTURE.Validator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace INFRASTRUCTURE.MediatR
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomMediatR<TType>(this IServiceCollection services,
            Action<IServiceCollection> doMoreActions = null)
        {
            services.AddMediatR(typeof(TType))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            doMoreActions?.Invoke(services);
            return services;
        }
    }
}
