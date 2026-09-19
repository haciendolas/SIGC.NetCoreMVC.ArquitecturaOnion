using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogRepositories
{
   internal class CatalogUpdateRepository : ICatalogUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
        )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> UpdateAsync(Catalog Model, CancellationToken CancellationToken)
        {      
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogUpdate",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };            
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogID", Model.CatalogID);
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
            Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);     
            Command.Parameters.AddWithValue("@CatalogUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogUpdatedDateTime", Model.CreatedDate);              
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        } 
    }
}