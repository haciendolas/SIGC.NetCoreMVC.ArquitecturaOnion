using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Role;
using SIGC.DomainModel.Dtos.RolePermission;
using SIGC.DomainModel.Dtos.UserCompany;
using SIGC.DomainService.IRepositories.IUserCompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserCompanyRepositories
{
    internal class UserCompanyGetRepository : IUserCompanyGetRepository
    {
        private readonly string ConnectionString;
        private readonly IJsonSerializerService JsonSerializerService;
        private readonly ITransactionAccessor TransactionAccessor;
        public UserCompanyGetRepository(IOptions<AppDbContext> Options, IJsonSerializerService JsonSerializerService,
            ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.JsonSerializerService = JsonSerializerService;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<UserCompanyGetResponseDto?> GetAsync(int UserID, int CompanyID, CancellationToken CancellationToken = default)
        {
            UserCompanyGetResponseDto? Get = null;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "[Security].uspUserCompanyGet";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@UserID", UserID);
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (DataReader.Read())
                        {
                            Get = new UserCompanyGetResponseDto()
                            {
                                UserID = Validation.SqlDBToInt32(ref DataReader, "UserID"),
                                UserFirstName = Validation.SqlDBToString(ref DataReader, "UserFirstName"),
                                UserLastName = Validation.SqlDBToString(ref DataReader, "UserLastName"),
                                UserName = Validation.SqlDBToString(ref DataReader, "UserName"),
                                UserPassword = Validation.SqlDBToString(ref DataReader, "UserPassword"),
                                UserMail = Validation.SqlDBToString(ref DataReader, "UserMail"),
                                UserPhoto = Validation.SqlDBToString(ref DataReader, "UserPhoto"), 
                                StateID = Validation.SqlDBToInt16(ref DataReader, "StateID"), 
                                RoleIDs = (Validation.SqlDBToString(ref DataReader, "RoleIDConcat")).Split(',',StringSplitOptions.RemoveEmptyEntries)
                                                                                                     .Where(p => int.TryParse(p,out int n))
                                                                                                     .Select(int.Parse)
                                                                                                     .ToList()
                            };
                        }
                    }
                }
            }
            return Get;
        }
    }
}
