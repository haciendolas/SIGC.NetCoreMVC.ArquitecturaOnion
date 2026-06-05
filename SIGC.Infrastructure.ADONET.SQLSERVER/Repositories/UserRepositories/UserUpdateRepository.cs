using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserRepositories
{
    internal class UserUpdateRepository : IUserUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public UserUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> UpdateAsync(User Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "[Security].uspUserUpdate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@UserID", Model.UserId);
                Command.Parameters.AddWithValue("@UserFirstName", Model.UserFirstName);
                Command.Parameters.AddWithValue("@UserLastName", Model.UserLastName);
                Command.Parameters.AddWithValue("@UserName", Model.UserName);
                Command.Parameters.AddWithValue("@UserPassword", Model.UserPassword);
                Command.Parameters.AddWithValue("@UserMail", string.IsNullOrWhiteSpace(Model.UserMail) ? DBNull.Value : Model.UserMail);
                Command.Parameters.AddWithValue("@UserPhoto", string.IsNullOrWhiteSpace(Model.UserPhoto) ? DBNull.Value : Model.UserPhoto);
                Command.Parameters.AddWithValue("@StateID", (short)Model.StateId);
                Command.Parameters.AddWithValue("@UserUpdatedUserID", Model.CreatedBy);
                Command.Parameters.AddWithValue("@UserUpdatedDateTime", Model.CreatedDateTime);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);            
            }
            return RecordAffected;
        }
    }
}
