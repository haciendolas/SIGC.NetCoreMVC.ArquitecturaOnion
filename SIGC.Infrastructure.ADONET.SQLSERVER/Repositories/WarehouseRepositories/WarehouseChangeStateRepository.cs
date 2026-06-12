using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.WarehouseRepositories
{
   internal class WarehouseChangeStateRepository : IWarehouseChangeStateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public WarehouseChangeStateRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> ChangeStateAsync(Warehouse Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Organization.uspWarehouseChangeState";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@WarehouseID", Model.WarehouseID);
                Command.Parameters.AddWithValue("@RecordStateID", (byte)Model.RecordStateID);
                Command.Parameters.AddWithValue("@WarehouseUpdatedUserID", Model.CreatedByID);
                Command.Parameters.AddWithValue("@WarehouseUpdatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@WarehouseUpdatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@WarehouseUpdatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
            }
            return RecordAffected;
        }
    }
}