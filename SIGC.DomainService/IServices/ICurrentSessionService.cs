namespace SIGC.DomainService.IServices
{
    public interface ICurrentSessionService
    {
        bool IsAuthenticated { get; }
        int UserID { get; }
        int CompanyID { get; }
        string UserName { get; }
        string CompanyDocumentNumber { get; }
        string CompanySocialReason { get; }
        string CompanyTradeName { get; }
        List<string> RoleList { get; }
        short IdiomID { get; }

    }
}