namespace SIGC.ApplicationService.Features.CatalogPresentationFeatures.Queries.CatalogPresentationList
{
    public sealed record CatalogVariantListQueryResponse(
        int CatalogVariantID,
        string CatalogVariantName,        
        List<CatalogPresentationListQueryResponse> CatalogPresentations
    );

    public sealed record CatalogPresentationListQueryResponse(
        int CatalogPresentationID,
        string CatalogPresentationName
    );
}