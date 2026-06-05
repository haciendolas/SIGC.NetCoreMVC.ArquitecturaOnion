using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Category;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
    internal class CategoryPaginationRepository : ICategoryPaginationRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CategoryPaginationRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<PaginationResponseDto<CategoryPaginationResponseDto>> PaginationAsync(CategoryPaginationRequestDto CategoryPaginationRequest, CancellationToken CancellationToken = default)
        {
            var Pagination = new PaginationResponseDto<CategoryPaginationResponseDto>();

            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCategoryPagination";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@RecordsTotal", SqlDbType.Int).Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", CategoryPaginationRequest.CompanyID);
                Command.Parameters.AddWithValue("@CategoryName", string.IsNullOrWhiteSpace(CategoryPaginationRequest.Parameters.Search) ? DBNull.Value : CategoryPaginationRequest.Parameters.Search);
                Command.Parameters.AddWithValue("@RecordStateID", CategoryPaginationRequest.RecordStateID.HasValue ? CategoryPaginationRequest.RecordStateID.Value : DBNull.Value);
                Command.Parameters.AddWithValue("@PageNumber", CategoryPaginationRequest.Parameters.PageNumber);
                Command.Parameters.AddWithValue("@PageSize", CategoryPaginationRequest.Parameters.PageSize);
                Command.Connection = Connection;

                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new CategoryPaginationResponseDto()
                            {
                                CategoryID = Validation.SqlDBToInt32(ref DataReader, "CategoryID"),
                                CategoryName = Validation.SqlDBToString(ref DataReader, "CategoryName"),
                                CategorySlug = Validation.SqlDBToString(ref DataReader, "CategorySlug"),
                                RecordStateID = Validation.SqlDBToTinyint(ref DataReader, "RecordStateID"),
                                CategoryLastUpdatedDateTime = Validation.SqlDBToDateTime(ref DataReader, "CategoryLastUpdatedDateTime"),
                                CategoryLastUpdatedUserID = Validation.SqlDBToInt32(ref DataReader, "CategoryLastUpdatedUserID"),
                                CategoryLastUpdatedUserName = Validation.SqlDBToString(ref DataReader, "CategoryLastUpdatedUserName"),
                                CategoryLastUpdatedUserFullName = Validation.SqlDBToString(ref DataReader, "CategoryLastUpdatedUserFullName"),
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
