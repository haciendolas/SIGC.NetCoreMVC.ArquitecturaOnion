using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ITokenRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.TokenRepositories
{
    internal class TokenCreateRepository(IServiceProvider ServiceProvider) : ITokenCreateRepository
    {
        private readonly string ConnectionString = ServiceProvider.GetRequiredService<IOptions<AppDbContext>>().Value.ConnectionDBCommerce360;
  
        public async Task<int> CreateAsync(Token Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspTokenCreate";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@TokenID", SqlDbType.Int);
                    Command.Parameters["@TokenID"].Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@UserID", Model.UserID);
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                    Command.Parameters.AddWithValue("@TokenRefreshRandom", Model.TokenRefreshRandom);
                    Command.Parameters.AddWithValue("@TokenSessionJson",string.IsNullOrWhiteSpace(Model.TokenSessionJson) ? DBNull.Value : Model.TokenSessionJson);
                    Command.Parameters.AddWithValue("@TokenAccessJWT", string.IsNullOrWhiteSpace(Model.TokenAccessJWT) ? DBNull.Value : Model.TokenAccessJWT);
                    Command.Parameters.AddWithValue("@TokenCreateDateTime", Model.TokenCreateDateTime);
                    Command.Parameters.AddWithValue("@TokenExpirationRandomDateTime", Model.TokenExpirationRandomDateTime);
                    Command.Parameters.AddWithValue("@TokenExpirationJWTDateTime", Model.TokenExpirationJWTDateTime);
                    Command.Connection = Connection;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                    Model.TokenID = Convert.ToInt32(Command.Parameters["@TokenID"].Value);                    
                }
            }
            return RecordAffected;
        } 
    }
}