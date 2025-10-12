using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyChangeStateRepository : ICompanyChangeStateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CompanyChangeStateRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> ChangeStateAsync(Company Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken); 
            using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRoleChangeState";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);           
                    Command.Parameters.AddWithValue("@StateID", (short)Model.StateID);
                    Command.Parameters.AddWithValue("@CompanyCreatedUserID", Model.CreatedBy);
                    Command.Parameters.AddWithValue("@CompanyCreatedDateTime", Model.CreatedDateTime);
                    Command.Connection = Connection;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
           }            
           return RecordAffected;
        }
    }
}
