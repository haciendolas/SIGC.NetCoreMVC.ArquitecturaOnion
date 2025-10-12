using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainModel.Dtos.PageCompany;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyGetRepository : ICompanyGetRepository
    {
        private readonly string ConnectionString;
        private readonly IJsonSerializerService JsonSerializerService;
        private readonly ITransactionAccessor TransactionAccessor;
        public CompanyGetRepository(IOptions<AppDbContext> Options, IJsonSerializerService JsonSerializerService,
            ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.JsonSerializerService = JsonSerializerService;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<CompanyGetResponseDto?> GetAsync(int CompanyID, CancellationToken CancellationToken = default)
        {
            CompanyGetResponseDto? Get = null;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);            
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "[Security].uspCompanyGet";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Connection = Connection;               
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (DataReader.Read())
                        {
                            Get = new CompanyGetResponseDto()
                            {
                                CompanyID = Validation.SqlDBToInt32(ref DataReader, "CompanyID"),
                                CompanyTradeName = Validation.SqlDBToString(ref DataReader, "CompanyTradeName"),
                                CompanySocialReason = Validation.SqlDBToString(ref DataReader, "CompanySocialReason"),
                                CompanyDocumentNumber = Validation.SqlDBToString(ref DataReader, "CompanyDocumentNumber"),
                                CompanyBirthDate = Validation.SqlDBToDateTime(ref DataReader, "CompanyBirthDate"),
                                CountryID = Validation.SqlDBToInt32(ref DataReader, "CountryID"),
                                CompanyAddress = Validation.SqlDBToString(ref DataReader, "CompanyAddress"),
                                TaxpayerTypeID = Validation.SqlDBToInt16(ref DataReader, "TaxpayerTypeID"),
                                SectorID = Validation.SqlDBToInt16(ref DataReader, "SectorID"),
                                CompanyCorporateEmail = Validation.SqlDBToString(ref DataReader, "CompanyCorporateEmail"),
                                CompanyMobile = Validation.SqlDBToString(ref DataReader, "CompanyMobile"),
                                CompanyPhone = Validation.SqlDBToString(ref DataReader, "CompanyPhone"),
                                CompanyLogo = Validation.SqlDBToString(ref DataReader, "CompanyLogo"),
                                StateID = Validation.SqlDBToInt16(ref DataReader, "StateID"),
                                PageCompany = JsonSerializerService.Deserialize<List<PageCompanyGetResponseDto>>(Validation.SqlDBToString(ref DataReader, "PageCompany"))
                            };
                        }
                    }
                }
            }
            return Get;
        }
    }
}
