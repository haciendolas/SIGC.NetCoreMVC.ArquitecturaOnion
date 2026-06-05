/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            24/09/2025
   Description:            Permite actualizar un registro en la tabla Product.Category
   Execute:		 
		  EXECUTE Product.uspCategoryUpdate 
		    @CompanyID=1,
			@CategoryID=1,			 
			@CategoryName='CAJERO',
			@CategorySlug=cajero,
			@CategoryImage=NULL,
			@RecordStateID=1,		 
			@CategoryUpdatedUserID= 1,
		    @CategoryUpdatedUserName = 'administrador',
			@CategoryUpdatedUserFullName = 'Joel Castillo',
			@CategoryUpdatedDateTime = '2025-09-02 11:00'	 	   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCategoryUpdate
(  @CompanyID INT,
   @CategoryID INT ,
   @CategoryName VARCHAR(100),
   @CategorySlug VARCHAR(100),
   @CategoryImage VARCHAR(100) = NULL,
   @RecordStateID TINYINT,
   @CategoryUpdatedUserID INT,
   @CategoryUpdatedUserName VARCHAR(20),
   @CategoryUpdatedUserFullName VARCHAR(80),
   @CategoryUpdatedDateTime DATETIME
)
AS
BEGIN 
  UPDATE Product.Category SET CategoryName = @CategoryName,
                          CategorySlug = @CategorySlug,
                          CategoryImage = @CategoryImage,						 
						  RecordStateID = @RecordStateID,
						  CategoryUpdatedUserID = @CategoryUpdatedUserID,
						  CategoryUpdatedUserName = @CategoryUpdatedUserName,
						  CategoryUpdatedUserFullName = @CategoryUpdatedUserFullName,
						  CategoryUpdatedDateTime = @CategoryUpdatedDateTime
  WHERE CategoryID=@CategoryID AND CompanyID=@CompanyID
END