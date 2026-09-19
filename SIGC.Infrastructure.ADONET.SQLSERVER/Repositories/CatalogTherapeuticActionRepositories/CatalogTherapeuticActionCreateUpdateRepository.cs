using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.ICatalogTherapeuticActionRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTherapeuticActionRepositories
{
    internal class CatalogTherapeuticActionCreateUpdateRepository : ICatalogTherapeuticActionCreateUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogTherapeuticActionCreateUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateUpdateAsync(CatalogTherapeuticAction Model, CancellationToken CancellationToken = default)
        { 
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using SqlCommand Command = new SqlCommand()
            {
                CommandText = "Product.uspCatalogTherapeuticActionCreateUpdate",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                Transaction = Transaction
            };
            Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
            Command.Parameters.AddWithValue("@CatalogID", Model.CatalogID);
            Command.Parameters.AddWithValue("@TherapeuticActionID", Model.TherapeuticActionID);
            Command.Parameters.AddWithValue("@RecordOriginID", (short)Model.RecordOriginID);
            Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);
            Command.Parameters.AddWithValue("@CatalogTherapeuticActionCreatedUserID", Model.CreatedById);
            Command.Parameters.AddWithValue("@CatalogTherapeuticActionCreatedUserName", Model.CreatedByName);
            Command.Parameters.AddWithValue("@CatalogTherapeuticActionCreatedUserFullName", Model.CreatedByFullName);
            Command.Parameters.AddWithValue("@CatalogTherapeuticActionCreatedDateTime", Model.CreatedDate);            
            return await Command.ExecuteNonQueryAsync(CancellationToken);
        }
    }
}
