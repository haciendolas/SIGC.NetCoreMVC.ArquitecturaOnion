namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company
{
    public record struct CompanyListResponseModel(
        int CompanyID,
        string CompanyDocumentNumber,
        string CompanySocialReason
    );
}