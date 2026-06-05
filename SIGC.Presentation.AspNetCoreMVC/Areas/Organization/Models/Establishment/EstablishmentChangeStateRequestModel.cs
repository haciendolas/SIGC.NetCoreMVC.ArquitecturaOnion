namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Establishment
{
    public sealed record EstablishmentChangeStateRequestModel
    (
           int EstablishmentID,
           byte RecordStateID
    );
}