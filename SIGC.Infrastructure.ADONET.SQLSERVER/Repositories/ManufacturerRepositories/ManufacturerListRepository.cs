using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Manufacturer;
using SIGC.DomainService.IRepositories.IManufacturerRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.ManufacturerRepositories
{
    internal class ManufacturerListRepository : IManufacturerListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public ManufacturerListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<ManufacturerListResponseDto>> ListAsync(CancellationToken CancellationToken = default)
        {
            var List = new List<ManufacturerListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspManufacturerList";
                Command.CommandType = CommandType.StoredProcedure;           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new ManufacturerListResponseDto(
                                    ManufacturerID: Validation.SqlDBToInt32(ref DataReader, "ManufacturerID"),
                                    ManufacturerName: Validation.SqlDBToString(ref DataReader, "ManufacturerName")                                  
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
