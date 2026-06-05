/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            06/12/2025
   Description:            Permite actualizar un registro en la tabla [Security].UserCompany
   Execute:		 
		  EXECUTE [Security].uspUserCompanyUpdate 
			@CompanyID =2
			@UserID= 2,			 
			@StateID=1,
			@UserCompanyUpdatedDateTime='2025-09-02 11:00',
			@UserCompanyUpdatedUserID=1

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE [Security].uspUserCompanyUpdate
(  @CompanyID INT,
   @UserID INT,   
   @StateID SMALLINT,
   @UserCompanyUpdatedUserID INT,
   @UserCompanyUpdatedDateTime DATETIME 
)
AS
BEGIN 
  UPDATE [Security].UserCompany SET StateID=@StateID,
                    UserCompanyUpdatedDateTime=@UserCompanyUpdatedDateTime,
	                UserCompanyUpdatedUserID=@UserCompanyUpdatedUserID
  WHERE CompanyID=@CompanyID AND UserID=@UserID
END