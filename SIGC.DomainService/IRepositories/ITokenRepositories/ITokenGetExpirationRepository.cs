using SIGC.DomainModel.Dtos.Token;

namespace SIGC.DomainService.IRepositories.ITokenRepositories
{
   public interface ITokenGetExpirationRepository
   {
       Task<TokenGetExpirationResponseDto?> GetExpirationAsync(TokenGetExpirationResquestDto TokenGetExpirationResquest, CancellationToken CancellationToken = default);
   }
}