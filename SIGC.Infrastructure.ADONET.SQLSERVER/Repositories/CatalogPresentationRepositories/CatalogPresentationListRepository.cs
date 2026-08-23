using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.CatalogPresentation;
using SIGC.DomainService.IRepositories.ICatalogPresentationRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogPresentationRepositories
{
    internal class CatalogPresentationListRepository : ICatalogPresentationListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogPresentationListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<CatalogPresentationListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default)
        {
            var List = new List<CatalogPresentationListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogPresentationList";
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
                            var Get = new CatalogPresentationListResponseDto(
                                    CatalogVariantID: Validation.SqlDBToInt32(ref DataReader, "CatalogVariantID"),
                                    CatalogVariantName: Validation.SqlDBToString(ref DataReader, "CatalogVariantName"),                           
                                    CatalogPresentationID: Validation.SqlDBToInt32(ref DataReader, "CatalogPresentationID"),
                                    CatalogPresentationName: Validation.SqlDBToString(ref DataReader, "CatalogPresentationName")
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
