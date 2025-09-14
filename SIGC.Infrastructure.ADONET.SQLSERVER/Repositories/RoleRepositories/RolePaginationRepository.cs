using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainModel.Dtos.Role;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories
{
    internal class RolePaginationRepository : IRolePaginationRepository
    {
        private readonly string ConnectionString;
        public RolePaginationRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }

        public async Task<PaginationResult<RolePaginationResponseDto>> PaginationAsync(RolePaginationResquestDto RolePaginationResquest, CancellationToken CancellationToken = default)
        {
            var Pagination = new PaginationResult<RolePaginationResponseDto>();          
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspRolePagination";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CompanyID", RolePaginationResquest.CompanyID);
                    Command.Parameters.AddWithValue("@RoleName", RolePaginationResquest.Parameters.Search);
                    Command.Parameters.AddWithValue("@StateID", RolePaginationResquest.StateID);
                    Command.Parameters.AddWithValue("@PageNumber", RolePaginationResquest.Parameters.PageNumber);
                    Command.Parameters.AddWithValue("@PageSize", RolePaginationResquest.Parameters.PageSize);
                    Command.Connection = Connection;

                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                                var Get = new RolePaginationResponseDto()
                                {
                                    RoleID = Validation.SqlDBToInt32(ref DataReader, "RoleID"),                                   
                                    RoleCode = Validation.SqlDBToString(ref DataReader, "RoleCode"),
                                    RoleName = Validation.SqlDBToString(ref DataReader, "RoleName"),
                                    RoleDescription = Validation.SqlDBToString(ref DataReader, "RoleDescription"),
                                    StateID = Validation.SqlDBToInt16(ref DataReader, "StateID"),
                                    RoleLastUpdatedDateTime = Validation.SqlDBToDateTime(ref DataReader, "RoleLastUpdatedDateTime"),
                                    RoleLastUpdatedUserName = Validation.SqlDBToString(ref DataReader, "RoleLastUpdatedUserName")                                
                                };
                                Pagination.Items.Add(Get);
                                Pagination.Filtered = Validation.SqlDBToInt32(ref DataReader, "RecordsFiltered");
                                Pagination.Total = Validation.SqlDBToInt32(ref DataReader, "RecordsTotal");
                            }                          
                        }
                    }
                }
            }
            return Pagination;
        }
    }
}
