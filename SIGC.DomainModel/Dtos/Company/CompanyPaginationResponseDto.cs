namespace SIGC.DomainModel.Dtos.Company
{
    public class CompanyPaginationResponseDto
    {
        public int RoleID { get; set; }
        public string RoleCode { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public short StateID { get; set; }
        public DateTime RoleLastUpdatedDateTime { get; set; }
        public string RoleLastUpdatedUserName { get; set; } = null!;
    };    
}