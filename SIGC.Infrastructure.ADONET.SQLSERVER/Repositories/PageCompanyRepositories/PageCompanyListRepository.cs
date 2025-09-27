using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos.PageCompany;
using SIGC.DomainService.IRepositories.IPageCompanyRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Extensions;
using System.Data;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PageCompanyRepositories
{
    internal class PageCompanyListRepository : IPageCompanyListRepository
    {
        private readonly string ConnectionString;
        public PageCompanyListRepository(IOptions<AppDbContext> Options)
        {
            ConnectionString = Options.Value.ConnectionDBCommerce360;
        }

        public async Task<List<PageCompanyListResponseDto>> ListAsync(int CompanyID, CancellationToken CancellationToken = default)
        {
            var List = new List<PageCompanyListResponseDto>();
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                await Connection.OpenAsync(CancellationToken);
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.CommandText = "Security.uspPageCompanyList";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CompanyID", CompanyID);
                    Command.Connection = Connection;
                    SqlDataReader DataReader;
                    using (DataReader = await Command.ExecuteReaderAsync(CancellationToken))
                    {
                        if (DataReader.HasRows)
                        {
                            while (await DataReader.ReadAsync(CancellationToken))
                            {
                                var Get = new PageCompanyListResponseDto()
                                {
                                    PageID = Validation.SqlDBToInt32(ref DataReader, "PageID"),
                                    PageParentID = Validation.SqlDBToInt32(ref DataReader, "PageParentID"),
                                    PageHierarchy = Validation.SqlDBToString(ref DataReader, "PageHierarchy"),
                                    PageName = Validation.SqlDBToString(ref DataReader, "PageName"),
                                    PageIconName = Validation.SqlDBToString(ref DataReader, "PageIconName"),
                                    PageOrder = Validation.SqlDBToInt16(ref DataReader, "PageOrder")
                                };
                                List.Add(Get);
                            }
                        }
                    }
                }
            }
            return List;
        }
    }
}