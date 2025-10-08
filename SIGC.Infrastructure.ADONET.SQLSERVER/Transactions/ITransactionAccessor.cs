using Microsoft.Data.SqlClient;

namespace SIGC.DomainService.Transactions
{
    public interface ITransactionAccessor
    {
        SqlConnection? CurrentConnection { get; }
        SqlTransaction? CurrentTransaction { get; }
        Task<SqlConnection> GetOrOpenConnectionAsync(string connectionString, CancellationToken cancellationToken);
        void SetTransaction(SqlTransaction transaction);
        Task ClearAsync();
    }
}