using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Warehouse;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.WarehouseRepositories
{
    internal class WarehouseListRepository : IWarehouseListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public WarehouseListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<WarehouseListResponseDto>> ListAsync(int CompanyID, int EstablishmentID,CancellationToken CancellationToken = default)
        {
            var List = new List<WarehouseListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspWarehouseList";
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Parameters.AddWithValue("@EstablishmentID", EstablishmentID);
                Command.CommandType = CommandType.StoredProcedure;           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new WarehouseListResponseDto(
                                    WarehouseID: Validation.SqlDBToInt32(ref DataReader, "WarehouseID"),
                                    WarehouseName: Validation.SqlDBToString(ref DataReader, "WarehouseName"),
                                    WarehouseCode: Validation.SqlDBToString(ref DataReader, "WarehouseCode")
                                );                          
                            List.Add(Get);
                        }
                    }
                }
            }
            return List;
        }
    }
}
