/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            02/09/2025
   Description:            Permite crear un registro de la tabla Security.Token
   Execute:
	 
		  DECLARE @TokenID INT  
		  EXECUTE Security.uspTokenCreate 
			@TokenID=@TokenID OUTPUT,
			@CompanyID=1,
			@UserID=1,
			@TokenSessionJson=NULL,
			@TokenRefreshRandom='12AASD23LSPQW12KEWW0QSSW2P1WQ2OW03W2PA0OSPZPAPWWQASWZXVVXVBNM',
			@TokenAccessJWT=NULL,
			@TokenCreateDateTime='2025-09-02 11:00',
			@TokenExpirationRandomDateTime='2025-09-02 11:40',
			@TokenExpirationJWTDateTime='2025-09-02 11:30'
		  SELECT @TokenID AS TokenID
	 
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Security.uspTokenCreate
(  @TokenID INT OUTPUT,
   @CompanyID INT,
   @UserID INT,
   @TokenSessionJson NVARCHAR(MAX),
   @TokenRefreshRandom VARCHAR(100),
   @TokenAccessJWT VARCHAR(500),
   @TokenCreateDateTime DATETIME,
   @TokenExpirationRandomDateTime DATETIME,
   @TokenExpirationJWTDateTime DATETIME
)
AS
BEGIN 
 INSERT INTO Security.Token(CompanyID,UserID,TokenSessionJson,TokenRefreshRandom,TokenAccessJWT,TokenCreateDateTime,TokenExpirationRandomDateTime,TokenExpirationJWTDateTime)
        VALUES(@CompanyID,@UserID,@TokenSessionJson,@TokenRefreshRandom,@TokenAccessJWT,@TokenCreateDateTime,@TokenExpirationRandomDateTime,@TokenExpirationJWTDateTime)

 SET @TokenID = IDENT_CURRENT('Security.Token')
END