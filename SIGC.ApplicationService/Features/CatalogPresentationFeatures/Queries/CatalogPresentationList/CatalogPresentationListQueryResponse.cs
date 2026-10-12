namespace SIGC.ApplicationService.Features.CatalogPresentationFeatures.Queries.CatalogPresentationList
{
    public sealed record CatalogPresentationListQueryResponse(
        int CatalogVariantID,
        string CatalogVariantName,        
        List<CatalogPresentationItemListQueryResponse> CatalogPresentations
    );

    public sealed record CatalogPresentationItemListQueryResponse(
        int CatalogPresentationID,
        string CatalogPresentationName
    );
}