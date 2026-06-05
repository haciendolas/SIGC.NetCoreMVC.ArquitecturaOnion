using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models; 
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.EstablishmentRepositories
{
   internal class EstablishmentChangeStateRepository : IEstablishmentChangeStateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public EstablishmentChangeStateRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> ChangeStateAsync(Establishment Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Organization.uspEstablishmentChangeState";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@EstablishmentID", Model.EstablishmentID);
                Command.Parameters.AddWithValue("@RecordStateID", (byte)Model.RecordStateId);
                Command.Parameters.AddWithValue("@EstablishmentUpdatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@EstablishmentUpdatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@EstablishmentUpdatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@EstablishmentUpdatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
            }
            return RecordAffected;
        }
    }
}