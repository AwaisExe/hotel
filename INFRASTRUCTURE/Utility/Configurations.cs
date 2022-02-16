using INFRASTRUCTURE.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace INFRASTRUCTURE.Utility
{
    public static class Configurations
    {
        public static IServiceProvider RunMi(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
                var context = services.GetService<ApplicationDbContext>();
                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(ApplicationDbContext).Name}");
                    var retry = Policy.Handle<Exception>().WaitAndRetry(new[]
                    {
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10),
                        TimeSpan.FromSeconds(15),
                    });

                    retry.Execute(() =>
                    {
                        context.Database.Migrate();
                    });
                    logger.LogInformation($"Migrated database associated with context {typeof(ApplicationDbContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(ApplicationDbContext).Name}");
                }
            }
            return serviceProvider;
        }
    }
}
