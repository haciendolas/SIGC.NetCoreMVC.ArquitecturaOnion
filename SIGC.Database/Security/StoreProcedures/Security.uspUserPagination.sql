 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            29/11/2025
-- Description:            Permite obtener listado paginado de user por @companyID tabla [Security].[UserCompany]
-- Update:				   Joel Castillo Rojas    
-- Exec                    
        /*
		DECLARE @RecordsTotal INT
		Exec Security.uspUserPagination @companyID=1, @UserFullName='',@UserName=NULL,@StateID=1,@PageNumber=1,@PageSize=10,
		@RecordsTotal=@RecordsTotal OUTPUT

		SELECT  @RecordsTotal AS 'RecordsTotal'
		*/
-- ============================================================================== 
ALTER PROCEDURE [Security].uspUserPagination(
   @CompanyID INT,
   @UserFullName VARCHAR(50),
   @UserName VARCHAR(20),
   @StateID SMALLINT=1,
   @PageNumber INT=1,
   @PageSize INT=10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON

    SET @RecordsTotal =(SELECT COUNT(U.UserID) FROM [Security].[User] U WITH(NOLOCK)
	                   INNER JOIN [Security].UserCompany UC WITH(NOLOCK) ON U.UserID=UC.UserID
	                 WHERE UC.CompanyID=@CompanyID AND UC.StateID!=2)

    SELECT U.UserID,U.UserFirstName,U.UserLastName,U.UserName,U.UserMail,UC.StateID,
		 ISNULL(U.UserUpdatedDateTime,U.UserCreatedDateTime) AS UserLastUpdatedDateTime,
		 U.UserName AS UserLastUpdatedUserName,
	     'UserRolNames'=STUFF((SELECT ','+ R.RoleName FROM [Security].UserRole UR WITH(NOLOCK)
				  INNER JOIN [Security].[Role] R WITH(NOLOCK) ON UR.RoleID= R.RoleID 
				  WHERE UR.UserID=U.UserID FOR XML PATH('')),1,1,''
		  ),
		 COUNT(*) OVER() AS RecordsFiltered
	 FROM [Security].[User] U WITH(NOLOCK)
	  INNER JOIN [Security].UserCompany UC WITH(NOLOCK) ON U.UserID=UC.UserID
	  INNER JOIN [Security].[User] URegister WITH(NOLOCK) ON ISNULL(U.UserUpdatedUserID,U.UserCreatedUserID)=URegister.UserID
	 WHERE UC.CompanyID=@CompanyID
	  AND UC.StateID=CASE WHEN @StateID=10 THEN UC.StateID ELSE @StateID END 
	  AND UC.StateID!=2
	  AND U.UserName LIKE '%'+ISNULL(@UserName,'')+'%'
	  AND CONCAT(U.UserLastName,' ',U.UserFirstName) LIKE ISNULL(@UserFullName,'')+'%'
	 ORDER BY U.UserID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END