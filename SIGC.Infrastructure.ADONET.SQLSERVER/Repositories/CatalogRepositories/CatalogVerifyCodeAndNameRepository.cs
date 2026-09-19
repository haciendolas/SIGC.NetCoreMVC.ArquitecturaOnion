using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogRepositories; 
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogRepositories
{
    internal class CatalogVerifyCodeAndNameRepository : ICatalogVerifyCodeAndNameRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogVerifyCodeAndNameRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
        )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> VerifyCodeAndNameAsync(Catalog Model, CancellationToken CancellationToken)
        { 
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogVerifyCodeAndName",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 20);
            Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogID", Model.CatalogID);
            Command.Parameters.AddWithValue("@CatalogCode", string.IsNullOrWhiteSpace(Model.CatalogCode) ? DBNull.Value : Model.CatalogCode);
            Command.Parameters.AddWithValue("@CatalogName", Model.CatalogName);            
            await Command.ExecuteNonQueryAsync(CancellationToken);
            return Command.Parameters["@RetMsg"].Value.ToString()!;           
        }
    }
}