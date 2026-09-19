using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogRepositories
{
    public interface ICatalogVerifyCodeAndNameRepository
    {
        Task<string> VerifyCodeAndNameAsync(Catalog Model, CancellationToken CancellationToken = default);
    }
}