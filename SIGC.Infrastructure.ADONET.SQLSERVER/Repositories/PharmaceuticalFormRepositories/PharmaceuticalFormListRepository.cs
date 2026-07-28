using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.PharmaceuticalForm;
using SIGC.DomainService.IRepositories.IPharmaceuticalFormRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PharmaceuticalFormRepositories
{
    internal class PharmaceuticalFormListRepository : IPharmaceuticalFormListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public PharmaceuticalFormListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<PharmaceuticalFormListResponseDto>> ListAsync(CancellationToken CancellationToken = default)
        {
            var List = new List<PharmaceuticalFormListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspPharmaceuticalFormList";
                Command.CommandType = CommandType.StoredProcedure;           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new PharmaceuticalFormListResponseDto(
                                    PharmaceuticalFormID: Validation.SqlDBToInt16(ref DataReader, "PharmaceuticalFormID"),
                                    PharmaceuticalFormName: Validation.SqlDBToString(ref DataReader, "PharmaceuticalFormName")                                  
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
