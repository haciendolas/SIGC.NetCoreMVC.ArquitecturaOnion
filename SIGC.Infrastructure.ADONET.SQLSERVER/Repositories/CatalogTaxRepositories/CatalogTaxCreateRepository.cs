using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogTaxRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTaxRepositories
{
   internal class CatalogTaxCreateRepository : ICatalogTaxCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogTaxCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(CatalogTax Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogTaxCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@CatalogTaxID", SqlDbType.Int);
                Command.Parameters["@CatalogTaxID"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@CatalogID", Model.CatalogID);
                Command.Parameters.AddWithValue("@TaxID", Model.TaxID);
                Command.Parameters.AddWithValue("@TaxAffectationTypeID", Model.TaxAffectationTypeID);   
                Command.Parameters.AddWithValue("@RecordOriginID", Model.RecordOriginID);
                Command.Parameters.AddWithValue("@RecordStateID", Model.RecordStateID);
                Command.Parameters.AddWithValue("@CatalogTaxCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CatalogTaxCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CatalogTaxCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CatalogTaxCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CatalogTaxID = Convert.ToInt32(Command.Parameters["@CatalogTaxID"].Value);
            }            
            return RecordAffected;
        } 
    }
}