using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Warehouse;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.WarehouseRepositories
{
    internal class WarehouseGetRepository : IWarehouseGetRepository
    {
        private readonly string ConnectionString;   
        private readonly ITransactionAccessor TransactionAccessor;

        public WarehouseGetRepository(IOptions<AppDbContext> Options, IJsonSerializerService JsonSerializerService,
            ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;        
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<WarehouseGetResponseDto?> GetAsync(int CompanyID, int WarehouseID, CancellationToken CancellationToken)
        {
            WarehouseGetResponseDto? Get = null;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Organization.uspWarehouseGet";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Parameters.AddWithValue("@WarehouseID", WarehouseID);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (DataReader.Read())
                        {
                            Get = new WarehouseGetResponseDto(
                                WarehouseID: Validation.SqlDBToInt32(ref DataReader, "WarehouseID"),
                                EstablishmentID : Validation.SqlDBToInt32(ref DataReader, "EstablishmentID"),
                                WarehouseTypeID: Validation.SqlDBToTinyint(ref DataReader, "WarehouseTypeID"),
                                WarehouseCode: Validation.SqlDBToString(ref DataReader, "WarehouseCode"),
                                WarehouseName: Validation.SqlDBToString(ref DataReader, "WarehouseName"),
                                WarehouseAddress: Validation.SqlDBToString(ref DataReader, "WarehouseAddress"),                        
                                RecordStateID : Validation.SqlDBToTinyint(ref DataReader, "RecordStateID")                             
                            );
                        }
                    }
                }
            }
            return Get;
        }
    }
}