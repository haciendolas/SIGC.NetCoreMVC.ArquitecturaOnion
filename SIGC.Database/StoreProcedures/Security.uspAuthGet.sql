 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            30/08/2025
-- Description:            Permite obtener un usuario apartir de su UserID y @companyID tabla Security.[User]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspAuthGet  @UserID=2, @companyID=3
-- ============================================================================== 
CREATE PROCEDURE Security.uspAuthGet(
   @UserID INT,
   @companyID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT U.UserID,u.UserName,U.UserFirstName,U.UserLastName,U.UserMail,U.StateID,
		C.CompanyID,c.CompanyDocumentNumber,C.CompanyTradeName,C.CompanySocialReason
		FROM Security.[User] U	
		INNER JOIN Security.UserCompany UC ON U.UserID=UC.UserID
		INNER JOIN Security.Company C ON C.CompanyID=UC.CompanyID
		WHERE C.CompanyID=@companyID
		AND U.UserID=@UserID 
		AND UC.StateID=1 
		AND C.StateID=1	 
	SET NOCOUNT OFF
END