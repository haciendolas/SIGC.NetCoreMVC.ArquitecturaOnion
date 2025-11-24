/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            23/11/2025
   Description:            Permite crear un registro en la tabla Security.PageCompany
   Execute:
		/*  
		  EXECUTE Security.uspPageCompanyCreate		 
			@CompanyID=1,
			@PageID=1,			 
			@PageCompanyCreatedDateTime='2025-09-02 11:00',
			@PageCompanyCreatedUserID=1
		*/						   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE [Security].uspPageCompanyCreate
( 
   @CompanyID INT, 
   @PageID INT,
   @PageCompanyCreatedDateTime DATETIME,
   @PageCompanyCreatedUserID INT
)
AS
BEGIN  
	  INSERT INTO [Security].PageCompany(CompanyID, PageID, StateID, PageCompanyCreatedDateTime, PageCompanyCreatedUserID)
	  VALUES(@CompanyID, @PageID, 1, @PageCompanyCreatedDateTime, @PageCompanyCreatedUserID) 
END