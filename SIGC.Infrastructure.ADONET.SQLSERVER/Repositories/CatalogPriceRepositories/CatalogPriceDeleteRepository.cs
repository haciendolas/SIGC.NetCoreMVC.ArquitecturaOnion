using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPriceRepositories
{
   internal class CatalogPriceDeleteRepository : ICatalogPriceDeleteRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogPriceDeleteRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> DeleteAsync(int CompanyID, int CatalogPriceID, CancellationToken CancellationToken)
        {            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogPriceDelete",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };               
            Command.Parameters.AddWithValue("@CompanyID", CompanyID);
            Command.Parameters.AddWithValue("@CatalogPriceID", CatalogPriceID); 
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}