using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogStockRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogStockRepositories
{
   internal class CatalogStockCreateRepository : ICatalogStockCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogStockCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(CatalogStock Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogStockCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@CatalogStockID", SqlDbType.Int);
                Command.Parameters["@CatalogStockID"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@CatalogVariantID", Model.CatalogVariantID);
                Command.Parameters.AddWithValue("@WarehouseID ", Model.WarehouseID);
                Command.Parameters.AddWithValue("@CatalogStockCurrentQuantity", Model.CatalogStockCurrentQuantity);
                Command.Parameters.AddWithValue("@CatalogStockMinimumQuantity ", Model.CatalogStockMinimumQuantity);         
                Command.Parameters.AddWithValue("@CatalogStockMaximumQuantity", Model.CatalogStockMaximumQuantity);         
                Command.Parameters.AddWithValue("@RecordOriginID", Model.RecordOriginID);
                Command.Parameters.AddWithValue("@RecordStateID", Model.RecordStateID);
                Command.Parameters.AddWithValue("@CatalogStockCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CatalogStockCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CatalogStockCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CatalogStockCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CatalogStockID = Convert.ToInt32(Command.Parameters["@CatalogStockID"].Value);
            }            
            return RecordAffected;
        } 
    }
}