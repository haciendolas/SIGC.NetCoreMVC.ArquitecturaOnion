/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            30/11/2025
   Description:            Permite crear un registro en la tabla [Security].[UserCompany]
   Execute:		 
		  EXECUTE [Security].uspUserCompanyCreate 
			@CompanyID =1
			@UserID= 2,			 
			@StateID=1,
			@UserCompanyCreatedDateTime='2025-09-02 11:00',
			@UserCompanyCreatedUserID=1

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE [Security].uspUserCompanyCreate
(  @CompanyID INT,
   @UserID INT,   
   @StateID SMALLINT,
   @UserCompanyCreatedUserID INT,
   @UserCompanyCreatedDateTime DATETIME 
)
AS
BEGIN 
  INSERT INTO [Security].UserCompany(CompanyID,UserID, StateID, UserCompanyCreatedDateTime, UserCompanyCreatedUserID)
  VALUES(@CompanyID,@UserID, @StateID, @UserCompanyCreatedDateTime, @UserCompanyCreatedUserID) 
END