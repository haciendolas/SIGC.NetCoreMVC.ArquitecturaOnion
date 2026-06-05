using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.EstablishmentRepositories
{
   internal class EstablishmentCreateRepository : IEstablishmentCreateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public EstablishmentCreateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> CreateAsync(Establishment Model, CancellationToken CancellationToken)
        {           
            string RetMsg = string.Empty;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Organization.uspEstablishmentCreate";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Add("@EstablishmentID", SqlDbType.Int);
                Command.Parameters["@EstablishmentID"].Direction = ParameterDirection.Output;         
                Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 11);
                Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@PersonID", Model.PersonID);
                Command.Parameters.AddWithValue("@TypeID", Model.TypeID);
                Command.Parameters.AddWithValue("@EstablishmentCode", Model.EstablishmentCode);
                Command.Parameters.AddWithValue("@EstablishmentName", Model.EstablishmentName);
                Command.Parameters.AddWithValue("@EstablishmentAddress", string.IsNullOrWhiteSpace(Model.EstablishmentAddress) ? DBNull.Value : Model.EstablishmentAddress);
                Command.Parameters.AddWithValue("@EstablishmentLogo", string.IsNullOrWhiteSpace(Model.EstablishmentLogo) ? DBNull.Value : Model.EstablishmentLogo);
                Command.Parameters.AddWithValue("@RecordOriginID", (short)Model.RecordOriginId);
                Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateId);
                Command.Parameters.AddWithValue("@EstablishmentCreatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@EstablishmentCreatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@EstablishmentCreatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@EstablishmentCreatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                await Command.ExecuteNonQueryAsync(CancellationToken);
                Model.EstablishmentID = Convert.ToInt32(Command.Parameters["@EstablishmentID"].Value);
                RetMsg = Command.Parameters["@RetMsg"].Value.ToString()!;
            }
            return RetMsg;
        }
    }
}