using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IPageCompanyRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PageCompanyRepositories
{
    internal class PageCompanyCreateNotExistsRepository : IPageCompanyCreateNotExistsRepository
    {
        private readonly string ConnectionString;
        public PageCompanyCreateNotExistsRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }
        public async Task<int> CreateNotExistsAsync(PageCompany Model, CancellationToken CancellationToken = default)
        {
            int RecordAffected = 0;
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspPageCompanyCreateNotExists";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CompanyID", Model.CompanyID);         
                    Command.Parameters.AddWithValue("@PageID", Model.PageID);
                    Command.Parameters.AddWithValue("@PageCompanyCreatedDateTime", Model.CreatedDateTime);
                    Command.Parameters.AddWithValue("@PageCompanyCreatedUserID", Model.CreatedBy);
                    Command.Connection = Connection;
                    RecordAffected = await Command.ExecuteNonQueryAsync(CancellationToken);
                }
            }
            return RecordAffected;
        }
    }
}