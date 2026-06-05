/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            20/09/2025
   Description:            Permite actualizar un registro en la tabla Security.[Role]
   Execute:
	 
		  EXECUTE Security.uspRoleUpdate 
			@RoleID=1,
			@CompanyID=1,
			@RoleCode='03',
			@RoleName='CAJERO',
			@RoleDescription='CAJERO',
			@StateID=1,
			@RoleUpdatedDateTime='2025-09-02 11:00'
			@RoleUpdatedUserID=1  				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Security.uspRoleUpdate
(  @RoleID INT ,
   @CompanyID INT,
   @RoleCode VARCHAR(5),
   @RoleName VARCHAR(50),
   @RoleDescription VARCHAR(150),
   @StateID SMALLINT,
   @RoleUpdatedUserID INT, 
   @RoleUpdatedDateTime DATETIME 
)
AS
BEGIN 
  UPDATE [Security].[Role] SET CompanyID=@CompanyID,
                               RoleCode=@RoleCode,
                               RoleName =@RoleName ,
							   RoleDescription=@RoleDescription,
							   StateID = @StateID, 
							   RoleUpdatedUserID=@RoleUpdatedUserID,
							   RoleUpdatedDateTime=@RoleUpdatedDateTime  
  WHERE RoleID=@RoleID
END