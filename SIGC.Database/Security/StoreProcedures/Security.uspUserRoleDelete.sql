/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            06/11/2025
   Description:            Permite eliminar registros en la tabla [Security].UserRole
   Execute:                 EXECUTE Security.uspUserRoleDelete @CompanyID=1,@UserID=3 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE [Security].uspUserRoleDelete
(  @CompanyID INT,
   @UserID INT  
)
AS
BEGIN 
   DELETE FROM [Security].UserRole WHERE CompanyID=@CompanyID AND UserID=@UserID
END