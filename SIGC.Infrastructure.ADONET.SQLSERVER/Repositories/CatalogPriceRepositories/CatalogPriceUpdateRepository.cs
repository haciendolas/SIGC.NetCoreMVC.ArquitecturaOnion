using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPriceRepositories
{
   internal class CatalogPriceUpdateRepository : ICatalogPriceUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogPriceUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> UpdateAsync(CatalogPrice Model, CancellationToken CancellationToken)
        {           
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogPriceUpdate",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CatalogPriceID", Model.CatalogPriceID);
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogPresentationID", Model.CatalogPresentationID);
            Command.Parameters.AddWithValue("@EstablishmentID", Model.EstablishmentID);
            Command.Parameters.AddWithValue("@PriceTypeID", Model.PriceTypeID);
            Command.Parameters.AddWithValue("@CurrencyTypeID ", Model.CurrencyTypeID);         
            Command.Parameters.AddWithValue("@CatalogPriceAmount", Model.CatalogPriceAmount);
            Command.Parameters.AddWithValue("@CatalogPriceIsTaxIncluded", Model.CatalogPriceIsTaxIncluded);
            Command.Parameters.AddWithValue("@RecordOriginID", Model.RecordOriginID);
            Command.Parameters.AddWithValue("@RecordStateID", Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogPriceUpdatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogPriceUpdatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogPriceUpdatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogPriceUpdatedDateTime", Model.CreatedDate);
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        } 
    }
}