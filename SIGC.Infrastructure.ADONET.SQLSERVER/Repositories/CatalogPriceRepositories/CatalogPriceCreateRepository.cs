using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPriceRepositories
{
   internal class CatalogPriceCreateRepository : ICatalogPriceCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogPriceCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(CatalogPrice Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogPriceCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@CatalogPriceID", SqlDbType.Int);
                Command.Parameters["@CatalogPriceID"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@CatalogPresentationID", Model.CatalogPresentationID);
                Command.Parameters.AddWithValue("@EstablishmentID", Model.EstablishmentID);
                Command.Parameters.AddWithValue("@PriceTypeID", Model.PriceTypeID);
                Command.Parameters.AddWithValue("@CurrencyTypeID ", Model.CurrencyTypeID);         
                Command.Parameters.AddWithValue("@CatalogPriceAmount", Model.CatalogPriceAmount);
                Command.Parameters.AddWithValue("@CatalogPriceIsTaxIncluded", Model.CatalogPriceIsTaxIncluded);
                Command.Parameters.AddWithValue("@RecordOriginID", Model.RecordOriginID);
                Command.Parameters.AddWithValue("@RecordStateID", Model.RecordStateID);
                Command.Parameters.AddWithValue("@CatalogPriceCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CatalogPriceCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CatalogPriceCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CatalogPriceCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CatalogPriceID = Convert.ToInt32(Command.Parameters["@CatalogPriceID"].Value);
            }            
            return RecordAffected;
        } 
    }
}