using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyPaginationRepository : ICompanyPaginationRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CompanyPaginationRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<PaginationResponseDto<CompanyPaginationResponseDto>> PaginationAsync(CompanyPaginationResquestDto CompanyPaginationResquest, CancellationToken CancellationToken = default)
        {
            var Pagination = new PaginationResponseDto<CompanyPaginationResponseDto>(); 

            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand()){
                    Command.CommandText = "[Security].uspCompanyPagination";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RecordsTotal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@CompanyIDRegister", CompanyPaginationResquest.CompanyIDRegister);
                    Command.Parameters.AddWithValue("@TaxpayerTypeID", CompanyPaginationResquest.TaxpayerTypeID.HasValue ? CompanyPaginationResquest.TaxpayerTypeID.Value: DBNull.Value);
                    Command.Parameters.AddWithValue("@RubroID", CompanyPaginationResquest.RubroID.HasValue ? CompanyPaginationResquest.RubroID.Value:DBNull.Value );
                    Command.Parameters.AddWithValue("@CompanyDocumentNumber", CompanyPaginationResquest.CompanyDocumentNumber ?? "");
                    Command.Parameters.AddWithValue("@CompanySocialReason", CompanyPaginationResquest.CompanySocialReason ?? "");
                    Command.Parameters.AddWithValue("@StateID", CompanyPaginationResquest.StateID);
                    Command.Parameters.AddWithValue("@PageNumber", CompanyPaginationResquest.Parameters.PageNumber);
                    Command.Parameters.AddWithValue("@PageSize", CompanyPaginationResquest.Parameters.PageSize);
                    Command.Connection = Connection;

                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                                var Get = new CompanyPaginationResponseDto()
                                {
                                    CompanyID = Validation.SqlDBToInt32(ref DataReader, "CompanyID"),
                                    TaxpayerTypeName = Validation.SqlDBToString(ref DataReader, "TaxpayerTypeName"),
                                    CompanyDocumentNumber = Validation.SqlDBToString(ref DataReader, "CompanyDocumentNumber"),
                                    CompanySocialReason = Validation.SqlDBToString(ref DataReader, "CompanySocialReason"),
                                    RubroName = Validation.SqlDBToString(ref DataReader, "RubroName"),
                                    CountryName = Validation.SqlDBToString(ref DataReader, "CountryName"), 
                                    StateID = Validation.SqlDBToInt16(ref DataReader, "StateID"),
                                    CompanyLastUpdatedDateTime = Validation.SqlDBToDateTime(ref DataReader, "CompanyLastUpdatedDateTime"),
                                    CompanyLastUpdatedUserName = Validation.SqlDBToString(ref DataReader, "CompanyLastUpdatedUserName")
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
