using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.ICatalogActiveIngredientRepositories
{
    public interface ICatalogActiveIngredientCreateUpdateRepository
    {
        Task<int> CreateUpdateAsync(CatalogActiveIngredient Model, CancellationToken CancellationToken = default);
    }
}
