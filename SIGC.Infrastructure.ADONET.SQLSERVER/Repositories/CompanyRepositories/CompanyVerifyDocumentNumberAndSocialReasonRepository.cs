using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyVerifyDocumentNumberAndSocialReasonRepository : ICompanyVerifyDocumentNumberAndSocialReasonRepository
    {
        public Task<string> VerifyDocumentNumberAndSocialAsync(Company Model, CancellationToken CancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
