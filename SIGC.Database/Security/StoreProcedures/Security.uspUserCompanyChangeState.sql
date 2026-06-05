/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            29/11/2025
   Description:            Permite cambiar el estado un registro de la tabla [Security].[UserCompany]
   Execute:
		 
		  EXECUTE [Security].uspUserCompanyChangeState  	 
			@CompanyID=1,
			@UserID=2,
			@UserCreatedUserID=1,
			@UserCreatedDateTime='2025-09-02 11:00',
			@StateID=0   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE [Security].uspUserCompanyChangeState
(   
   @CompanyID INT,
   @UserID INT,
   @StateID SMALLINT,  
   @UserCreatedUserID INT,
   @UserCreatedDateTime DATETIME
   
)
AS
BEGIN 
    UPDATE [Security].UserCompany SET StateID = @StateID ,
	                           UserCompanyUpdatedDateTime = @UserCreatedDateTime,
							   UserCompanyUpdatedUserID = @UserCreatedUserID
	       WHERE CompanyID=@CompanyID AND UserID = @UserID
END