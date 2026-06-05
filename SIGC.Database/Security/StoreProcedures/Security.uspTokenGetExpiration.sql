/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            02/09/2025
   Description:            Permite obtener la expiración del token de la tabla Security.Token
   Execute:		 		   
   EXEC Security.uspTokenGetExpiration 
       @UserId=2,
	   @TokenRefreshRandom='iEF1TvoXlQiCKD06p-S4e5T17vvN-90AVqg8MwbK35k',
	   @TokenExpirationDateTime='2025-09-03 01:22'	 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Security.uspTokenGetExpiration
(  
  @UserID INT,
  @TokenRefreshRandom VARCHAR(100),
  @TokenExpirationDateTime DATETIME
)
AS
BEGIN 
  SET NOCOUNT ON

  SELECT TokenID,TokenExpirationRandomDateTime AS TokenExpirationDateTime,TokenRevocationDateTime FROM [Security].[Token]
         WHERE UserID=@UserID 
	       AND TokenRefreshRandom=@TokenRefreshRandom
           AND TokenRevocationDateTime IS NULL
           AND TokenExpirationRandomDateTime>@TokenExpirationDateTime

  SET NOCOUNT OFF
END