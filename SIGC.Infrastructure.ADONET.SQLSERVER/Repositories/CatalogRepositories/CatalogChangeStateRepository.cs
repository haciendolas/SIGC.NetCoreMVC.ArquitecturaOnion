using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogRepositories
{
   internal class CatalogChangeStateRepository : ICatalogChangeStateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogChangeStateRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> ChangeStateAsync(Catalog Model, CancellationToken CancellationToken)
        {            
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogChangeState",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };               
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogID", Model.CatalogID);
            Command.Parameters.AddWithValue("@RecordStateID", (byte)Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogUpdatedDateTime", Model.CreatedDate);
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}