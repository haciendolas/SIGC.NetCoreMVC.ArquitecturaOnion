using SIGC.DomainModel.Dtos.Ubigeo;

namespace SIGC.DomainService.IRepositories.IUbigeoRepositories
{
    public interface IUbigeoListSearchRepository
    {
        Task<List<UbigeoListSearchResponseDto>> ListSearchAsync(int UbigeoClassContinent,string UbigeoName, CancellationToken CancellationToken = default);
    }
}