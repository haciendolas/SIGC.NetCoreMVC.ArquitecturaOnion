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
    internal class UbigeoListByUbigeoClassRepository : IUbigeoListByUbigeoClassRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public UbigeoListByUbigeoClassRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<UbigeoListByUbigeoClassResponseDto>> ListByUbigeoClassAsync(int UbigeoClass, CancellationToken CancellationToken = default)
        {
            var List = new List<UbigeoListByUbigeoClassResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Security.uspUbigeoListByUbigeoClass";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@UbigeoClass", UbigeoClass);           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new UbigeoListByUbigeoClassResponseDto()
                            { 
                                UbigeoID = Validation.SqlDBToInt32(ref DataReader, "UbigeoID"),
                                UbigeoCode = Validation.SqlDBToString(ref DataReader, "UbigeoCode"),
                                UbigeoName = Validation.SqlDBToString(ref DataReader, "UbigeoName"),
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
