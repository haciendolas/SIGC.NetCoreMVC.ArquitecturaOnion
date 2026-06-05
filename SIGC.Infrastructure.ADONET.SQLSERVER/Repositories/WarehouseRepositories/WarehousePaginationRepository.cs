using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainModel.Dtos.Warehouse;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.WarehouseRepositories
{
    internal class WarehousePaginationRepository : IWarehousePaginationRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public WarehousePaginationRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<PaginationResponseDto<WarehousePaginationResponseDto>> PaginationAsync(WarehousePaginationRequestDto WarehousePaginationRequest, CancellationToken CancellationToken = default)
        {          
            var Pagination = new PaginationResponseDto<WarehousePaginationResponseDto>();
        
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Organization.uspWarehousePagination";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RecordsTotal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@CompanyID", WarehousePaginationRequest.CompanyID);
                    Command.Parameters.AddWithValue("@EstablishmentID", WarehousePaginationRequest.EstablishmentID);
                    Command.Parameters.AddWithValue("@WarehouseName", string.IsNullOrWhiteSpace(WarehousePaginationRequest.Parameters.Search) ? DBNull.Value : WarehousePaginationRequest.Parameters.Search);
                    Command.Parameters.AddWithValue("@RecordStateID", WarehousePaginationRequest.RecordStateID.HasValue ? WarehousePaginationRequest.RecordStateID.Value : DBNull.Value);
                    Command.Parameters.AddWithValue("@PageNumber", WarehousePaginationRequest.Parameters.PageNumber);
                    Command.Parameters.AddWithValue("@PageSize", WarehousePaginationRequest.Parameters.PageSize);
                    Command.Connection = Connection;

                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                                var Get = new WarehousePaginationResponseDto()
                                {
                                    WarehouseID = Validation.SqlDBToInt32(ref DataReader, "WarehouseID"),
                                    WarehouseCode = Validation.SqlDBToString(ref DataReader, "WarehouseCode"),
                                    WarehouseName = Validation.SqlDBToString(ref DataReader, "WarehouseName"),
                                    EstablishmentCode = Validation.SqlDBToString(ref DataReader, "EstablishmentCode"),
                                    EstablishmentName = Validation.SqlDBToString(ref DataReader, "EstablishmentName"),
                                    RecordStateID = Validation.SqlDBToTinyint(ref DataReader, "RecordStateID"),
                                    WarehouseLastUpdatedDateTime = Validation.SqlDBToDateTime(ref DataReader, "WarehouseLastUpdatedDateTime"),
                                    WarehouseLastUpdatedUserID = Validation.SqlDBToInt32(ref DataReader, "WarehouseLastUpdatedUserID"),
                                    WarehouseLastUpdatedUserName = Validation.SqlDBToString(ref DataReader, "WarehouseLastUpdatedUserName"),
                                    WarehouseLastUpdatedUserFullName = Validation.SqlDBToString(ref DataReader, "WarehouseLastUpdatedUserFullName"),
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
