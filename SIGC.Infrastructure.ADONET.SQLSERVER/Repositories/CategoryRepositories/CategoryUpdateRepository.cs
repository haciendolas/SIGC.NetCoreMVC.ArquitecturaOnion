using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
    internal class CategoryUpdateRepository : ICategoryUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CategoryUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> UpdateAsync(Category Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCategoryUpdate";
                Command.CommandType = CommandType.StoredProcedure; 
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyId);
                Command.Parameters.AddWithValue("@CategoryId", Model.CategoryId);
                Command.Parameters.AddWithValue("@CategoryName", Model.CategoryName);
                Command.Parameters.AddWithValue("@CategorySlug", Model.CategorySlug);
                Command.Parameters.AddWithValue("@CategoryImage", string.IsNullOrWhiteSpace(Model.CategoryImage) ? DBNull.Value : Model.CategoryImage);        
                Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateId);
                Command.Parameters.AddWithValue("@CategoryUpdatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CategoryUpdatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CategoryUpdatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CategoryUpdatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);      
            }
            return RecordAffected;
        }
    }
}