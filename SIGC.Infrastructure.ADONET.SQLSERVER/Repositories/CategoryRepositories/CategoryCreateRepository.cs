using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
   internal class CategoryCreateRepository : ICategoryCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CategoryCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<int> CreateAsync(Category Model, CancellationToken CancellationToken)
        {
            int RecordAffected = 0;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCategoryCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@CategoryID", SqlDbType.Int);
                Command.Parameters["@CategoryID"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyId);
                Command.Parameters.AddWithValue("@CategoryName", Model.CategoryName);
                Command.Parameters.AddWithValue("@CategorySlug", Model.CategorySlug);
                Command.Parameters.AddWithValue("@CategoryImage", string.IsNullOrWhiteSpace(Model.CategoryImage) ? DBNull.Value : Model.CategoryImage);
                Command.Parameters.AddWithValue("@RecordOriginID", (short)Model.RecordOriginId);
                Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateId);
                Command.Parameters.AddWithValue("@CategoryCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@CategoryCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@CategoryCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@CategoryCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.CategoryId = Convert.ToInt32(Command.Parameters["@CategoryID"].Value);
            }

            return RecordAffected;
        }
    }
}