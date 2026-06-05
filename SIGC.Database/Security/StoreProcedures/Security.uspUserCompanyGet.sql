 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            06/12/2025
-- Description:            Permite obtener un usuario con sus roles apartir de su UserID y CompanyID de la tabla [Security].[UserCompany]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec [Security].uspUserCompanyGet  @UserID=6,@CompanyID=1
-- ============================================================================== 
ALTER PROCEDURE [Security].uspUserCompanyGet(
   @UserID INT,
   @CompanyID INT 
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT U.UserID,U.UserFirstName,U.UserLastName,U.UserName,U.UserPassword,
		U.UserMail,U.UserPhoto,UC.StateID,
	   'RoleIDConcat'=STUFF((SELECT ','+ Convert(VARCHAR(20),UR.RoleID) FROM [Security].UserRole UR WITH(NOLOCK) 
				  WHERE UR.UserID=U.UserID AND UR.CompanyID=UC.CompanyID
				 FOR XML PATH('')),1,1,''
		)
		FROM [Security].[User] U WITH(NOLOCK)
		INNER JOIN [Security].UserCompany UC WITH(NOLOCK) ON U.UserID=UC.UserID		 
		WHERE U.UserID=@UserID AND UC.CompanyID=@CompanyID 

	SET NOCOUNT OFF
END