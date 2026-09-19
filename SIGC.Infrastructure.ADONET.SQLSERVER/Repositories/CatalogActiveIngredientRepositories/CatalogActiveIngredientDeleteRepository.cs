using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainService.IRepositories.ICatalogActiveIngredientRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogActiveIngredientRepositories
{
    internal class CatalogActiveIngredientDeleteRepository : ICatalogActiveIngredientDeleteRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogActiveIngredientDeleteRepository(IOptions<AppDbContext> Options, ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> DeleteAsync(int CompanyID, int CatalogID, List<int> ActiveIngredientIDList, CancellationToken CancellationToken = default)
        {
            var DataTable = new DataTable();
            DataTable.Columns.Add("Id", typeof(int));
            foreach (var ID in ActiveIngredientIDList)
            {
                DataTable.Rows.Add(ID);
            }            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogActiveIngredientDelete",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CompanyID", CompanyID);
            Command.Parameters.AddWithValue("@CatalogID", CatalogID);
            Command.Parameters.Add(new SqlParameter("@CatalogActiveIngredientListID", SqlDbType.Structured)
            {
                TypeName = "Product.ttCatalogActiveIngredientListID",
                Value = DataTable
            });
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}