using INFRASTRUCTURE.MediatR;
using INFRASTRUCTURE.Validator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace APPLICATION
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddCustomMediatR<Actor>();
            services.AddCustomValidators<Actor>();
            return services;
        }
    }
}
