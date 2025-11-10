using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyUpdateRepository : ICompanyUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CompanyUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public  async Task<int> UpdateAsync(Company Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "[Security].uspCompanyUpdate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@CompanyTradeName", Model.CompanyTradeName);
                Command.Parameters.AddWithValue("@CompanySocialReason", Model.CompanySocialReason);
                Command.Parameters.AddWithValue("@CompanyDocumentNumber", Model.CompanyDocumentNumber);
                Command.Parameters.AddWithValue("@CompanyBirthDate", Model.CompanyBirthDate);
                Command.Parameters.AddWithValue("@CountryID", Model.CountryID);
                Command.Parameters.AddWithValue("@CompanyAddress", string.IsNullOrWhiteSpace(Model.CompanyAddress) ? DBNull.Value : Model.CompanyAddress);
                Command.Parameters.AddWithValue("@CompanyCorporateEmail", string.IsNullOrWhiteSpace(Model.CompanyCorporateEmail) ? DBNull.Value : Model.CompanyCorporateEmail);
                Command.Parameters.AddWithValue("@CompanyMobile", string.IsNullOrWhiteSpace(Model.CompanyMobile) ? DBNull.Value : Model.CompanyMobile);
                Command.Parameters.AddWithValue("@CompanyPhone", string.IsNullOrWhiteSpace(Model.CompanyPhone) ? DBNull.Value : Model.CompanyPhone);
                Command.Parameters.AddWithValue("@CompanyLogo", string.IsNullOrWhiteSpace(Model.CompanyLogo) ? DBNull.Value : Model.CompanyLogo);
                Command.Parameters.AddWithValue("@TaxpayerTypeID", Model.TaxpayerTypeID);
                Command.Parameters.AddWithValue("@RubroID", Model.RubroID);
                Command.Parameters.AddWithValue("@StateID", (short)Model.StateID);
                Command.Parameters.AddWithValue("@CompanyUpdatedUserID", Model.CreatedBy);
                Command.Parameters.AddWithValue("@CompanyUpdatedDateTime", Model.CreatedDateTime);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);                
            }

            return RecordAffected;
        }
    }
}
