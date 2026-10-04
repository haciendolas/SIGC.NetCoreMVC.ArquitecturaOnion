using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPriceRepositories
{
   internal class CatalogPriceChangeStateRepository : ICatalogPriceChangeStateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogPriceChangeStateRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> ChangeStateAsync(CatalogPrice Model, CancellationToken CancellationToken)
        {            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogPriceChangeState",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };               
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogPriceID", Model.CatalogPriceID);
            Command.Parameters.AddWithValue("@RecordStateID", Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogPriceUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogPriceUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogPriceUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogPriceUpdatedDateTime", Model.CreatedDate);
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}