using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IUserCompanyRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserCompanyRepositories
{
    internal class UserCompanyCreateRepository : IUserCompanyCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public UserCompanyCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(UserCompany Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction; 
            using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "[Security].uspUserCompanyCreate";
                    Command.CommandType = CommandType.StoredProcedure;   
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                    Command.Parameters.AddWithValue("@UserID", Model.UserID);
                    Command.Parameters.AddWithValue("@StateID", (short)Model.StateID);
                    Command.Parameters.AddWithValue("@UserCompanyCreatedUserID", Model.CreatedBy);
                    Command.Parameters.AddWithValue("@UserCompanyCreatedDateTime", Model.CreatedDateTime);
                    Command.Connection = Connection;
                    Command.Transaction = Transaction;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);              
            }       
            return RecordAffected;
        }
    }
}
