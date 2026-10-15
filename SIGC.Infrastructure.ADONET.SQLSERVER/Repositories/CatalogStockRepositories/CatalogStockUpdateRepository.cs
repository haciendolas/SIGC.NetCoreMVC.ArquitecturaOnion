using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogStockRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogStockRepositories
{
   internal class CatalogStockUpdateRepository : ICatalogStockUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogStockUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> UpdateAsync(CatalogStock Model, CancellationToken CancellationToken)
        {           
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogStockUpdate",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CatalogStockID", Model.CatalogStockID);
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogVariantID", Model.CatalogVariantID);
            Command.Parameters.AddWithValue("@WarehouseID", Model.WarehouseID);
            Command.Parameters.AddWithValue("@CatalogStockCurrentQuantity", Model.CatalogStockCurrentQuantity.HasValue ? Model.CatalogStockCurrentQuantity: DBNull.Value);
            Command.Parameters.AddWithValue("@CatalogStockMinimumQuantity ", Model.CatalogStockMinimumQuantity);         
            Command.Parameters.AddWithValue("@CatalogStockMaximumQuantity", Model.CatalogStockMaximumQuantity);
            Command.Parameters.AddWithValue("@CatalogStockUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogStockUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogStockUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogStockUpdatedDateTime", Model.CreatedDate);
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        } 
    }
}