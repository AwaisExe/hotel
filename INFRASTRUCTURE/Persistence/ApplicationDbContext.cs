using DOMAIN.Entities;
using DOMAIN.Interface;
using INFRASTRUCTURE.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace INFRASTRUCTURE.Persistence
{

    public class ApplicationDbContext : DbContext
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
           ) : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.Model.GetEntityTypes().ToList();
            LoadConfiguration(modelBuilder);
            DeleteFilter(modelBuilder);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                OnBeforeSaving();
                var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return result;

            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbConcurrencyException(
                    "The record you attempted to edit was modified by another " +
                    "user after you loaded it. The edit operation was cancelled and the " +
                    "currect values of the record are displayed. Please try again.", exception);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadConfiguration(ModelBuilder modelBuilder)
        {
            GetType().Assembly.GetTypes()
               .Where(t => !t.GetTypeInfo().IsAbstract && t.GetInterfaces().Contains(typeof(IEntityConfiguration)) || !t.GetTypeInfo().IsAbstract && t.GetInterfaces().Contains(typeof(IBaseEntityConfiguration)))
               .ToList()
               .ForEach(t =>
               {
                   if (typeof(IEntityConfiguration).IsAssignableFrom(t)) ((IEntityConfiguration)Activator.CreateInstance(t, new[] { modelBuilder })).Configure();
                   if (typeof(IBaseEntityConfiguration).IsAssignableFrom(t)) Activator.CreateInstance(t, new[] { modelBuilder });
               });
        }
        protected static void ConfigureSoftDeleteFilter<T>(ModelBuilder modelBuilder)
        where T : class, ISoftDelete
        {
            modelBuilder.Entity<T>()
                .HasQueryFilter(e => !e.IsDeleted);

        }
        protected void DeleteFilter(ModelBuilder modelBuilder)
        {
            var deletableEntityTypes = GetType().GetTypeInfo().DeclaredMethods.Single(m => m.Name == nameof(ConfigureSoftDeleteFilter));
            var args = new object[] { modelBuilder };
            var deleteEntityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(ISoftDelete).IsAssignableFrom(t.ClrType));
            foreach (var entityType in deleteEntityTypes)
                deletableEntityTypes.MakeGenericMethod(entityType.ClrType).Invoke(this, args);
        }       
        private void OnBeforeSaving()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    (e.Entity is ITrackCreated || e.Entity is ISoftDelete || e.Entity is ITrackUpdated) &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in changedEntries)
            {
                TrackAdded(entry);
                TrackUpdated(entry);
                TrackDeleted(entry);

            }
        }

        private static void TrackUpdated(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            if (entry.Entity is ITrackUpdated updated)
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    updated.UpdatedBy = "Administrator";// Todo httpcontext
                    updated.UpdatedOn = DateTime.Now;
                }
            }
        }

        private static void TrackAdded(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            if (entry.Entity is ITrackCreated added)
            {
                if (entry.State == EntityState.Added && added.CreatedOn == default)
                {
                    added.CreatedBy = "Administrator"; // Todo httpcontext
                    added.CreatedOn = DateTime.Now;
                }
            }
        }

        private static void TrackDeleted(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            if (entry.Entity is ISoftDelete deleted)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["UpdatedOn"] = DateTimeOffset.UtcNow;
                    deleted.IsDeleted = true;
                }
            }
        }
    }
}
