using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTaxExemptionRepositories
{
   internal class CatalogTaxExemptionChangeStateRepository : ICatalogTaxExemptionChangeStateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogTaxExemptionChangeStateRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> ChangeStateAsync(CatalogTaxExemption Model, CancellationToken CancellationToken)
        {            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogTaxExemptionChangeState",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };               
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogTaxExemptionID", Model.CatalogTaxExemptionID);
            Command.Parameters.AddWithValue("@RecordStateID", Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogTaxExemptionUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogTaxExemptionUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogTaxExemptionUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogTaxExemptionUpdatedDateTime", Model.CreatedDate);
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}