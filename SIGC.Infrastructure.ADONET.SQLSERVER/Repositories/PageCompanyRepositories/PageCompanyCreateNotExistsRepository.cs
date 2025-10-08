using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IPageCompanyRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PageCompanyRepositories
{
    internal class PageCompanyCreateNotExistsRepository : IPageCompanyCreateNotExistsRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public PageCompanyCreateNotExistsRepository(IOptions<AppDbContext> Options, ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> CreateNotExistsAsync(PageCompany Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction; 
            using (SqlCommand Command = new SqlCommand()){
                    Command.CommandText = "Security.uspPageCompanyCreateNotExists";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);         
                    Command.Parameters.AddWithValue("@PageID", Model.PageID);
                    Command.Parameters.AddWithValue("@PageCompanyCreatedDateTime", Model.CreatedDateTime);
                    Command.Parameters.AddWithValue("@PageCompanyCreatedUserID", Model.CreatedBy);
                    Command.Connection = Connection;
                    Command.Transaction = Transaction;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
             }        
            return RecordAffected;
        }
    }
}