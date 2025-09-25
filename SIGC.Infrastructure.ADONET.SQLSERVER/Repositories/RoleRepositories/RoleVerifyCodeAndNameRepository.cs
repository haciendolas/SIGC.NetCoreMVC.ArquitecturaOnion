using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories
{
    internal class RoleVerifyCodeAndNameRepository : IRoleVerifyCodeAndNameRepository
    {
        private readonly string ConnectionString;
        public RoleVerifyCodeAndNameRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }

        public async Task<string> VerifyCodeAndNameAsync(Role Model, CancellationToken CancellationToken = default)
        {
            string RetMsg = string.Empty;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRoleVerifyCodeAndName";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RetMsg", SqlDbType.VarChar,11);
                    Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@RoleID", Model.RoleID);
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                    Command.Parameters.AddWithValue("@RoleCode", Model.RoleCode);
                    Command.Parameters.AddWithValue("@RoleName", Model.RoleName); 
                    Command.Connection = Connection;
                    await Command.ExecuteNonQueryAsync(CancellationToken);
                    RetMsg =Command.Parameters["@RetMsg"].Value.ToString()!;
                }
            }
            return RetMsg;
        }
    }
}