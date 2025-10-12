using SIGC.DomainModel.Dtos.Company;

namespace SIGC.DomainService.IRepositories.ICompanyRepositories
{
    public interface ICompanyGetRepository
    {
        Task<CompanyGetResponseDto?> GetAsync(int CompanyID, CancellationToken CancellationToken = default);
    }
}