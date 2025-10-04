namespace SIGC.ApplicationService.Features.RoleFeatures.Queries.RoleGet
{
   public record struct RoleGetQueryResponse
   (
        int RoleID,
        string RoleCode,
        string RoleName,
        string RoleDescription,
        short StateID,
        List<RolePageGetQueryResponse> Pages
   );
   
   public record struct RolePageGetQueryResponse(
       int PageID,
       List<RoleActionGetQueryResponse> Actions
   );

   public record struct RoleActionGetQueryResponse(
       int PageActionID
   );
}