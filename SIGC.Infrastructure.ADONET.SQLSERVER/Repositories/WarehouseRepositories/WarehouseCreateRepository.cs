using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.WarehouseRepositories
{
   internal class WarehouseCreateRepository : IWarehouseCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public WarehouseCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> CreateAsync(Warehouse Model, CancellationToken CancellationToken)
        {           
            string RetMsg = string.Empty;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Organization.uspWarehouseCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@WarehouseID", SqlDbType.Int);
                Command.Parameters["@WarehouseID"].Direction = ParameterDirection.Output;         
                Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 11);
                Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@EstablishmentID", Model.EstablishmentID);
                Command.Parameters.AddWithValue("@WarehouseTypeID", Model.WarehouseTypeID);
                Command.Parameters.AddWithValue("@WarehouseCode", Model.WarehouseCode);
                Command.Parameters.AddWithValue("@WarehouseName", Model.WarehouseName);
                Command.Parameters.AddWithValue("@WarehouseAddress", string.IsNullOrWhiteSpace(Model.WarehouseAddress) ? DBNull.Value : Model.WarehouseAddress); 
                Command.Parameters.AddWithValue("@RecordOriginID", (short)Model.RecordOriginID);
                Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateID);
                Command.Parameters.AddWithValue("@WarehouseCreatedUserID", Model.CreatedByID);
                Command.Parameters.AddWithValue("@WarehouseCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@WarehouseCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@WarehouseCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.WarehouseID = Convert.ToInt32(Command.Parameters["@WarehouseID"].Value);
                RetMsg = Command.Parameters["@RetMsg"].Value.ToString()!;
            }
            return RetMsg;
        }
    }
}