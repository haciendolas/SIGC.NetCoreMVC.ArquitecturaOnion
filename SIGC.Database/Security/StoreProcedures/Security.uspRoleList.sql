 /*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            01/12/2025
   Description:            Permite listar registros de la tabla [Security].[Role] por CompanyID 
   Execute:                Exec [Security].uspRoleList @CompanyID=1 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     
==============================================================================*/
CREATE PROCEDURE [Security].uspRoleList
  @CompanyID INT
AS
BEGIN
  SET NOCOUNT ON
    SELECT R.RoleID,R.RoleCode,R.RoleName
	     FROM [Security].[Role] R WITH(NOLOCK) WHERE R.StateID=1
		 AND R.CompanyID = @CompanyID
 SET NOCOUNT OFF
END