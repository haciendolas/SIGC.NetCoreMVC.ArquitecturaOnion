using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogVariantRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogVariantRepositories
{
   internal class CatalogVariantCreateRepository : ICatalogVariantCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogVariantCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(CatalogVariant Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogVariantCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@CatalogVariantID", SqlDbType.Int);
                Command.Parameters["@CatalogVariantID"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@CatalogID", Model.CatalogID);
                Command.Parameters.AddWithValue("@CatalogVariantName", Model.CatalogVariantName);
                Command.Parameters.AddWithValue("@CatalogVariantSKU", string.IsNullOrWhiteSpace(Model.CatalogVariantSKU) ? DBNull.Value : Model.CatalogVariantSKU);
                Command.Parameters.AddWithValue("@RecordOriginID", (short)Model.RecordOriginID);
                Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);
                Command.Parameters.AddWithValue("@CatalogVariantCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CatalogVariantCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CatalogVariantCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CatalogVariantCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CatalogVariantID = Convert.ToInt32(Command.Parameters["@CatalogVariantID"].Value);
            }            
            return RecordAffected;
        } 
    }
}