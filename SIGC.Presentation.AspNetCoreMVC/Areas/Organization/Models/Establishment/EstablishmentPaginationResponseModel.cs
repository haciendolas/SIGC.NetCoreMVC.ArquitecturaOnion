namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Establishment
{
   public class EstablishmentPaginationResponseModel
    {
        public int EstablishmentID { get; set; }
        public string EstablishmentCode { get; set; } = null!;
        public string EstablishmentName { get; set; } = null!;
        public string EstablishmentAddress { get; set; } = null!;
        public byte RecordStateID { get; set; }
        public DateTime EstablishmentLastUpdatedDateTime { get; set; }
        public int EstablishmentLastUpdatedUserID { get; set; }
        public string EstablishmentLastUpdatedUserName { get; set; } = null!;
        public string EstablishmentLastUpdatedUserFullName { get; set; } = null!;
    }
}