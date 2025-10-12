using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyVerifyDocumentNumberAndSocialReasonRepository : ICompanyVerifyDocumentNumberAndSocialReasonRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CompanyVerifyDocumentNumberAndSocialReasonRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> VerifyDocumentNumberAndSocialAsync(Company Model, CancellationToken CancellationToken = default)
        {
            string RetMsg = string.Empty;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken); 
            using (SqlCommand Command = new SqlCommand()){
                    Command.CommandText = "[Security].uspCompanyVerifyDocumentNumberAndSocialReason";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 25);
                    Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;                
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                    Command.Parameters.AddWithValue("@CompanyDocumentNumber", Model.CompanyDocumentNumber);
                    Command.Parameters.AddWithValue("@CompanySocialReason", Model.CompanySocialReason);
                    Command.Connection = Connection;
                    await Command.ExecuteNonQueryAsync(CancellationToken);
                    RetMsg = Command.Parameters["@RetMsg"].Value.ToString()!;
            }          
            return RetMsg;
        }
    }
}
