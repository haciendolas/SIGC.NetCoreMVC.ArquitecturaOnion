using SIGC.DomainModel.Dtos.ActiveIngredient;

namespace SIGC.DomainService.IRepositories.IActiveIngredientRepositories
{
    public interface IActiveIngredientListRepository
    {
        Task<List<ActiveIngredientListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}