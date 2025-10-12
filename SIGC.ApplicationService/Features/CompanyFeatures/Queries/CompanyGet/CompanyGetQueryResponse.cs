namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyGet
{
   public record struct CompanyGetQueryResponse
   (
        int CompanyID,
        string CompanyTradeName,
        string CompanySocialReason,
        string CompanyDocumentNumber,
        DateTime CompanyBirthDate,
        int CountryID,
        string CompanyAddress,
        short TaxpayerTypeID,
        short SectorID,
        string CompanyCorporateEmail,
        string CompanyMobile,
        string CompanyPhone,
        string CompanyLogo,
        short StateID,
        List<CompanyPageGetQueryResponse> PageCompany
   );
   
   public record struct CompanyPageGetQueryResponse(
       int PageID     
   );  
}