using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTaxExemptionRepositories
{
   internal class CatalogTaxExemptionCreateRepository : ICatalogTaxExemptionCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogTaxExemptionCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(CatalogTaxExemption Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogTaxExemptionCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@CatalogTaxExemptionID", SqlDbType.Int);
                Command.Parameters["@CatalogTaxExemptionID"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@EstablishmentID", Model.EstablishmentID);
                Command.Parameters.AddWithValue("@CatalogTaxID", Model.CatalogTaxID);           
                Command.Parameters.AddWithValue("@RecordOriginID", Model.RecordOriginID);
                Command.Parameters.AddWithValue("@RecordStateID", Model.RecordStateID);
                Command.Parameters.AddWithValue("@CatalogTaxExemptionCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CatalogTaxExemptionCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CatalogTaxExemptionCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CatalogTaxExemptionCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CatalogTaxExemptionID = Convert.ToInt32(Command.Parameters["@CatalogTaxExemptionID"].Value);
            }            
            return RecordAffected;
        } 
    }
}