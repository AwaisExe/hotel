using INFRASTRUCTURE.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace INFRASTRUCTURE.Persistence
{
    public class UnitOfWork : IUnitOfWork      
    {
        protected readonly ApplicationDbContext DbContext;
        private IDbContextTransaction Transaction;
        private readonly IsolationLevel? IsolationLevel;
        public UnitOfWork(ApplicationDbContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));       
        }
        public DbSet<TEntity> Set<TEntity>() where TEntity : class => DbContext.Set<TEntity>();
        public async Task BeginTransaction(CancellationToken cancellationToken) => await NewTransactionIfNeeded(cancellationToken);
        public async Task CommitTransaction(CancellationToken cancellationToken)
        {           
            if (Transaction == null) return;
            Transaction.Commit();
            Transaction.Dispose();
            Transaction = null;
            await Task.CompletedTask;
        }

        public async Task NewTransactionIfNeeded(CancellationToken cancellationToken)
        {
            if (Transaction == null)
            {
                Transaction = IsolationLevel.HasValue ? DbContext.Database.BeginTransaction(IsolationLevel.GetValueOrDefault()) : DbContext.Database.BeginTransaction();
            }
            await Task.CompletedTask;
        }

        public async Task RollbackTransaction(CancellationToken cancellationToken)
        {
            if (Transaction == null) return;
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
            await Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await DbContext.SaveChangesAsync(cancellationToken);
    }
}
