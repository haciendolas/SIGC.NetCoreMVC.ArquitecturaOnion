using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.ICompanyRegisterRepositories
{
   public interface ICompanyRegisterCreateRepository
    {
        Task<int> CreateAsync(CompanyRegister Model, CancellationToken CancellationToken = default);
    }
}