namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyPagination
{
    public class CompanyPaginationQueryResponse
    {
        public int CompanyID { get; set; }
        public string TaxpayerTypeName { get; set; } = null!;
        public string CompanyDocumentNumber { get; set; } = null!;
        public string CompanySocialReason { get; set; } = null!;
        public string SectorName { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public short StateID { get; set; }    
        public DateTime CompanyLastUpdatedDateTime { get; set; }
        public string CompanyLastUpdatedUserName { get; set; } = null!;
    }
}