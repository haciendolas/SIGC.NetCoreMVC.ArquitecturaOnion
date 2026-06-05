using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.EstablishmentRepositories
{
   internal class EstablishmentUpdateRepository : IEstablishmentUpdateRepository
    {
        private readonly string ConnectionString;
        private readonly ITransactionAccessor TransactionAccessor;

        public EstablishmentUpdateRepository(IOptions<AppDbContext> Options,
            ITransactionAccessor TransactionAccessor
            )
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
            this.TransactionAccessor = TransactionAccessor;
        }

        public async Task<string> UpdateAsync(Establishment Model, CancellationToken CancellationToken)
        {           
            string RetMsg = string.Empty;
            var Connection = await TransactionAccessor.GetOrOpenConnectionAsync(ConnectionString, CancellationToken);
            var Transaction = TransactionAccessor.CurrentTransaction;
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "Organization.uspEstablishmentUpdate";
                Command.CommandType = CommandType.StoredProcedure; 
                Command.Parameters.Add("@RetMsg", SqlDbType.VarChar, 11);
                Command.Parameters["@RetMsg"].Direction = ParameterDirection.Output;
                Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);
                Command.Parameters.AddWithValue("@EstablishmentID", Model.EstablishmentID);
                Command.Parameters.AddWithValue("@PersonID", Model.PersonID);
                Command.Parameters.AddWithValue("@TypeID", Model.TypeID);
                Command.Parameters.AddWithValue("@EstablishmentCode", Model.EstablishmentCode);
                Command.Parameters.AddWithValue("@EstablishmentName", Model.EstablishmentName);
                Command.Parameters.AddWithValue("@EstablishmentAddress", string.IsNullOrWhiteSpace(Model.EstablishmentAddress) ? DBNull.Value : Model.EstablishmentAddress);
                Command.Parameters.AddWithValue("@EstablishmentLogo", string.IsNullOrWhiteSpace(Model.EstablishmentLogo) ? DBNull.Value : Model.EstablishmentLogo);
                Command.Parameters.AddWithValue("@RecordStateID", (short)Model.RecordStateId);
                Command.Parameters.AddWithValue("@EstablishmentUpdatedUserID", Model.CreatedById);
                Command.Parameters.AddWithValue("@EstablishmentUpdatedUserName", Model.CreatedByName);
                Command.Parameters.AddWithValue("@EstablishmentUpdatedUserFullName", Model.CreatedByFullName);
                Command.Parameters.AddWithValue("@EstablishmentUpdatedDateTime", Model.CreatedDate);
                Command.Connection = Connection;
                Command.Transaction = Transaction;
                await Command.ExecuteNonQueryAsync(CancellationToken);                
                RetMsg = Command.Parameters["@RetMsg"].Value.ToString()!;
            }
            return RetMsg;
        }
    }
}