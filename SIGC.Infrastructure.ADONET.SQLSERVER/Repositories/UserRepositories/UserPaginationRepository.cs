using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainModel.Dtos.User;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserRepositories
{
    internal class UserPaginationRepository : IUserPaginationRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public UserPaginationRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<PaginationResponseDto<UserPaginationResponseDto>> PaginationAsync(UserPaginationRequestDto UserPaginationRequest, CancellationToken CancellationToken = default)
        {        
            var Pagination = new PaginationResponseDto<UserPaginationResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand()){
                    Command.CommandText = "[Security].uspUserPagination";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@RecordsTotal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Command.Parameters.AddWithValue("@CompanyID", UserPaginationRequest.CompanyID);
                    Command.Parameters.AddWithValue("@UserFullName", string.IsNullOrWhiteSpace(UserPaginationRequest.UserFullName) ? DBNull.Value : UserPaginationRequest.UserFullName);
                    Command.Parameters.AddWithValue("@UserName", string.IsNullOrWhiteSpace(UserPaginationRequest.Parameters.Search) ? DBNull.Value : UserPaginationRequest.Parameters.Search);
                    Command.Parameters.AddWithValue("@StateID", UserPaginationRequest.StateID);
                    Command.Parameters.AddWithValue("@PageNumber", UserPaginationRequest.Parameters.PageNumber);
                    Command.Parameters.AddWithValue("@PageSize", UserPaginationRequest.Parameters.PageSize);
                    Command.Connection = Connection;

                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                                var Get = new UserPaginationResponseDto()
                                {
                                    UserID = Validation.SqlDBToInt32(ref DataReader, "UserID"),
                                    UserFirstName = Validation.SqlDBToString(ref DataReader, "UserFirstName"),
                                    UserLastName = Validation.SqlDBToString(ref DataReader, "UserLastName"),
                                    UserName = Validation.SqlDBToString(ref DataReader, "UserName"),
                                    UserMail = Validation.SqlDBToString(ref DataReader, "UserMail"),
                                    UserRolNames = Validation.SqlDBToString(ref DataReader, "UserRolNames"),
                                    StateID = Validation.SqlDBToInt16(ref DataReader, "StateID"),
                                    UserLastUpdatedDateTime = Validation.SqlDBToDateTime(ref DataReader, "UserLastUpdatedDateTime"),
                                    UserLastUpdatedUserName = Validation.SqlDBToString(ref DataReader, "UserLastUpdatedUserName")                                
                                };
                                Pagination.Entities.Add(Get);
                                Pagination.Filtered = Validation.SqlDBToInt32(ref DataReader, "RecordsFiltered");                               
                            }                          
                        }
                    }
                    Pagination.Total = Convert.ToInt32(Command.Parameters["@RecordsTotal"].Value);                   
                }
        
            return Pagination;
        }
 
    }
}
