using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace INFRASTRUCTURE.Validator
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomValidators<TType>(this IServiceCollection services)
        {
            return services.Scan(scan => scan
                .FromAssemblyOf<TType>()
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}
