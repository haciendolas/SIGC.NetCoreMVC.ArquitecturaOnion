 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            30/00/2025
-- Description:            Permite obtener listado paginado de Role por @companyID tabla Security.[Role]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspRolePagination  @companyID=1, @RoleName='',@StateID=1,@PageNumber=1,@PageSize=10
-- ============================================================================== 
ALTER PROCEDURE Security.uspRolePagination(
   @CompanyID INT,
   @RoleName VARCHAR(50),
   @StateID SMALLINT=1,
   @PageNumber INT=1,
   @PageSize INT=10
)
AS
BEGIN
  SET NOCOUNT ON

    DECLARE @RecordsTotal INT =(SELECT count(R.RoleID) FROM Security.[Role] R WITH(NOLOCK) )

    SELECT R.RoleID,R.RoleCode,R.RoleName,R.RoleDescription,R.StateID,
		 ISNULL(R.RoleUpdatedDateTime,R.RoleCreatedDateTime) AS RoleLastUpdatedDateTime,
		 U.UserName AS RoleLastUpdatedUserName,
		 COUNT(*) OVER() AS RecordsFiltered,@RecordsTotal AS 'RecordsTotal'
	 FROM Security.[Role] R WITH(NOLOCK) 
	  INNER JOIN Security.[User] U WITH(NOLOCK) ON ISNULL(R.RoleUpdatedUserID,R.RoleCreatedUserID)=U.UserID
	  WHERE R.CompanyID=@CompanyID
	  AND R.StateID=@StateID OR @StateID=10 
	  AND R.RoleName LIKE '%'+@RoleName+'%'
	 ORDER BY R.RoleID ASC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END