using SIGC.DomainModel.Dtos.Token;

namespace SIGC.DomainService.IRepositories.ITokenRepositories
{
   public interface ITokenGetExpirationRepository
   {
       Task<TokenGetExpirationResponseDto?> GetExpirationAsync(TokenGetExpirationRequestDto TokenGetExpirationResquest, CancellationToken CancellationToken = default);
   }
}