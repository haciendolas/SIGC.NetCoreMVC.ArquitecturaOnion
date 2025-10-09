using SIGC.DomainModel.Dtos.Company;

namespace SIGC.DomainService.IRepositories.ICompanyRepositories
{
    public interface ICompanyListRepository
    {
        Task<List<CompanyListResponseDto>> ListAsync(int CompanyIDRegister, CancellationToken CancellationToken = default);
    }
}