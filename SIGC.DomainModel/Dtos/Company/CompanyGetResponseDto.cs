using SIGC.DomainModel.Dtos.PageCompany;

namespace SIGC.DomainModel.Dtos.Company
{
    public record struct CompanyGetResponseDto
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
        List<PageCompanyGetResponseDto> PageCompany
    );
}