using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
    internal class CategoryVerifyNameRepository : ICategoryVerifyNameRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CategoryVerifyNameRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> VerifyNameAsync(Category Model, CancellationToken CancellationToken)
        {
            string RetMsg = string.Empty;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCategoryVerifyName";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 11);
                Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyId);
                Command.Parameters.AddWithValue("@CategoryID", Model.CategoryId);
                Command.Parameters.AddWithValue("@CategoryName", Model.CategoryName);          
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                await Command.ExecuteNonQueryAsync(CancellationToken);
                RetMsg = Command.Parameters["@RetMsg"].Value.ToString()!;
            }
            return RetMsg;
        }
    }
}