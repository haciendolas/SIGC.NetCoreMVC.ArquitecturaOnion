using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTaxExemptionRepositories
{
   internal class CatalogTaxExemptionDeleteRepository : ICatalogTaxExemptionDeleteRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogTaxExemptionDeleteRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> DeleteAsync(int CompanyID, int CatalogTaxExemptionID, CancellationToken CancellationToken)
        {            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogTaxExemptionDelete",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };               
            Command.Parameters.AddWithValue("@CompanyID", CompanyID);
            Command.Parameters.AddWithValue("@CatalogTaxExemptionID", CatalogTaxExemptionID); 
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}