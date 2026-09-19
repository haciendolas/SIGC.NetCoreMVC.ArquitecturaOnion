using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.ICatalogActiveIngredientRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogActiveIngredientRepositories
{
    internal class CatalogActiveIngredientCreateUpdateRepository : ICatalogActiveIngredientCreateUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogActiveIngredientCreateUpdateRepository(
            IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
        )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateUpdateAsync(CatalogActiveIngredient Model, CancellationToken CancellationToken = default)
        { 
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogActiveIngredientCreateUpdate",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogID", Model.CatalogID);
            Command.Parameters.AddWithValue("@ActiveIngredientID", Model.ActiveIngredientID);
            Command.Parameters.AddWithValue("@CatalogActiveIngredientQuantity", Model.CatalogActiveIngredientQuantity.HasValue ? Model.CatalogActiveIngredientQuantity.Value : DBNull.Value);
            Command.Parameters.AddWithValue("@UnitMeasureID", Model.UnitMeasureID.HasValue ? Model.UnitMeasureID.Value : DBNull.Value);
            Command.Parameters.AddWithValue("@CatalogActiveIngredientLabel", !string.IsNullOrWhiteSpace(Model.CatalogActiveIngredientLabel) ? Model.CatalogActiveIngredientLabel : DBNull.Value);
            Command.Parameters.AddWithValue("@RecordOriginID", (short)Model.RecordOriginID);
            Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogActiveIngredientCreatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogActiveIngredientCreatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogActiveIngredientCreatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogActiveIngredientCreatedDateTime", Model.CreatedDate);              
            return await Command.ExecuteNonQueryAsync(CancellationToken);   
        } 
    }
}
