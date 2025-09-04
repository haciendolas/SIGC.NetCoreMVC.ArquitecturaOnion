namespace SIGC.DomainModel.Models
{
    public class Token{
        public int TokenID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string? TokenSessionJson { get; set; }
        public string TokenRefreshRandom { get; set; } = null!;
        public string? TokenAccessJWT { get; set; }
        public DateTime TokenCreateDateTime { get; set; }
        public DateTime TokenExpirationRandomDateTime { get; set; }
        public DateTime TokenExpirationJWTDateTime { get; set; }
    }
}