using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.CatalogVariant;
using SIGC.DomainService.IRepositories.ICatalogVariantRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogVariantRepositories
{
    internal class CatalogVariantListRepository : ICatalogVariantListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        private readonly IJsonSerializerService JsonSerializerService;
        public CatalogVariantListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor,
              IJsonSerializerService jsonSerializerService)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
            this.JsonSerializerService = jsonSerializerService;
        }

        public async Task<List<CatalogVariantListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default)
        {
            var List = new List<CatalogVariantListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogVariantList";
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Parameters.AddWithValue("@CatalogID", CatalogID);
                Command.CommandType = CommandType.StoredProcedure;           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new CatalogVariantListResponseDto(
                                    CatalogVariantID: Validation.SqlDBToInt32(ref DataReader, "CatalogVariantID"),
                                    CatalogVariantName: Validation.SqlDBToString(ref DataReader, "CatalogVariantName"),
                                    CatalogVariantSKU: Validation.SqlDBToString(ref DataReader, "CatalogVariantSKU"),
                                    CatalogVariantStateID: Validation.SqlDBToTinyint(ref DataReader, "CatalogVariantStateID"),
                                    CatalogPresentationID: Validation.SqlDBToInt32(ref DataReader, "CatalogPresentationID"),
                                    UnitMeasureID: Validation.SqlDBToInt32(ref DataReader, "UnitMeasureID"),
                                    UnitMeasureName: Validation.SqlDBToString(ref DataReader, "UnitMeasureName"),
                                    PresentationID: Validation.SqlDBToInt32(ref DataReader, "PresentationID"),
                                    PresentationName: Validation.SqlDBToString(ref DataReader, "PresentationName"),
                                    CatalogPresentationIsDefault: Validation.SqlDBToBoolean(ref DataReader, "CatalogPresentationIsDefault"),
                                    CatalogPresentationEquivalence: Validation.SqlDBToDecimal(ref DataReader, "CatalogPresentationEquivalence"),
                                    CatalogPresentationSKU: Validation.SqlDBToString(ref DataReader, "CatalogPresentationSKU"),
                                    CatalogPresentationBarcode: Validation.SqlDBToString(ref DataReader, "CatalogPresentationBarcode"),
                                    CatalogPresentationStateID: Validation.SqlDBToTinyint(ref DataReader, "CatalogPresentationStateID"),
                                    CatalogVariantValues:JsonSerializerService.Deserialize<List<CatalogVariantValueListResponseDto>>(Validation.SqlDBToString(ref DataReader, "CatalogVariantValues"))
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
