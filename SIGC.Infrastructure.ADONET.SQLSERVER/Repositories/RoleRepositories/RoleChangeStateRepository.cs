using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories
{
    internal class RoleChangeStateRepository : IRoleChangeStateRepository
    {
        private readonly string ConnectionString;
        public RoleChangeStateRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }

        public async Task<int> ChangeStateAsync(Role Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRoleChangeState";
                    Command.CommandType = CommandType.StoredProcedure;            
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                    Command.Parameters.AddWithValue("@RoleID", Model.RoleID); 
                    Command.Parameters.AddWithValue("@StateID", (short)Model.StateID);
                    Command.Parameters.AddWithValue("@RoleCreatedUserID", Model.CreatedBy);
                    Command.Parameters.AddWithValue("@RoleCreatedDateTime", Model.CreatedDateTime);
                    Command.Connection = Connection;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);                    
                }
            }
            return RecordAffected;
        }
    }
}