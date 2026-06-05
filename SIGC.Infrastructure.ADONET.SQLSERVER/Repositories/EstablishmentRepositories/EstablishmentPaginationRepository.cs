using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Establishment; 
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories; 
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.EstablishmentRepositories
{
    internal class EstablishmentPaginationRepository : IEstablishmentPaginationRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public EstablishmentPaginationRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<PaginationResponseDto<EstablishmentPaginationResponseDto>> PaginationAsync(EstablishmentPaginationRequestDto EstablishmentPaginationRequest, CancellationToken CancellationToken = default)
        {          
            var Pagination = new PaginationResponseDto<EstablishmentPaginationResponseDto>();
        
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Organization.uspEstablishmentPagination";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RecordsTotal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@CompanyID", EstablishmentPaginationRequest.CompanyID);
                    Command.Parameters.AddWithValue("@PersonID", EstablishmentPaginationRequest.PersonID);
                    Command.Parameters.AddWithValue("@EstablishmentName", string.IsNullOrWhiteSpace(EstablishmentPaginationRequest.Parameters.Search) ? DBNull.Value : EstablishmentPaginationRequest.Parameters.Search);
                    Command.Parameters.AddWithValue("@RecordStateID", EstablishmentPaginationRequest.RecordStateID.HasValue ? EstablishmentPaginationRequest.RecordStateID.Value : DBNull.Value);
                    Command.Parameters.AddWithValue("@PageNumber", EstablishmentPaginationRequest.Parameters.PageNumber);
                    Command.Parameters.AddWithValue("@PageSize", EstablishmentPaginationRequest.Parameters.PageSize);
                    Command.Connection = Connection;

                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                                var Get = new EstablishmentPaginationResponseDto()
                                {
                                    EstablishmentID = Validation.SqlDBToInt32(ref DataReader, "EstablishmentID"),
                                    EstablishmentCode = Validation.SqlDBToString(ref DataReader, "EstablishmentCode"),
                                    EstablishmentName = Validation.SqlDBToString(ref DataReader, "EstablishmentName"),
                                    EstablishmentAddress = Validation.SqlDBToString(ref DataReader, "EstablishmentAddress"),
                                    RecordStateID = Validation.SqlDBToTinyint(ref DataReader, "RecordStateID"),
                                    EstablishmentLastUpdatedDateTime = Validation.SqlDBToDateTime(ref DataReader, "EstablishmentLastUpdatedDateTime"),
                                    EstablishmentLastUpdatedUserID = Validation.SqlDBToInt32(ref DataReader, "EstablishmentLastUpdatedUserID"),
                                    EstablishmentLastUpdatedUserName = Validation.SqlDBToString(ref DataReader, "EstablishmentLastUpdatedUserName"),
                                    EstablishmentLastUpdatedUserFullName = Validation.SqlDBToString(ref DataReader, "EstablishmentLastUpdatedUserFullName"),
                                };
                                Pagination.Entities.Add(Get);
                                Pagination.Filtered = Validation.SqlDBToInt32(ref DataReader, "RecordsFiltered");
                            }
                        }
                    }
                    Pagination.Total = Convert.ToInt32(Command.Parameters["@RecordsTotal"].Value);
                }   
            return Pagination;
        }
    }
}
