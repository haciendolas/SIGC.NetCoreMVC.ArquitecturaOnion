using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Ubigeo;
using SIGC.DomainService.IRepositories.IUbigeoRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UbigeoRepositories
{
    internal class UbigeoListSearchRepository : IUbigeoListSearchRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public UbigeoListSearchRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<UbigeoListSearchResponseDto>> ListSearchAsync(int UbigeoClassContinent, string UbigeoName, CancellationToken CancellationToken = default)
        {
            var List = new List<UbigeoListSearchResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Security.uspUbigeoListSearch";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@UbigeoClassContinent", UbigeoClassContinent);
                Command.Parameters.AddWithValue("@UbigeoName", UbigeoName ?? "");
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new UbigeoListSearchResponseDto()
                            {
                              
                                DepartmentName = Validation.SqlDBToString(ref DataReader, "DepartmentName"),
                                ProvinceName = Validation.SqlDBToString(ref DataReader, "ProvinceName"),
                                DistrictID = Validation.SqlDBToInt32(ref DataReader, "DistrictID"),
                                DistrictCode = Validation.SqlDBToString(ref DataReader, "DistrictCode"),
                                DistrictName = Validation.SqlDBToString(ref DataReader, "DistrictName"),
                            };
                            List.Add(Get);
                        }
                    }
                }
            }
            return List;
        }
    }
}
