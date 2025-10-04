using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Role;
using SIGC.DomainModel.Dtos.RolePermission;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories
{
    internal class RoleGetRepository : IRoleGetRepository
    {
        private readonly string ConnectionString;
        private readonly IJsonSerializerService JsonSerializerService;
        public RoleGetRepository(IOptions<AppDbContext> Options, IJsonSerializerService JsonSerializerService)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.JsonSerializerService = JsonSerializerService;
        }

        public async Task<RoleGetResponseDto?> GetAsync(int RoleID, CancellationToken CancellationToken = default)
        {
            RoleGetResponseDto? Get = null;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRoleGet";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@RoleID", RoleID);              
                    Command.Connection = Connection;
                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (DataReader.Read())
                            {
                                Get = new RoleGetResponseDto()
                                {
                                    RoleID = Validation.SqlDBToInt32(ref DataReader, "RoleID"),
                                    RoleCode = Validation.SqlDBToString(ref DataReader, "RoleCode"),
                                    RoleName = Validation.SqlDBToString(ref DataReader, "RoleName"),
                                    RoleDescription = Validation.SqlDBToString(ref DataReader, "RoleDescription"),
                                    StateID = Validation.SqlDBToInt16(ref DataReader, "StateID"),
                                    RolePermission = JsonSerializerService.Deserialize<List<RolePermissionGetResponseDto>>(Validation.SqlDBToString(ref DataReader, "RolePermission"))
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