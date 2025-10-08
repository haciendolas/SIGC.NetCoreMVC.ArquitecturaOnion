using Microsoft.Data.SqlClient;

namespace SIGC.DomainService.Transactions
{ 
    public class TransactionAccessor : ITransactionAccessor
    {
        public SqlConnection? CurrentConnection { get; private set; }
        public SqlTransaction? CurrentTransaction { get; private set; }
        private bool _ownsConnection;

        public async Task<SqlConnection> GetOrOpenConnectionAsync(string connectionString, CancellationToken cancellationToken)
        {
            if (CurrentConnection != null)
                return CurrentConnection;

            CurrentConnection = new SqlConnection(connectionString);
            await CurrentConnection.OpenAsync(cancellationToken);
            _ownsConnection = true;
            return CurrentConnection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            CurrentTransaction = transaction;
        }

        public async Task ClearAsync()
        {
            CurrentTransaction?.Dispose();

            if (_ownsConnection && CurrentConnection != null)
            {
                await CurrentConnection.CloseAsync();
                CurrentConnection.Dispose();
            }

            CurrentTransaction = null;
            CurrentConnection = null;
            _ownsConnection = false;
        }
    }

}