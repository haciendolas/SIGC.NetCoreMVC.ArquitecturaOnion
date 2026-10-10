using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.CatalogTaxExemption;
using SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTaxExemptionRepositories
{
    internal class CatalogTaxExemptionListRepository : ICatalogTaxExemptionListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogTaxExemptionListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<CatalogTaxExemptionListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default)
        {
            var List = new List<CatalogTaxExemptionListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogTaxExemptionList";
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
                            var Get = new CatalogTaxExemptionListResponseDto(
                                    CatalogTaxExemptionID: Validation.SqlDBToInt32(ref DataReader, "CatalogTaxExemptionID"),
                                    CatalogTaxID: Validation.SqlDBToInt32(ref DataReader, "CatalogTaxID"),                                                                     
                                    EstablishmentName: Validation.SqlDBToString(ref DataReader, "EstablishmentName"),                                                                      
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
