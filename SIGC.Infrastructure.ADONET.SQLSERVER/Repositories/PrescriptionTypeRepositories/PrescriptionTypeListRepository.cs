using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.PrescriptionType;
using SIGC.DomainService.IRepositories.IPrescriptionTypeRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PrescriptionTypeRepositories
{
    internal class PrescriptionTypeListRepository : IPrescriptionTypeListRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public PrescriptionTypeListRepository(IOptions<AppDbContext> Options,
              ITransactionAccessor TransactionAccessor)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<List<PrescriptionTypeListResponseDto>> ListAsync(CancellationToken CancellationToken = default)
        {
            var List = new List<PrescriptionTypeListResponseDto>();
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Product.uspPrescriptionTypeList";
                Command.CommandType = CommandType.StoredProcedure;           
                Command.Connection = Connection;
                SqlDataReader DataReader;
                using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                {
                    if (DataReader.HasRows)
                    {
                        while (await DataReader.ReadAsync(CancellationToken))
                        {
                            var Get = new PrescriptionTypeListResponseDto(
                                    PrescriptionTypeID: Validation.SqlDBToTinyint(ref DataReader, "PrescriptionTypeID"),
                                    PrescriptionTypeName: Validation.SqlDBToString(ref DataReader, "PrescriptionTypeName")                                  
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
