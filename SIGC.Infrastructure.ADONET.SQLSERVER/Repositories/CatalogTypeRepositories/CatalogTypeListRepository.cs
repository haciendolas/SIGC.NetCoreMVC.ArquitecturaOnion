using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.CatalogType;
using SIGC.DomainService.IRepositories.ICatalogTypeRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTypeRepositories
{
    internal class CatalogTypeListRepository : ICatalogTypeListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public CatalogTypeListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<CatalogTypeListResponseDto>> ListAsync(CancellationToken CancellationToken = default)
        {
            var List = new List<CatalogTypeListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspCatalogTypeList";
                Command.CommandType = CommandType.StoredProcedure;           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new CatalogTypeListResponseDto(
                                    CatalogTypeID: Validation.SqlDBToTinyint(ref DataReader, "CatalogTypeID"),
                                    CatalogTypeName: Validation.SqlDBToString(ref DataReader, "CatalogTypeName")                                  
                                );                          
                            List.Add(Get);
                        }
                    }
                }
            }
            return List;
        }
    }
}
