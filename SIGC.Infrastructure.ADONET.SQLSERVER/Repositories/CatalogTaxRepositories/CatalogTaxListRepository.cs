using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.CatalogTax;
using SIGC.DomainService.IRepositories.ICatalogTaxRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTaxRepositories
{
    internal class CatalogTaxListRepository : ICatalogTaxListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogTaxListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<CatalogTaxListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default)
        {
            var List = new List<CatalogTaxListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogTaxList";
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
                            var Get = new CatalogTaxListResponseDto(
                                    CatalogTaxID: Validation.SqlDBToInt32(ref DataReader, "CatalogTaxID"), 
                                    TaxID: Validation.SqlDBToInt16(ref DataReader, "TaxID"),
                                    TaxName: Validation.SqlDBToString(ref DataReader, "TaxName"),
                                    TaxValor: Validation.SqlDBToDecimal(ref DataReader, "TaxValor"),
                                    CalculationTypeName: Validation.SqlDBToString(ref DataReader, "CalculationTypeName"),
                                    TaxAffectationTypeID: Validation.SqlDBToTinyint(ref DataReader, "TaxAffectationTypeID"),
                                    TaxAffectationTypeName: Validation.SqlDBToString(ref DataReader, "TaxAffectationTypeName"),                                    
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
