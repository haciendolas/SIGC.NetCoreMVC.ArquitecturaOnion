using SIGC.DomainModel.Dtos.Ubigeo;

namespace SIGC.DomainService.IRepositories.IUbigeoRepositories
{
    public interface IUbigeoListByUbigeoClassRepository
    {
        Task<List<UbigeoListByUbigeoClassResponseDto>> ListByUbigeoClassAsync(int UbigeoClass, CancellationToken CancellationToken = default);
    }
}