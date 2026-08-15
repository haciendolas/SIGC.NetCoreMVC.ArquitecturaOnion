using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.UnitMeasure;
using SIGC.DomainService.IRepositories.IUnitMeasureRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UnitMeasureRepositories
{
    internal class UnitMeasureListRepository : IUnitMeasureListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public UnitMeasureListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<UnitMeasureListResponseDto>> ListAsync(int CountryID, CancellationToken CancellationToken = default)
        {
            var List = new List<UnitMeasureListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspUnitMeasureList";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CountryID", CountryID);
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new UnitMeasureListResponseDto(
                                    UnitMeasureID: Validation.SqlDBToInt32(ref DataReader, "UnitMeasureID"),
                                    UnitMeasureCode: Validation.SqlDBToString(ref DataReader, "UnitMeasureCode"),
                                    UnitMeasureName: Validation.SqlDBToString(ref DataReader, "UnitMeasureName")
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
