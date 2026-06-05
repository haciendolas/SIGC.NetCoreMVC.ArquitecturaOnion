using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserRepositories
{
    internal class UserVerifyNameAndMailRepository : IUserVerifyNameAndMailRepository
    {
        private readonly string ConnectionString;
        public UserVerifyNameAndMailRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }

        public async Task<string> VerifyNameAndMailAsync(User Model, CancellationToken CancellationToken = default)
        {
            string RetMsg = string.Empty;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "[Security].uspUserVerifyNameAndMail";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RetMsg", SqlDbType.VarChar,20);
                    Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@UserID", Model.UserId);               
                    Command.Parameters.AddWithValue("@UserName", Model.UserName);
                    Command.Parameters.AddWithValue("@UserMail",string.IsNullOrWhiteSpace(Model.UserMail) ? DBNull.Value : Model.UserMail); 
                    Command.Connection = Connection;
                    await Command.ExecuteNonQueryAsync(CancellationToken);
                    RetMsg =Command.Parameters["@RetMsg"].Value.ToString()!;
                }
            }
            return RetMsg;
        }
    }
}