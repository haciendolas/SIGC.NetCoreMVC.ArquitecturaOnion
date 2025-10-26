using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Constant;
using SIGC.DomainService.IRepositories.IConstantRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.ConstantRepositories
{
    internal class ConstantListRepository : IConstantListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public ConstantListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<ConstantListResponseDto>> ListAsync(string ConstantClass, CancellationToken CancellationToken = default)
        {
            var List = new List<ConstantListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Security.uspUbigeoListSearch";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@ConstantClass", ConstantClass);             
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new ConstantListResponseDto(){                           
                                ConstantID = Validation.SqlDBToInt16(ref DataReader, "ConstantID"),
                                ConstantClass = Validation.SqlDBToInt32(ref DataReader, "ConstantClass"),
                                ConstantAbbreviation = Validation.SqlDBToString(ref DataReader, "ConstantAbbreviation"),
                                ConstantName = Validation.SqlDBToString(ref DataReader, "ConstantName"),
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
