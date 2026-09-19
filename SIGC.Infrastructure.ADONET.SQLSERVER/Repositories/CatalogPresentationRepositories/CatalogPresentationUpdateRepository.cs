using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPresentationRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPresentationRepositories
{
   internal class CatalogPresentationUpdateRepository : ICatalogPresentationUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogPresentationUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
        )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> UpdateAsync(CatalogPresentation Model, CancellationToken CancellationToken)
        {      
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogPresentationUpdate",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogPresentationID", Model.CatalogPresentationID);
            Command.Parameters.AddWithValue("@CatalogVariantID", Model.CatalogVariantID);
            Command.Parameters.AddWithValue("@PresentationID", Model.PresentationID);       
            Command.Parameters.AddWithValue("@CatalogPresentationEquivalence", Model.CatalogPresentationEquivalence);
            Command.Parameters.AddWithValue("@CatalogPresentationSKU", string.IsNullOrWhiteSpace(Model.CatalogPresentationSKU) ? DBNull.Value : Model.CatalogPresentationSKU);
            Command.Parameters.AddWithValue("@CatalogPresentationBarcode", string.IsNullOrWhiteSpace(Model.CatalogPresentationBarcode) ? DBNull.Value : Model.CatalogPresentationBarcode);
            Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogPresentationUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogPresentationUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogPresentationUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogPresentationUpdatedDateTime", Model.CreatedDate);              
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        } 
    }
}