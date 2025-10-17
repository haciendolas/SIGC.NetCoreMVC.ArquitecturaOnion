namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company
{
    public class CompanyCreateUpdateRequestModel
    {
        public int CompanyId { get; set; }
        public string CompanyTradeName { get; set; } = null!;
        public string CompanySocialReason { get; set; } = null!;
        public string CompanyDocumentNumber { get; set; } = null!;
        public DateTime CompanyBirthDate { get; set; }
        public int CountryID { get; set; }
        public string? CompanyAddress { get; set; }
        public Int16 TaxpayerTypeID { get; set; }
        public short SectorID { get; set; }
        public string? CompanyMobile { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyLogo { get; set; }
        public short StateID { get; set; }
   }
}