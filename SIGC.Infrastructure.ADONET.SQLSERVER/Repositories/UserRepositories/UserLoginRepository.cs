using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.User;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserRepositories
{
    internal class UserLoginRepository(IServiceProvider ServiceProvider) : IUserLoginRepository
    {
        private readonly string ConnectionString = ServiceProvider.GetRequiredService<IOptions<AppDbContext>>().Value.ConnectionDBCommerce360;
        private readonly string ConnectionString2 = ServiceProvider.GetRequiredService<IOptions<AppDbContext>>().Value.ConnectionDBContabilidad;
        public async Task<UserLoginResponseDto?> GetAsync(UserLoginRequestDto UserCredentials)
        {
            UserLoginResponseDto? Get = null;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspUserLogin";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CompanyDocumentNumber", UserCredentials.CompanyDocumentNumber);
                    Command.Parameters.AddWithValue("@UserName", UserCredentials.UserName);
                    Command.Parameters.AddWithValue("@UserPassword", UserCredentials.UserPassword);
                    Command.Connection = Connection;
                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync())
                    {
                        if (DataReader.HasRows) {
                            while (DataReader.Read())
                            {
                                Get = new UserLoginResponseDto()
                                {
                                    UserID = Validation.SqlDBToInt32(ref DataReader, "UserID"),
                                    UserFirstName = Validation.SqlDBToString(ref DataReader, "UserFirstName"),
                                    UserLastName = Validation.SqlDBToString(ref DataReader, "UserLastName"),
                                    UserMail = Validation.SqlDBToString(ref DataReader, "UserMail"),
                                    CompanyID = Validation.SqlDBToInt32(ref DataReader, "CompanyID"),
                                    CompanyDocumentNumber = UserCredentials.CompanyDocumentNumber,
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
