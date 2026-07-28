using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.AttributeValueList;
using SIGC.DomainService.IRepositories.IAttributeValueRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.AttributeValueRepositories
{
    internal class AttributeValueListRepository : IAttributeValueListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public AttributeValueListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<AttributeValueListResponseDto>> ListAsync(bool? AttributeIsVariant,CancellationToken CancellationToken = default)
        {
            var List = new List<AttributeValueListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspAttributeValueList";
                Command.Parameters.AddWithValue("@AttributeIsVariant", AttributeIsVariant.HasValue ? AttributeIsVariant.Value : DBNull.Value);
                Command.CommandType = CommandType.StoredProcedure;           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new AttributeValueListResponseDto(
                                    AttributeID: Validation.SqlDBToTinyint(ref DataReader, "AttributeID"),
                                    AttributeName: Validation.SqlDBToString(ref DataReader, "AttributeName"),
                                    AttributeIsVariant: Validation.SqlDBToBoolean(ref DataReader, "AttributeIsVariant"),
                                    AttributeValueID: Validation.SqlDBToInt16(ref DataReader, "AttributeValueID"),
                                    AttributeValueName: Validation.SqlDBToString(ref DataReader, "AttributeValueName")
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
