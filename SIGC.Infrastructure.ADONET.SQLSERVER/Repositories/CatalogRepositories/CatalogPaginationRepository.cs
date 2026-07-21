using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Catalog; 
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.ICatalogRepositories; 
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogRepositories
{
    internal class CatalogPaginationRepository : ICatalogPaginationRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CatalogPaginationRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<PaginationResponseDto<CatalogPaginationResponseDto>> PaginationAsync(CatalogPaginationRequestDto CatalogPaginationRequest, CancellationToken CancellationToken = default)
        {          
            var Pagination = new PaginationResponseDto<CatalogPaginationResponseDto>();
        
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Product.uspCatalogPagination";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RecordsTotal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@CompanyID", CatalogPaginationRequest.CompanyID);
                    Command.Parameters.AddWithValue("@CatalogTypeID", CatalogPaginationRequest.CatalogTypeID.HasValue ? CatalogPaginationRequest.CatalogTypeID.Value : DBNull.Value);
                    Command.Parameters.AddWithValue("@CatalogName", string.IsNullOrWhiteSpace(CatalogPaginationRequest.Parameters.Search) ? DBNull.Value : CatalogPaginationRequest.Parameters.Search);
                    Command.Parameters.AddWithValue("@RecordStateID", CatalogPaginationRequest.RecordStateID.HasValue ? CatalogPaginationRequest.RecordStateID.Value : DBNull.Value);
                    Command.Parameters.AddWithValue("@CategoryID", CatalogPaginationRequest.CategoryID.HasValue ? CatalogPaginationRequest.CategoryID.Value : DBNull.Value);
                    Command.Parameters.AddWithValue("@ManufacturerID", CatalogPaginationRequest.ManufacturerID.HasValue ? CatalogPaginationRequest.ManufacturerID.Value : DBNull.Value);
                    Command.Parameters.AddWithValue("@BrandID", CatalogPaginationRequest.BrandID.HasValue ? CatalogPaginationRequest.BrandID.Value : DBNull.Value);
                    Command.Parameters.AddWithValue("@PageNumber", CatalogPaginationRequest.Parameters.PageNumber);
                    Command.Parameters.AddWithValue("@PageSize", CatalogPaginationRequest.Parameters.PageSize);
                    Command.Connection = Connection;

                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                                var Get = new CatalogPaginationResponseDto()
                                {
                                    CatalogID = Validation.SqlDBToInt32(ref DataReader, "CatalogID"),
                                    CatalogName = Validation.SqlDBToString(ref DataReader, "CatalogName"),
                                    CatalogDescription = Validation.SqlDBToString(ref DataReader, "CatalogDescription"),
                                    CatalogTypeName = Validation.SqlDBToString(ref DataReader, "CatalogTypeName"),

                                    CategoryName = Validation.SqlDBToString(ref DataReader, "CategoryName"),
                                    CatalogVariantName = Validation.SqlDBToString(ref DataReader, "CatalogVariantName"),
                                    UnitMeasureName = Validation.SqlDBToString(ref DataReader, "UnitMeasureName"),
                                    PresentationName = Validation.SqlDBToString(ref DataReader, "PresentationName"),
                                    BrandName = Validation.SqlDBToString(ref DataReader, "BrandName"),
                                    ManufacturerName = Validation.SqlDBToString(ref DataReader, "ManufacturerName"),
                                    ActiveIngredient = Validation.SqlDBToString(ref DataReader, "ActiveIngredient"),
                                    TherapeuticAction = Validation.SqlDBToString(ref DataReader, "TherapeuticAction"),                                   
                                    RecordStateID = Validation.SqlDBToTinyint(ref DataReader, "RecordStateID"),
                                    CatalogLastUpdatedDateTime = Validation.SqlDBToDateTime(ref DataReader, "CatalogLastUpdatedDateTime"),                                    
                                    CatalogLastUpdatedUserName = Validation.SqlDBToString(ref DataReader, "CatalogLastUpdatedUserName")                                   
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
