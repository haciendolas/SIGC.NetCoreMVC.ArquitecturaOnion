using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Token;
using SIGC.DomainService.IRepositories.ITokenRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.TokenRepositories
{
    internal class TokenGetExpirationRepository(IServiceProvider ServiceProvider) : ITokenGetExpirationRepository
    {
        private readonly string ConnectionString = ServiceProvider.GetRequiredService<IOptions<AppDbContext>>().Value.ConnectionDBCommerce360;

        public async Task<TokenGetExpirationResponseDto?> GetExpirationAsync(TokenGetExpirationResquestDto TokenGetExpirationResquest, CancellationToken CancellationToken = default)
        {
            TokenGetExpirationResponseDto? Get = null;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspTokenGetExpiration";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", TokenGetExpirationResquest.UserID);
                    Command.Parameters.AddWithValue("@TokenRefreshRandom", TokenGetExpirationResquest.TokenRefreshRandom);
                    Command.Parameters.AddWithValue("@TokenExpirationDateTime", TokenGetExpirationResquest.TokenExpirationDateTime);
                    Command.Connection = Connection;
                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (DataReader.Read())
                            {
                                Get = new TokenGetExpirationResponseDto()
                                {
                                    TokenID = Validation.SqlDBToInt32(ref DataReader, "TokenID"),
                                    UserID = TokenGetExpirationResquest.UserID,
                                    TokenExpirationDateTime = TokenGetExpirationResquest.TokenExpirationDateTime
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
