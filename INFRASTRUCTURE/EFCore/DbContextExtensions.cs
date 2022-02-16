using INFRASTRUCTURE.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;

namespace INFRASTRUCTURE.EFCore
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this ApplicationDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);
            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);
            return !total.Except(applied).Any();
        }

        public static IQueryable<TEntity> IgnoreSoftDeleteFilter<TEntity>(this IQueryable<TEntity> baseQuery)
                      where TEntity : class
        {
            return baseQuery.IgnoreQueryFilters();
        }

        public static bool CheckTableExists<M>(this ApplicationDbContext context) where M : class
        {
            try
            {
                context.Set<M>().Count();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

