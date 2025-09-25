 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            30/00/2025
-- Description:            Permite obtener listado paginado de Role por @companyID tabla Security.[Role]
-- Update:				   Joel Castillo Rojas    
-- Exec                    
        /*
		DECLARE @RecordsTotal INT
		Exec Security.uspRolePagination @companyID=1, @RoleName='XSS',@StateID=1,@PageNumber=1,@PageSize=10,
		@RecordsTotal=@RecordsTotal OUTPUT

		SELECT  @RecordsTotal AS 'RecordsTotal'
		*/
-- ============================================================================== 
ALTER PROCEDURE Security.uspRolePagination(
   @CompanyID INT,
   @RoleName VARCHAR(50),
   @StateID SMALLINT=1,
   @PageNumber INT=1,
   @PageSize INT=10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON

    SET @RecordsTotal =(SELECT count(R.RoleID) FROM Security.[Role] R WITH(NOLOCK) )

    SELECT R.RoleID,R.RoleCode,R.RoleName,R.RoleDescription,R.StateID,
		 ISNULL(R.RoleUpdatedDateTime,R.RoleCreatedDateTime) AS RoleLastUpdatedDateTime,
		 U.UserName AS RoleLastUpdatedUserName,
		 COUNT(*) OVER() AS RecordsFiltered
	 FROM Security.[Role] R WITH(NOLOCK) 
	  INNER JOIN Security.[User] U WITH(NOLOCK) ON ISNULL(R.RoleUpdatedUserID,R.RoleCreatedUserID)=U.UserID
	  WHERE R.CompanyID=@CompanyID
	  AND R.StateID=CASE WHEN @StateID=10 THEN R.StateID ELSE @StateID END 
	  AND R.StateID!=2
	  AND R.RoleName LIKE '%'+@RoleName+'%'
	 ORDER BY R.RoleID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END