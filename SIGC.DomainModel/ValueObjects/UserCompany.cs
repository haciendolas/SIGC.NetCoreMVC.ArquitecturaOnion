using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.ValueObjects
{
    public class UserCompany
    {
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public RecordStateEnum StateID { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedBy { get; set; }
    }
}