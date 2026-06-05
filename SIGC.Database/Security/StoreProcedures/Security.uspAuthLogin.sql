 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            30/08/2025
-- Description:            Permite obtener un usuario apartir de sus credenciales de la tabla Security.[User]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspAuthLogin  @CompanyDocumentNumber='10404358087', @UserName='administrador',@UserPassword='123456'
-- ============================================================================== 
ALTER PROCEDURE Security.uspAuthLogin(
   @CompanyDocumentNumber VARCHAR(11),
   @UserName VARCHAR(15),
   @UserPassword VARCHAR(10)
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT U.UserID,U.UserFirstName,U.UserLastName,U.UserMail,UC.StateID,U.UserPhoto,
		C.CompanyID,C.CompanyTradeName,C.CompanySocialReason,C.CountryID AS CompanyCountryID,
	   'UserRoleCodes' = STUFF((SELECT ','+ R.RoleCode FROM [Security].UserRole UR WITH(NOLOCK)
				  INNER JOIN [Security].[Role] R WITH(NOLOCK) ON UR.RoleID= R.RoleID
				  WHERE UR.UserID=U.UserID AND R.StateID=1 FOR XML PATH('')),1,1,''
		)
		FROM [Security].[User] U WITH(NOLOCK)	
		INNER JOIN [Security].UserCompany UC WITH(NOLOCK) ON U.UserID=UC.UserID
		INNER JOIN [Security].Company C WITH(NOLOCK) ON C.CompanyID=UC.CompanyID
		WHERE C.CompanyDocumentNumber=@CompanyDocumentNumber
		AND U.UserName=@UserName
		AND U.UserPassword=@UserPassword 
		AND UC.StateID=1 
		AND C.StateID=1	 
	SET NOCOUNT OFF
END