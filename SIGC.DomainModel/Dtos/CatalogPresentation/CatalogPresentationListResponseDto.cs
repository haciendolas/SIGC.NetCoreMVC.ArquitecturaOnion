namespace SIGC.DomainModel.Dtos.CatalogPresentation
{
    public sealed record CatalogPresentationListResponseDto
    (
        int CatalogVariantID,
        string CatalogVariantName,
        int CatalogPresentationID  ,
        string CatalogPresentationName
    );
}