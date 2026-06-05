/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            30/11/2025
   Description:            Permite crear un registro en la tabla [Security].[UserRole]
   Execute:		 
		  EXECUTE [Security].uspUserRoleCreate 
			@CompanyID = 1					 
			@RoleID=1,
			@UserID=2
			 
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE [Security].uspUserRoleCreate
(  @CompanyID INT,
   @UserID INT,   
   @RoleID INT
)
AS
BEGIN 
  INSERT INTO [Security].UserRole(CompanyID,UserID,RoleID) VALUES(@CompanyID,@UserID, @RoleID) 
END