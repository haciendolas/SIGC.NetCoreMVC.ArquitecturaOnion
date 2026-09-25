using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Catalog;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogRepositories
{
    internal class CatalogGetRepository : ICatalogGetRepository
    {
        private readonly string ConnectionString;   
        private readonly ITransactionAccessor TransactionAccessor;
        private readonly IJsonSerializerService JsonSerializerService;
        public CatalogGetRepository(IOptions<AppDbContext> Options, IJsonSerializerService JsonSerializerService,
            ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;        
            this.TransactionAccessor = TransactionAccessor;
            this.JsonSerializerService = JsonSerializerService;
        }

        public async Task<CatalogGetResponseDto?> GetAsync(int CompanyID, int CatalogID, CancellationToken CancellationToken)
        {
            CatalogGetResponseDto? Get = null;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogGet";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Parameters.AddWithValue("@CatalogID", CatalogID);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (DataReader.Read())
                        {
                            Get = new CatalogGetResponseDto(                            
                                CatalogID : Validation.SqlDBToInt32(ref DataReader, "CatalogID"),
                                CatalogTypeID: Validation.SqlDBToTinyint(ref DataReader, "CatalogTypeID"),
                                CategoryID: Validation.SqlDBToInt32(ref DataReader, "CategoryID"),
                                CatalogCode: Validation.SqlDBToString(ref DataReader, "CatalogCode"),
                                CatalogSlug: Validation.SqlDBToString(ref DataReader, "CatalogSlug"),
                                CatalogName: Validation.SqlDBToString(ref DataReader, "CatalogName"),
                                SaleConditionID: Validation.SqlDBToTinyint(ref DataReader, "SaleConditionID"),
                                ManufacturerID: Validation.SqlDBToInt32(ref DataReader, "ManufacturerID"),
                                BrandID: Validation.SqlDBToInt32(ref DataReader, "BrandID"),
                                PharmaceuticalFormID: Validation.SqlDBToInt16(ref DataReader, "PharmaceuticalFormID"),
                                CatalogBrandType: Validation.SqlDBToString(ref DataReader, "CatalogBrandType"),
                                CatalogConcentration: Validation.SqlDBToString(ref DataReader, "CatalogConcentration"),
                                CatalogHasVariants: Validation.SqlDBToBoolean(ref DataReader, "CatalogHasVariants"),
                                CatalogDescription: Validation.SqlDBToString(ref DataReader, "CatalogDescription"),
                                CatalogImage: Validation.SqlDBToString(ref DataReader, "CatalogImage"),
                                RecordStateID : Validation.SqlDBToTinyint(ref DataReader, "RecordStateID"),
                                CatalogUrl: null,
                                TherapeuticActionIDs: JsonSerializerService.Deserialize<List<short>>(Validation.SqlDBToString(ref DataReader, "TherapeuticActionIDs")),
                                ActiveIngredientIDs: JsonSerializerService.Deserialize<List<int>>(Validation.SqlDBToString(ref DataReader, "ActiveIngredientIDs"))
                            );
                        }
                    }
                }
            }
            return Get;
        }
    }
}