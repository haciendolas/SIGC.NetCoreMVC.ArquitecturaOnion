namespace SIGC.DomainModel.Dtos
{
    public class AppUserDto {  
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string UserMail { get; set; } = null!;
        public int CompanyID { get; set; }
        public short IdiomID { get; set; }
        public string CompanyDocumentNumber { get; set; } = null!;
        public string CompanyTradeName { get; set; } = null!;
        public string CompanySocialReason { get; set; } = null!;
        public string RoleCodes { get; set; } = null!;
        public DateTime CurrentDateTime { get; set; }
        public DateTime ExpirationRandomDateTime { get; set; }
        public DateTime ExpirationJWTDateTime { get; set; }
    }
}