namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Establishment
{
    public class EstablishmentCreateUpdateRequestModel
    {
        public int? PersonID { get; set; }
        public int EstablishmentID { get; set; }
        public byte TypeID { get; set; }
        public string EstablishmentCode { get; set; } = null!;
        public string EstablishmentName { get; set; } = null!;
        public string EstablishmentAddress { get; set; } = null!;
        public byte RecordOriginId { get; set; }
        public byte RecordStateId { get; set; }
        public IFormFile? FormFile { get; set; }
        public string? EstablishmentLogo { get; set; }
        public string? EstablishmentLogoBandera { get; set; }
    }
}