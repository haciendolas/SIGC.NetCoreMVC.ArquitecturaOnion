using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogRepositories
{
   internal class CatalogCreateRepository : ICatalogCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(Catalog Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@CatalogID", SqlDbType.Int);
                Command.Parameters["@CatalogID"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@CatalogTypeID", Model.CatalogTypeID);
                Command.Parameters.AddWithValue("@CategoryID", Model.CategoryID);
                Command.Parameters.AddWithValue("@CatalogCode", string.IsNullOrWhiteSpace(Model.CatalogCode) ? DBNull.Value : Model.CatalogCode);
                Command.Parameters.AddWithValue("@CatalogName", Model.CatalogName);
                Command.Parameters.AddWithValue("@CatalogSlug", Model.CatalogSlug);
                Command.Parameters.AddWithValue("@CatalogHasVariants", Model.CatalogHasVariants);
                Command.Parameters.AddWithValue("@SaleConditionID", Model.SaleConditionID);
                Command.Parameters.AddWithValue("@ManufacturerID", Model.ManufacturerID.HasValue && Model.ManufacturerID.Value > 0 ? Model.ManufacturerID : DBNull.Value);
                Command.Parameters.AddWithValue("@BrandID", Model.BrandID.HasValue && Model.BrandID.Value > 0 ? Model.BrandID : DBNull.Value);
                Command.Parameters.AddWithValue("@PharmaceuticalFormID", Model.PharmaceuticalFormID.HasValue && Model.PharmaceuticalFormID.Value > 0 ? Model.PharmaceuticalFormID : DBNull.Value);
                Command.Parameters.AddWithValue("@CatalogBrandType", string.IsNullOrWhiteSpace(Model.CatalogBrandType) ? "NINGUNO" : Model.CatalogBrandType);
                Command.Parameters.AddWithValue("@CatalogImage", string.IsNullOrWhiteSpace(Model.CatalogImage) ? DBNull.Value : Model.CatalogImage);
                Command.Parameters.AddWithValue("@CatalogConcentration", string.IsNullOrWhiteSpace(Model.CatalogConcentration) ? DBNull.Value : Model.CatalogConcentration);
                Command.Parameters.AddWithValue("@CatalogDescription", string.IsNullOrWhiteSpace(Model.CatalogDescription) ? DBNull.Value : Model.CatalogDescription);
                Command.Parameters.AddWithValue("@RecordOriginID", (short)Model.RecordOriginID);
                Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);
                Command.Parameters.AddWithValue("@CatalogCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CatalogCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CatalogCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CatalogCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CatalogID = Convert.ToInt32(Command.Parameters["@CatalogID"].Value);
            }         
            return RecordAffected;
        } 
    }
}