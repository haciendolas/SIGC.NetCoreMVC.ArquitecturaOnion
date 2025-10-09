using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyListRepository : ICompanyListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CompanyListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<CompanyListResponseDto>> ListAsync(int CompanyIDRegister, CancellationToken CancellationToken = default)
        {
            var List = new List<CompanyListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken); 
            using (SqlCommand Command = new SqlCommand()){
                    Command.CommandText = "Security.uspCompanyList";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CompanyIDRegister", CompanyIDRegister);
                    Command.Connection = Connection;
                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                                var Get = new CompanyListResponseDto()
                                {
                                    CompanyID = Validation.SqlDBToInt32(ref DataReader, "CompanyID"),                              
                                    CompanyDocumentNumber = Validation.SqlDBToString(ref DataReader, "CompanyDocumentNumber"),
                                    CompanySocialReason = Validation.SqlDBToString(ref DataReader, "CompanySocialReason") 
                                };
                                List.Add(Get);
                            }
                        }
                    }
            }         
            return List;
        }
    }
}