using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.ICompanyRegisterRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRegisterRepositories
{
    internal class CompanyRegisterCreateRepository : ICompanyRegisterCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CompanyRegisterCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(CompanyRegister Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "[Security].uspCompanyRegisterCreate";
                Command.CommandType = CommandType.StoredProcedure; 
                Command.Parameters.AddWithValue("@CompanyIDRegister", Model.CompanyIDRegister);
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@CompanyRegisterCreatedDateTime", Model.CompanyRegisterCreatedDateTime);
                Command.Parameters.AddWithValue("@CompanyRegisterCreatedUserID", Model.CompanyRegisterCreatedUserID); 
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CompanyID = Convert.ToInt32(Command.Parameters["@CompanyID"].Value);
            }

            return RecordAffected;
        }
    }
}
