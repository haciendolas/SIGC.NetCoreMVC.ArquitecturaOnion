namespace SIGC.DomainModel.Dtos.CatalogPrice
{
    public sealed record CatalogPriceListResponseDto
    (
        int EstablishmentID,
        string EstablishmentName,
        int CatalogPriceID,
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