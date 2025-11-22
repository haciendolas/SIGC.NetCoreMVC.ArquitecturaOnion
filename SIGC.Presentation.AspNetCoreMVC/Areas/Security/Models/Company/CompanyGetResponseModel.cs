namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company
{
    public record struct CompanyGetResponseModel
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
        short StateID,
        List<CompanyPageGetResponseModel> PageCompany
   );

    public record struct CompanyPageGetResponseModel(
        int PageID
    );
}
