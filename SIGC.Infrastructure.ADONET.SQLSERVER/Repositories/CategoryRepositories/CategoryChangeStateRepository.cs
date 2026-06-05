using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
   internal class CategoryChangeStateRepository : ICategoryChangeStateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;
        public CategoryChangeStateRepository(IOptions<AppDbContext> Options,
           ITransactionAccessor TransactionAccessor
          )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }
        public async Task<int> ChangeStateAsync(Category Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCategoryChangeState";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyId);
                Command.Parameters.AddWithValue("@CategoryID", Model.CategoryId);
                Command.Parameters.AddWithValue("@RecordStateID", (byte)Model.RecordStateId);
                Command.Parameters.AddWithValue("@CategoryUpdatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CategoryUpdatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CategoryUpdatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CategoryUpdatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
            }
            return RecordAffected;
        }
    }
}