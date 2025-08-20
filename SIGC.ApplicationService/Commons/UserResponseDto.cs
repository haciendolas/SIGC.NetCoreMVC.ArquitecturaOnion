namespace SIGC.ApplicationService.Commons
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyDocumentNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
    }

}