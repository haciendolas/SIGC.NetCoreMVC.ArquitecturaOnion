namespace SIGC.ApplicationService.Features.RoleFeatures.Queries.RolePagination
{
    public class RolePaginationQueryResponse
    {
        public int RoleID { get; set; }
        public string RoleCode { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public short StateID { get; set; }
        public string? RoleDescription { get; set; }
        public DateTime RoleLastUpdatedDateTime { get; set; }
        public string RoleLastUpdatedUserName { get; set; } = null!;
    }
}