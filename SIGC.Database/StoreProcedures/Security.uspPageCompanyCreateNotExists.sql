/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            20/09/2025
   Description:            Permite crear un registro en la tabla Security.PageCompany
   Execute:
		/*  
		  
		  EXECUTE Security.uspPageCompanyCreateNotExists		 
			@CompanyID=1,
			@PageID=1,			 
			@PageCompanyCreatedDateTime='2025-09-02 11:00',
			@PageCompanyCreatedUserID=1
		*/						   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE [Security].uspPageCompanyCreateNotExists
( 
   @CompanyID INT, 
   @PageID INT,
   @PageCompanyCreatedDateTime DATETIME,
   @PageCompanyCreatedUserID INT
)
AS
BEGIN  
  IF NOT EXISTS(SELECT * FROM [Security].PageCompany PC WITH(NOLOCK) WHERE PC.CompanyID=@CompanyID AND PC.PageID=@PageID)
  BEGIN   
	  INSERT INTO [Security].PageCompany(CompanyID, PageID, StateID, PageCompanyCreatedDateTime, PageCompanyCreatedUserID)
	  VALUES(@CompanyID, @PageID, 1, @PageCompanyCreatedDateTime, @PageCompanyCreatedUserID)
  END
END