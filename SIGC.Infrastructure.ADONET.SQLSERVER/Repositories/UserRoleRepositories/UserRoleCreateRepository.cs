using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IUserRoleRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.DomainService.IRepositories.UserRoleRepositories
{
    internal class UserRoleCreateRepository : IUserRoleCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public UserRoleCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(UserRole Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "[Security].uspUserRoleCreate";
                Command.CommandType = CommandType.StoredProcedure;     
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@RoleID", Model.RoleID);
                Command.Parameters.AddWithValue("@UserID", Model.UserID); 
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);              
            }
            return RecordAffected;
        }
    }
}