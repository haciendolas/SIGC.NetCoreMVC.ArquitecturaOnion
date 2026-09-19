using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainService.IRepositories.ICatalogTherapeuticActionRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTherapeuticActionRepositories
{
    internal class CatalogTherapeuticActionDeleteRepository : ICatalogTherapeuticActionDeleteRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogTherapeuticActionDeleteRepository(IOptions<AppDbContext> Options, ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> DeleteAsync(int CompanyID, int CatalogID, List<short> TherapeuticActionIDList, CancellationToken CancellationToken = default)
        {
            var DataTable = new DataTable();
            DataTable.Columns.Add("Id", typeof(short));
            foreach (var ID in TherapeuticActionIDList)
            {
                DataTable.Rows.Add(ID);
            }            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogTherapeuticActionDelete",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CompanyID", CompanyID);
            Command.Parameters.AddWithValue("@CatalogID", CatalogID);
            Command.Parameters.Add(new SqlParameter("@TherapeuticActionListID", SqlDbType.Structured)
            {
                TypeName = "Product.ttTherapeuticActionListID",
                Value = DataTable
            });
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}