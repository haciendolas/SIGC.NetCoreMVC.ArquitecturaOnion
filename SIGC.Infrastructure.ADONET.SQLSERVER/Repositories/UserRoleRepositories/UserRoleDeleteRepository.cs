using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainService.IRepositories.IUserRoleRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserRoleRepositories
{
    internal class UserRoleDeleteRepository : IUserRoleDeleteRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public UserRoleDeleteRepository(IOptions<AppDbContext> Options, ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> DeleteAsync(int CompanyID, int UserID, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "[Security].uspUserRoleDelete";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Parameters.AddWithValue("@UserID", UserID);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
            }
            return RecordAffected;
        }
    }
}
