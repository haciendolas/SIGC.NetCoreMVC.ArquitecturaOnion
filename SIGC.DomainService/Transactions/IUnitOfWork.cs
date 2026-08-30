using System.Data;

namespace SIGC.DomainService.Transactions
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken = default, IsolationLevel isolationLevel = IsolationLevel.Serializable);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}