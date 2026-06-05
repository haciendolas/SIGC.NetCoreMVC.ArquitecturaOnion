using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Role;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories
{
    internal class RoleListRepository : IRoleListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public RoleListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<RoleListResponseDto>> ListAsync(int CompanyID,CancellationToken CancellationToken = default)
        {
            var List = new List<RoleListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "[Security].uspRoleList";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", CompanyID);             
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new RoleListResponseDto()
                            {
                                RoleID = Validation.SqlDBToInt32(ref DataReader, "RoleID"),
                                RoleCode = Validation.SqlDBToString(ref DataReader, "RoleCode"),
                                RoleName = Validation.SqlDBToString(ref DataReader, "RoleName")                       
                            };
                            List.Add(Get);
                        }
                    }
                }
            }
            return List;
        }
    }
}