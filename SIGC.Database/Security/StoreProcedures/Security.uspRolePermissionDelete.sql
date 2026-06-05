/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            05/10/2025
   Description:            Permite eliminar un registro en la tabla Security.[RolePermission]
   Execute:                 EXECUTE Security.uspRolePermissionDelete @RoleID=1 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE [Security].uspRolePermissionDelete
(  
   @RoleID INT  
)
AS
BEGIN 
   DELETE FROM [Security].RolePermission WHERE RoleID=@RoleID
END