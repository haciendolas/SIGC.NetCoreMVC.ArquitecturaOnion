using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogVariantRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogVariantRepositories
{
   internal class CatalogVariantUpdateRepository : ICatalogVariantUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogVariantUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> UpdateAsync(CatalogVariant Model, CancellationToken CancellationToken)
        {           
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogVariantUpdate",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogVariantID", Model.CatalogVariantID);
            Command.Parameters.AddWithValue("@CatalogID", Model.CatalogID);
            Command.Parameters.AddWithValue("@CatalogVariantName", Model.CatalogVariantName); 
            Command.Parameters.AddWithValue("@CatalogVariantSKU", string.IsNullOrWhiteSpace(Model.CatalogVariantSKU) ? DBNull.Value : Model.CatalogVariantSKU);
            Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogVariantUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogVariantUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogVariantUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogVariantUpdatedDateTime", Model.CreatedDate);
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        } 
    }
}