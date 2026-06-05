using SIGC.DomainModel.Dtos;

namespace SIGC.DomainService.IServices
{
    public interface IGenerateTokenService
    {
        Task<string> GenerateJWTToken(AppUserDto AppUser);

        Task<ClaimsPrincipalDto?> ValidateJWTToken(string JWTToken, bool IgnoreExpiration = false);

        Task<string> GenerateRandomToken(int ByteLength = 32); 
    }
}