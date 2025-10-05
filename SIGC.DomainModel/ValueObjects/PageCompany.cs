namespace SIGC.DomainModel.ValueObjects
{
    public class PageCompany
    {
        public int CompanyID { get; set; }
        public int PageID { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedBy { get; set; }
    }
}