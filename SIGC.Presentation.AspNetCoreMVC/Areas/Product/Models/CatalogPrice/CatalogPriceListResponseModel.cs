namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPrice
{
    public sealed record CatalogPriceListResponseModel
    (
        int EstablishmentID,
        string EstablishmentName,
        int CatalogPriceID,
        string CatalogVariantName,
        int CatalogPresentationID,
        string CatalogPresentationName,
        byte PriceTypeID,
        string PriceTypeName,
        byte CurrencyTypeID,
        string CurrencyTypeName,
        decimal CatalogPriceAmount,
        bool CatalogPriceIsTaxIncluded,
        byte RecordStateID
    );    
}