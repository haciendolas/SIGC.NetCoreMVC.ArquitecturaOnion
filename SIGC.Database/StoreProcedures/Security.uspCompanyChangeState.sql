/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            11/10/2025
   Description:            Permite cambiar el estado un registro de la tabla [Security].Company
   Execute:
		/* 		 
		  EXECUTE Security.uspCompanyChangeState  	 
			@CompanyID=1,			 
			@CompanyCreatedUserID=1,
			@CompanyCreatedDateTime='2025-09-02 11:00',
			@StateID=1	 
		*/						   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE [Security].uspCompanyChangeState
(   
   @CompanyID INT,   
   @StateID SMALLINT,  
   @CompanyCreatedUserID INT,
   @CompanyCreatedDateTime DATETIME
   
)
AS
BEGIN 
    UPDATE [Security].Company SET StateID = @StateID ,
	                           CompanyUpdatedDateTime = @CompanyCreatedDateTime,
							   CompanyUpdatedUserID = @CompanyCreatedUserID
	       WHERE CompanyID=@CompanyID
END