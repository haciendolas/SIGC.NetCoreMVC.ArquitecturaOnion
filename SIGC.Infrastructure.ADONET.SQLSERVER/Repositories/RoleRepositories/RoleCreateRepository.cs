using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories
{
    internal class RoleCreateRepository : IRoleCreateRepository
    {
        private readonly string ConnectionString;
        public RoleCreateRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }

        public async Task<int> CreateAsync(Role Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRoleCreate";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RoleID", SqlDbType.Int);
                    Command.Parameters["@RoleID"].Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                    Command.Parameters.AddWithValue("@RoleCode", Model.RoleCode);
                    Command.Parameters.AddWithValue("@RoleName", Model.RoleName);
                    Command.Parameters.AddWithValue("@RoleDescription", string.IsNullOrWhiteSpace(Model.RoleDescription) ? DBNull.Value: Model.RoleDescription);
                    Command.Parameters.AddWithValue("@StateID", (short)Model.StateID);
                    Command.Parameters.AddWithValue("@RoleCreatedUserID", Model.CreatedBy);
                    Command.Parameters.AddWithValue("@RoleCreatedDateTime", Model.CreatedDateTime);
                    Command.Connection = Connection;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                    Model.RoleID = Convert.ToInt32(Command.Parameters["@RoleID"].Value);
                }
            }
            return RecordAffected;
        }
    }
}
