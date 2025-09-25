using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IRolePermissionRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RolePermissionRepositories
{
    internal class RolePermissionCreateRepository : IRolePermissionCreateRepository
    {
        private readonly string ConnectionString;
        public RolePermissionCreateRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }

        public async Task<int> CreateAsync(RolePermission Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRolePermissionCreate";
                    Command.CommandType = CommandType.StoredProcedure;              
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                    Command.Parameters.AddWithValue("@RoleID", Model.RoleID);
                    Command.Parameters.AddWithValue("@PageID", Model.PageID);                  
                    Command.Parameters.AddWithValue("@PageActionID", (short)Model.PageActionID);
                    Command.Parameters.AddWithValue("@PageRoleCreatedDateTime", Model.PageRoleCreatedDateTime);           
                    Command.Connection = Connection;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);                     
                }
            }
            return RecordAffected;
        }
    }
}
