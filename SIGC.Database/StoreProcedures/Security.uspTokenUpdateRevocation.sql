/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            02/09/2025
   Description:            Permite actualizar un registro de la tabla Security.Token
   Execute:                
	 	EXEC Security.uspTokenUpdateRevocation @TokenID=1,@TokenRevocationDateTime='2025-09-02 11:38'			   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Security.uspTokenUpdateRevocation
(  
   @TokenID INT, 
   @TokenRevocationDateTime DATETIME
)
AS
BEGIN 
 UPDATE Security.Token SET TokenRevocationDateTime=@TokenRevocationDateTime WHERE TokenID=@TokenID
END