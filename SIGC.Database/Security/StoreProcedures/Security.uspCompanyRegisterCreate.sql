/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            11/10/2025
   Description:            Permite crear un registro en la tabla  [Security].CompanyRegister
   Execute:
				  
		  EXECUTE Security.uspCompanyRegisterCreate 
			@CompanyIDRegister=1,
			@CompanyID =1	 
			@CompanyRegisterCreatedDateTime='2025-09-02 11:00'
			@CompanyRegisterCreatedUserID=1							   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE [Security].uspCompanyRegisterCreate
(  @CompanyIDRegister INT,
   @CompanyID INT,
   @CompanyRegisterCreatedUserID INT,
   @CompanyRegisterCreatedDateTime DATETIME 
)
AS
BEGIN 
  INSERT INTO [Security].CompanyRegister(CompanyIDRegister, CompanyID, CompanyRegisterCreatedDateTime, CompanyRegisterCreatedUserID)
  VALUES(@CompanyIDRegister, @CompanyID, @CompanyRegisterCreatedDateTime, @CompanyRegisterCreatedUserID) 
END