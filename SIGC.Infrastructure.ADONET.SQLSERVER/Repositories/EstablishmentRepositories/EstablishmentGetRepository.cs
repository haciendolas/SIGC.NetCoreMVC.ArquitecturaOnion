using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.EstablishmentRepositories
{
    internal class EstablishmentGetRepository : IEstablishmentGetRepository
    {
        private readonly string ConnectionString;   
        private readonly ITransactionAccessor TransactionAccessor;

        public EstablishmentGetRepository(IOptions<AppDbContext> Options, IJsonSerializerService JsonSerializerService,
            ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;        
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<EstablishmentGetResponseDto?> GetAsync(int CompanyID, int EstablishmentID, CancellationToken CancellationToken)
        {
            EstablishmentGetResponseDto? Get = null;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Organization.uspEstablishmentGet";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Parameters.AddWithValue("@EstablishmentID", EstablishmentID);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (DataReader.Read())
                        {
                            Get = new EstablishmentGetResponseDto(                            
                                EstablishmentID : Validation.SqlDBToInt32(ref DataReader, "EstablishmentID"),
                                TypeID : Validation.SqlDBToTinyint(ref DataReader, "TypeID"),
                                EstablishmentCode : Validation.SqlDBToString(ref DataReader, "EstablishmentCode"),
                                EstablishmentName: Validation.SqlDBToString(ref DataReader, "EstablishmentName"),
                                EstablishmentAddress: Validation.SqlDBToString(ref DataReader, "EstablishmentAddress"),
                                EstablishmentLogo : Validation.SqlDBToString(ref DataReader, "EstablishmentLogo"),
                                RecordStateID : Validation.SqlDBToTinyint(ref DataReader, "RecordStateID"),
                                EstablishmentUrl: null
                            );
                        }
                    }
                }
            }
            return Get;
        }
    }
}