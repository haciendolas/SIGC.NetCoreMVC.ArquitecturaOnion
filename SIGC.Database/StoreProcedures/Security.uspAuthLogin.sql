 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            30/08/2025
-- Description:            Permite obtener un usuario apartir de sus credenciales de la tabla Security.[User]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspAuthLogin  @CompanyDocumentNumber='10404358087', @UserName='jcastillorro',@UserPassword='123456'
-- ============================================================================== 
CREATE PROCEDURE Security.uspAuthLogin(
   @CompanyDocumentNumber VARCHAR(11),
   @UserName VARCHAR(15),
   @UserPassword VARCHAR(10)
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT U.UserID,U.UserFirstName,U.UserLastName,U.UserMail,U.StateID,
		C.CompanyID,C.CompanyTradeName,C.CompanySocialReason
		FROM Security.[User] U	
		INNER JOIN Security.UserCompany UC ON U.UserID=UC.UserID
		INNER JOIN Security.Company C ON C.CompanyID=UC.CompanyID
		WHERE C.CompanyDocumentNumber=@CompanyDocumentNumber
		AND U.UserName=@UserName
		AND U.UserPassword=@UserPassword 
		AND UC.StateID=1 
		AND C.StateID=1	 
	SET NOCOUNT OFF
END