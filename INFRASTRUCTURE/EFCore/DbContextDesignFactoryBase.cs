using INFRASTRUCTURE.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRASTRUCTURE.EFCore
{
    public abstract class DbContextDesignFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
                ?.GetConnectionString("DefaultConnection");

            Console.WriteLine($"Connection String: {connString}");

            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(
                    connString ?? throw new InvalidOperationException(),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    }
                );

            Console.WriteLine(connString);
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
        }
    }
}
