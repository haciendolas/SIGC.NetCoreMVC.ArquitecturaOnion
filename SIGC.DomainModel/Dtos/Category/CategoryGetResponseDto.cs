namespace SIGC.DomainModel.Dtos.Category
{
    public record struct CategoryGetResponseDto(
         int CategoryId,
         string CategoryName,
         string CategorySlug,
         string CategoryImage,
         byte RecordStateID,
         string CategoryUrl
    );    
}