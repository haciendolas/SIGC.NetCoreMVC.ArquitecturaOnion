namespace SIGC.DomainModel.ValueObjects
{
    public class CompanyRegister
    {
        public int CompanyIDRegister { get; set; }
        public int CompanyID { get; set; }
        public DateTime CompanyRegisterCreatedDateTime { get; set; }
        public int CompanyRegisterCreatedUserID { get; set; }
    }
}