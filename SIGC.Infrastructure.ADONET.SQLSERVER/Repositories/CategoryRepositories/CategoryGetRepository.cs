using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.Category;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
    internal class CategoryGetRepository : ICategoryGetRepository
    {
        private readonly string ConnectionString; 
        private readonly ITransactionAccessor TransactionAccessor;
        public CategoryGetRepository(IOptions<AppDbContext> Options, IJsonSerializerService JsonSerializerService,
            ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;        
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<CategoryGetResponseDto?> GetAsync(int CompanyId,int CategoryId, CancellationToken CancellationToken)
        {
            CategoryGetResponseDto? Get = null;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCategoryGet";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@CompanyID", CompanyId);
                Command.Parameters.AddWithValue("@CategoryID", CategoryId);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (DataReader.Read())
                        {
                            Get = new CategoryGetResponseDto()
                            {
                                CategoryId = Validation.SqlDBToInt32(ref DataReader, "CategoryID"),
                                CategoryName = Validation.SqlDBToString(ref DataReader, "CategoryName"),
                                CategorySlug  = Validation.SqlDBToString(ref DataReader, "CategorySlug"),
                                CategoryImage = Validation.SqlDBToString(ref DataReader, "CategoryImage"),
                                RecordStateID = Validation.SqlDBToTinyint(ref DataReader, "RecordStateID"),                               
                            };
                        }
                    }
                }
            }
            return Get;
        }
    }
}