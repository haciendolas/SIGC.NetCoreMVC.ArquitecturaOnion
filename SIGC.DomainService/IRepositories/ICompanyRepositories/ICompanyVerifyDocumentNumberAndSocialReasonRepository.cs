using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICompanyRepositories
{
    public interface ICompanyVerifyDocumentNumberAndSocialReasonRepository
    {
        Task<string> VerifyDocumentNumberAndSocialAsync(Company Model, CancellationToken CancellationToken = default);
    }
}