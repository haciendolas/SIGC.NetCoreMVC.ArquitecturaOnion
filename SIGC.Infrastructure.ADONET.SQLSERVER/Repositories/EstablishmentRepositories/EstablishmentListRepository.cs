using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.EstablishmentRepositories
{
    internal class EstablishmentListRepository : IEstablishmentListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public EstablishmentListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<EstablishmentListResponseDto>> ListAsync(int CompanyID, int PersonID, CancellationToken CancellationToken = default)
        {
            var List = new List<EstablishmentListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Organization.uspEstablishmentList";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Parameters.AddWithValue("@PersonID", PersonID);
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new EstablishmentListResponseDto(
                                    EstablishmentID: Validation.SqlDBToInt32(ref DataReader, "EstablishmentID"),
                                    EstablishmentName: Validation.SqlDBToString(ref DataReader, "EstablishmentName"),
                                    EstablishmentAddress: Validation.SqlDBToString(ref DataReader, "EstablishmentAddress")
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
