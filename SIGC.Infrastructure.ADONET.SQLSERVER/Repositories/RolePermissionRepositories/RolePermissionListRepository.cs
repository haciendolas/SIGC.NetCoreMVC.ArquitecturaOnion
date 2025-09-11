using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.RolePermission;
using SIGC.DomainService.IRepositories.IRolePermissionRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RolePermissionRepositories
{
    internal class RolePermissionListRepository : IRolePermissionListRepository
    {
        private readonly string ConnectionString;
        public RolePermissionListRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }

        public async Task<List<RolePermissionListResponseDto>> ListAsync(int UserID, int CompanyID, CancellationToken CancellationToken = default)
        {
            var List = new List<RolePermissionListResponseDto>();
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRolePermissionList";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", UserID);
                    Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    Command.Connection = Connection;
                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                               var Get = new RolePermissionListResponseDto()
                                {
                                   PageID = Validation.SqlDBToInt32(ref DataReader, "PageID"),
                                   PageParentID = Validation.SqlDBToInt32(ref DataReader, "PageParentID"),
                                   PageHierarchy = Validation.SqlDBToString(ref DataReader, "PageHierarchy"),
                                   PageName = Validation.SqlDBToString(ref DataReader, "PageName"),
                                   PageUrlName = Validation.SqlDBToString(ref DataReader, "PageUrlName"),
                                   PageIconName = Validation.SqlDBToString(ref DataReader, "PageIconName"),
                                   PageOrder = Validation.SqlDBToInt16(ref DataReader, "PageOrder")
                               };
                                List.Add(Get);
                            }
                        }
                    }
                }
            }
            return List;
        }
    }
}
