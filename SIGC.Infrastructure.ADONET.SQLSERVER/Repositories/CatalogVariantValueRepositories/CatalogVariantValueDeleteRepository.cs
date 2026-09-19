using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainService.IRepositories.ICatalogVariantValueRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogVariantValueRepositories
{
    internal class CatalogVariantValueDeleteRepository : ICatalogVariantValueDeleteRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogVariantValueDeleteRepository(IOptions<AppDbContext> Options, ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> DeleteAsync(int CompanyID, int CatalogVariantID, List<short> AttributeValueIDList, CancellationToken CancellationToken = default)
        {
            var DataTable = new DataTable();
            DataTable.Columns.Add("Id", typeof(short));
            foreach (var ID in AttributeValueIDList)
            {
                DataTable.Rows.Add(ID);
            }            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogVariantValueDelete",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CompanyID", CompanyID);
            Command.Parameters.AddWithValue("@CatalogVariantID", CatalogVariantID);
            Command.Parameters.Add(new SqlParameter("@AttributeValueListID", SqlDbType.Structured)
            {
                TypeName = "Product.ttAttributeValueListID",
                Value = DataTable
            });
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}