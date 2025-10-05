using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainService.IRepositories.IRolePermissionRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RolePermissionRepositories
{
    internal class RolePermissionDeleteRepository : IRolePermissionDeleteRepository
    {
        private readonly string ConnectionString;
        public RolePermissionDeleteRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }
        public async Task<int> DeleteAsync(int RoleID, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRolePermissionDelete";
                    Command.CommandType = CommandType.StoredProcedure;   
                    Command.Parameters.AddWithValue("@RoleID", RoleID); 
                    Command.Connection = Connection;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                }
            }
            return RecordAffected;
        }
    }
}