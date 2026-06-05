/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            20/09/2025
   Description:            Permite crear un registro en la tabla Security.[RolePermission]
   Execute:		  
		  EXECUTE Security.uspRolePermissionCreate 
			@RoleID=1,
			@CompanyID=1,
			@PageID=1,
			@PageActionID=1, 
			@PageRoleCreatedDateTime='2025-09-02 11:00' 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Security.uspRolePermissionCreate
( 
   @CompanyID INT,
   @RoleID INT,
   @PageID INT,
   @PageActionID INT,
   @PageRoleCreatedDateTime	DATETIME 
)
AS
BEGIN 
  INSERT INTO [Security].RolePermission(CompanyID, RoleID, PageID, PageActionID, PageRoleCreatedDateTime)
  VALUES(@CompanyID, @RoleID, @PageID, @PageActionID, @PageRoleCreatedDateTime)
END