
using Microsoft.EntityFrameworkCore;


namespace INFRASTRUCTURE.Interface
{
    public interface IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task BeginTransaction(CancellationToken cancellationToken);
        Task CommitTransaction(CancellationToken cancellationToken);
        Task NewTransactionIfNeeded(CancellationToken cancellationToken);
        Task RollbackTransaction(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }   
}
