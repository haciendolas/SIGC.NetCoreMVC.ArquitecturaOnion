/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            20/09/2025
   Description:            Permite crear un registro en la tabla Security.[Role]
   Execute:
 
		  DECLARE @RoleID INT  
		  EXECUTE Security.uspRoleCreate 
			@RoleID=@RoleID OUTPUT,
			@CompanyID=1,
			@RoleCode='03',
			@RoleName='CAJERO',
			@RoleDescription='CAJERO',
			@StateID=1,
			@RoleCreatedDateTime='2025-09-02 11:00'
			@RoleCreatedUserID=1
					 
		  SELECT @RoleID 	   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Security.uspRoleCreate
(  @RoleID INT OUTPUT,
   @CompanyID INT,
   @RoleCode VARCHAR(5),
   @RoleName VARCHAR(50),
   @RoleDescription VARCHAR(150),
   @StateID SMALLINT,
   @RoleCreatedUserID INT,
   @RoleCreatedDateTime DATETIME 
)
AS
BEGIN 
  INSERT INTO [Security].[Role](CompanyID, RoleCode, RoleName, RoleDescription, StateID, RoleCreatedDateTime, RoleCreatedUserID)
  VALUES(@CompanyID,@RoleCode, @RoleName, @RoleDescription, @StateID, @RoleCreatedDateTime, @RoleCreatedUserID)
 SET @RoleID = IDENT_CURRENT('Security.Role')
END