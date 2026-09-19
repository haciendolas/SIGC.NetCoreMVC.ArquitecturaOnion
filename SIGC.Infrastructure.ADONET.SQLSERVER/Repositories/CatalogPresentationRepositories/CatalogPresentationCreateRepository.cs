using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPresentationRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPresentationRepositories
{
   internal class CatalogPresentationCreateRepository : ICatalogPresentationCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogPresentationCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(CatalogPresentation Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogPresentationCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@CatalogPresentationID", SqlDbType.Int);
                Command.Parameters["@CatalogPresentationID"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@CatalogVariantID", Model.CatalogVariantID);
                Command.Parameters.AddWithValue("@PresentationID", Model.PresentationID);
                Command.Parameters.AddWithValue("@CatalogPresentationIsDefault", Model.CatalogPresentationIsDefault);
                Command.Parameters.AddWithValue("@CatalogPresentationEquivalence", Model.CatalogPresentationEquivalence); 
                Command.Parameters.AddWithValue("@CatalogPresentationSKU", string.IsNullOrWhiteSpace(Model.CatalogPresentationSKU) ? DBNull.Value : Model.CatalogPresentationSKU);
                Command.Parameters.AddWithValue("@CatalogPresentationBarcode", string.IsNullOrWhiteSpace(Model.CatalogPresentationBarcode) ? DBNull.Value : Model.CatalogPresentationBarcode);
                Command.Parameters.AddWithValue("@RecordOriginID", (short)Model.RecordOriginID);
                Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);
                Command.Parameters.AddWithValue("@CatalogPresentationCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CatalogPresentationCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CatalogPresentationCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CatalogPresentationCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CatalogPresentationID = Convert.ToInt32(Command.Parameters["@CatalogPresentationID"].Value);
            }            
            return RecordAffected;
        } 
    }
}