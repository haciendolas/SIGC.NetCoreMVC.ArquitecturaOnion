namespace SIGC.DomainModel.Dtos.Category
{
    public record struct CategoryGetResponseDto(
         int CategoryId,
         string CategoryName,
         short StateId
    );    
}