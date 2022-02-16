using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace INFRASTRUCTURE.Extensions
{
    public static class ConfigurationAddExtensions
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (env == null) throw new ArgumentNullException(nameof(env));

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
