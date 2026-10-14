using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.CatalogStock;
using SIGC.DomainService.IRepositories.ICatalogStockRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogStockRepositories
{
    internal class CatalogStockListRepository : ICatalogStockListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogStockListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<CatalogStockListResponseDto>> ListAsync(int CompanyID,int CatalogID, int? EstablishmentID, CancellationToken CancellationToken = default)
        {
            var List = new List<CatalogStockListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogStockList";
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Parameters.AddWithValue("@CatalogID", CatalogID);
                Command.Parameters.AddWithValue("@EstablishmentID", EstablishmentID.HasValue ? EstablishmentID :DBNull.Value);
                Command.CommandType = CommandType.StoredProcedure;           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new CatalogStockListResponseDto(
                                   CatalogStockID: Validation.SqlDBToInt32(ref DataReader, "CatalogStockID"),
                                   CatalogVariantID: Validation.SqlDBToInt32(ref DataReader, "CatalogVariantID"),
                                   CatalogVariantName: Validation.SqlDBToString(ref DataReader, "CatalogVariantName"),
                                   EstablishmentID: Validation.SqlDBToInt32(ref DataReader, "EstablishmentID"),
                                   EstablishmentName: Validation.SqlDBToString(ref DataReader, "EstablishmentName"),
                                   WarehouseID: Validation.SqlDBToInt32(ref DataReader, "WarehouseID"),
                                   WarehouseCode: Validation.SqlDBToString(ref DataReader, "WarehouseCode"),
                                   WarehouseName: Validation.SqlDBToString(ref DataReader, "WarehouseName"),
                                   CatalogStockMinimumQuantity: Validation.SqlDBToDecimal(ref DataReader, "CatalogStockMinimumQuantity"),
                                   CatalogStockMaximumQuantity: Validation.SqlDBToDecimal(ref DataReader, "CatalogStockMaximumQuantity"),
                                   CatalogStockCurrentQuantity: Validation.SqlDBToDecimal(ref DataReader, "CatalogStockCurrentQuantity")                               
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
