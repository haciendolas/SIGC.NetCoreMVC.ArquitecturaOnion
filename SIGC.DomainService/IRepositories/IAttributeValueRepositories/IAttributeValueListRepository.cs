using SIGC.DomainModel.Dtos.AttributeValueList;
 
namespace SIGC.DomainService.IRepositories.IAttributeValueRepositories
{
    public interface IAttributeValueListRepository
    {
        Task<List<AttributeValueListResponseDto>> ListAsync(bool? AttributeIsVariant, CancellationToken CancellationToken = default);
    }
}