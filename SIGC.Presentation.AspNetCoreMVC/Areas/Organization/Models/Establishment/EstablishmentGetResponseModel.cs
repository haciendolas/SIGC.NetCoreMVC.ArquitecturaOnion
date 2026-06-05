namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Establishment
{
    public sealed record EstablishmentGetResponseModel
    (
        int EstablishmentID,
        byte TypeID,
        string EstablishmentCode,
        string EstablishmentName,
        string EstablishmentAddress,
        string EstablishmentLogo,
        byte RecordStateID,
        string? EstablishmentUrl
    );    
}
