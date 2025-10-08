using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Role;
using SIGC.DomainModel.Dtos.RolePermission;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories
{
    internal class RoleGetRepository : IRoleGetRepository
    {
        private readonly string ConnectionString;
        private readonly IJsonSerializerService JsonSerializerService;
        private readonly ITransactionAccessor TransactionAccessor;
        public RoleGetRepository(IOptions<AppDbContext> Options, IJsonSerializerService JsonSerializerService,
            ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.JsonSerializerService = JsonSerializerService;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<RoleGetResponseDto?> GetAsync(int RoleID, CancellationToken CancellationToken = default)
        {
            RoleGetResponseDto? Get = null;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction; 
            using (SqlCommand Command = new SqlCommand()){
                    Command.CommandText = "Security.uspRoleGet";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@RoleID", RoleID);              
                    Command.Connection = Connection;
                    Command.Transaction = Transaction;
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
            return Get;
        }
    }
}