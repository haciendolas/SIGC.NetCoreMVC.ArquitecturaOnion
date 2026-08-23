using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Tax;
using SIGC.DomainService.IRepositories.ITaxRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.TaxRepositories
{
    internal class TaxListRepository : ITaxListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public TaxListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<TaxListResponseDto>> ListAsync(int CountryID,CancellationToken CancellationToken = default)
        {
            var List = new List<TaxListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Accounting.uspTaxList";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CountryID", CountryID);
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new TaxListResponseDto(
                                    TaxID: Validation.SqlDBToInt16(ref DataReader, "TaxID"),
                                    TaxCode: Validation.SqlDBToString(ref DataReader, "TaxCode"),
                                    TaxName: Validation.SqlDBToString(ref DataReader, "TaxName"),
                                    TaxValor: Validation.SqlDBToDecimal(ref DataReader, "TaxValor")
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
