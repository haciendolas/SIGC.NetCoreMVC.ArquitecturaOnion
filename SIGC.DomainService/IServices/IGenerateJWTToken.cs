
using SIGC.DomainModel.Dtos;

namespace SIGC.DomainService.IServices
{
    public interface IGenerateJWTToken
    {
        Task<string> GenerateJWTToken(AppUserDto AppUser);
    }
}
