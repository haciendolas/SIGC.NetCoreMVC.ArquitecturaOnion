namespace SIGC.DomainModel.Dtos
{
   public class ClaimsPrincipalDto
    {
        public bool IsValid => UserID != null;
        public bool IsExpired { get; set; }
        public int? UserID { get; set; }      
        public int CompanyID { get; set; }
        public string UserName { get; set; } = "";  
   }
}