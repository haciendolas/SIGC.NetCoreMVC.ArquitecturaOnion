using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPresentationRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPresentationRepositories
{
    internal class CatalogPresentationVerifyFieldsRepository : ICatalogPresentationVerifyFieldsRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogPresentationVerifyFieldsRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
        )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> VerifyFieldsAsync(CatalogPresentation Model, CancellationToken CancellationToken)
        { 
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogPresentationVerifyFields",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 60);
            Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
            Command.Parameters.AddWithValue("@CatalogPresentationID", Model.CatalogPresentationID);
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);        
            Command.Parameters.AddWithValue("@CatalogVariantID", Model.CatalogVariantID);
            Command.Parameters.AddWithValue("@PresentationID", Model.PresentationID);
            Command.Parameters.AddWithValue("@CatalogPresentationSKU", string.IsNullOrWhiteSpace(Model.CatalogPresentationSKU) ? DBNull.Value :Model.CatalogPresentationSKU);
            Command.Parameters.AddWithValue("@CatalogPresentationBarcode", string.IsNullOrWhiteSpace(Model.CatalogPresentationBarcode) ? DBNull.Value : Model.CatalogPresentationBarcode);
            await Command.ExecuteNonQueryAsync(CancellationToken);
            return Command.Parameters["@RetMsg"].Value.ToString()!;           
        }
    }
}