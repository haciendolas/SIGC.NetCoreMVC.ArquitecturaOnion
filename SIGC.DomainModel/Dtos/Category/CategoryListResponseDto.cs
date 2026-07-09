namespace SIGC.DomainModel.Dtos.Category
{
    public sealed record CategoryListResponseDto
    (
        int CategoryID,
        string CategoryName,
        string CategorySlug
    );    
}
