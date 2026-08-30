using Microsoft.Extensions.Options;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        private readonly ITransactionAccessor _transactionAccessor;

        public UnitOfWork(IOptions<AppDbContext> options, ITransactionAccessor transactionAccessor)
        {
            _connectionString = options.Value.ConnectionDBCommerce360 ?? throw new ArgumentNullException(nameof(options));
            _transactionAccessor = transactionAccessor ?? throw new ArgumentNullException(nameof(transactionAccessor));
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default, IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            // Obtiene o abre la conexión
            var connection = await _transactionAccessor.GetOrOpenConnectionAsync(_connectionString, cancellationToken);

            // Inicia la transacción
            var transaction = connection.BeginTransaction(isolationLevel);
            //var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            _transactionAccessor.SetTransaction(transaction);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _transactionAccessor.CurrentTransaction;
            if (transaction == null)
                throw new InvalidOperationException("No transaction started.");

            await transaction.CommitAsync(cancellationToken);
            await _transactionAccessor.ClearAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _transactionAccessor.ClearAsync();
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _transactionAccessor.CurrentTransaction;
            if (transaction == null)
                throw new InvalidOperationException("No transaction started.");

            await transaction.RollbackAsync(cancellationToken);
            await _transactionAccessor.ClearAsync();
        }     
    }
}