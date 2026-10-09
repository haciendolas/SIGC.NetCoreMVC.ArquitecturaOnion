using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogTaxRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTaxRepositories
{
   internal class CatalogTaxChangeStateRepository : ICatalogTaxChangeStateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogTaxChangeStateRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> ChangeStateAsync(CatalogTax Model, CancellationToken CancellationToken)
        {            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogTaxChangeState",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };               
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogTaxID", Model.CatalogTaxID);
            Command.Parameters.AddWithValue("@RecordStateID", Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogTaxUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogTaxUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogTaxUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogTaxUpdatedDateTime", Model.CreatedDate);
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}