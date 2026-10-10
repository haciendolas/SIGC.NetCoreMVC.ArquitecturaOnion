using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTaxExemptionRepositories
{
    internal class CatalogTaxExemptionVerifyFieldsRepository : ICatalogTaxExemptionVerifyFieldsRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogTaxExemptionVerifyFieldsRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
        )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> VerifyFieldsAsync(CatalogTaxExemption Model, CancellationToken CancellationToken)
        { 
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogTaxExemptionVerifyFields",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 20);
            Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
            Command.Parameters.AddWithValue("@CatalogTaxExemptionID", Model.CatalogTaxExemptionID);
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@EstablishmentID", Model.EstablishmentID);
            Command.Parameters.AddWithValue("@CatalogTaxID", Model.CatalogTaxID);     
            await Command.ExecuteNonQueryAsync(CancellationToken);
            return Command.Parameters["@RetMsg"].Value.ToString()!;           
        }
    }
}