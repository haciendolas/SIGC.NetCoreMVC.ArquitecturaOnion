using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Auth;
using SIGC.DomainService.IRepositories.IAuthRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.AuthRepositories
{
    internal class AuthGetRepository(IServiceProvider ServiceProvider) : IAuthGetRepository
    {
        private readonly string ConnectionString = ServiceProvider.GetRequiredService<IOptions<AppDbContext>>().Value.ConnectionDBCommerce360;        
        public async Task<AuthLoginResponseDto?> GetAsync(int UserID,int CompanyID, CancellationToken CancellationToken)
        {
            AuthLoginResponseDto? Get = null;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspAuthGet";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", UserID);
                    Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    Command.Connection = Connection;
                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows) {
                            while (DataReader.Read())
                            {
                                Get = new AuthLoginResponseDto()
                                {
                                    UserID = Validation.SqlDBToInt32(ref DataReader, "UserID"),
                                    UserName = Validation.SqlDBToString(ref DataReader, "UserName"),
                                    UserFirstName = Validation.SqlDBToString(ref DataReader, "UserFirstName"),
                                    UserLastName = Validation.SqlDBToString(ref DataReader, "UserLastName"),
                                    UserMail = Validation.SqlDBToString(ref DataReader, "UserMail"),
                                    CompanyID = Validation.SqlDBToInt32(ref DataReader, "CompanyID"),
                                    CompanyDocumentNumber = Validation.SqlDBToString(ref DataReader, "CompanyDocumentNumber"),                                    
                                    CompanyTradeName = Validation.SqlDBToString(ref DataReader, "CompanyTradeName"),
                                    CompanySocialReason = Validation.SqlDBToString(ref DataReader, "CompanySocialReason"),
                                    StateID = Validation.SqlDBToInt16(ref DataReader, "StateID")
                                };
                            }
                        }
                    }
                }
            }
            return Get;
        }
    }
}
