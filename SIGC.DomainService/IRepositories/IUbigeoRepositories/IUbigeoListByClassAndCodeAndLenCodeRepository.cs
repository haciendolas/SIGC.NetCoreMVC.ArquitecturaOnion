using SIGC.DomainModel.Dtos.Ubigeo;

namespace SIGC.DomainService.IRepositories.IUbigeoRepositories
{
    public interface IUbigeoListByClassAndCodeAndLenCodeRepository
    {
        Task<List<UbigeoListByClassAndCodeAndLenCodeResponseDto>> ListByClassAndCodeAndLenCodeAsync(int UbigeoClass,string UbigeoCode,int LenUbigeoCode, CancellationToken CancellationToken = default);
    }
}