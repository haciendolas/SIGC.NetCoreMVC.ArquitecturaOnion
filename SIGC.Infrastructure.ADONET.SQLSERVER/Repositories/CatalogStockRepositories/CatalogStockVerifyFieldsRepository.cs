using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogStockRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogStockRepositories
{
    internal class CatalogStockVerifyFieldsRepository : ICatalogStockVerifyFieldsRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogStockVerifyFieldsRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
        )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> VerifyFieldsAsync(CatalogStock Model, CancellationToken CancellationToken)
        { 
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogStockVerifyFields",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 20);
            Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
            Command.Parameters.AddWithValue("@CatalogStockID", Model.CatalogStockID);
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogVariantID", Model.CatalogVariantID);
            Command.Parameters.AddWithValue("@WarehouseID", Model.WarehouseID);     
            await Command.ExecuteNonQueryAsync(CancellationToken);
            return Command.Parameters["@RetMsg"].Value.ToString()!;           
        }
    }
}