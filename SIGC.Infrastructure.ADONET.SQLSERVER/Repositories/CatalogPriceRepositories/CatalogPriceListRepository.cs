using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.CatalogPrice;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPriceRepositories
{
    internal class CatalogPriceListRepository : ICatalogPriceListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogPriceListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<CatalogPriceListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default)
        {
            var List = new List<CatalogPriceListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogPriceList";
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
                            var Get = new CatalogPriceListResponseDto(
                                    EstablishmentID: Validation.SqlDBToInt32(ref DataReader, "EstablishmentID"),
                                    EstablishmentName: Validation.SqlDBToString(ref DataReader, "EstablishmentName"),
                                    CatalogPriceID: Validation.SqlDBToInt32(ref DataReader, "CatalogPriceID"),
                                    CatalogPresentationID: Validation.SqlDBToInt32(ref DataReader, "CatalogPresentationID"),
                                    CatalogPresentationName: Validation.SqlDBToString(ref DataReader, "CatalogPresentationName"),
                                    PriceTypeID: Validation.SqlDBToTinyint(ref DataReader, "PriceTypeID"),
                                    PriceTypeName: Validation.SqlDBToString(ref DataReader, "PriceTypeName"),
                                    CurrencyTypeID: Validation.SqlDBToTinyint(ref DataReader, "CurrencyTypeID"),
                                    CurrencyTypeName: Validation.SqlDBToString(ref DataReader, "CurrencyTypeName"),
                                    CatalogPriceAmount: Validation.SqlDBToDecimal(ref DataReader, "CatalogPriceAmount"),
                                    CatalogPriceIsTaxIncluded: Validation.SqlDBToBoolean(ref DataReader, "CatalogPriceIsTaxIncluded"),
                                    RecordStateID: Validation.SqlDBToTinyint(ref DataReader, "RecordStateID")
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
