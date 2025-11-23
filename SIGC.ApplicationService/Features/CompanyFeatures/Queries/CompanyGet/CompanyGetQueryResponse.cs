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
        short RubroID,
        string CompanyCorporateEmail,
        string CompanyMobile,
        string CompanyPhone,
        string CompanyLogo,
        string CompanyUrl,
        short StateID,
        List<CompanyPageGetQueryResponse> PageCompany
   );
   
   public record struct CompanyPageGetQueryResponse(
       int PageID     
   );  
}