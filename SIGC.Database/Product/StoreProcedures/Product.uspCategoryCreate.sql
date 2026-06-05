/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            24/09/2025
   Description:            Permite crear un registro en la tabla Product.Category
   Execute:	
		  DECLARE @CategoryID INT  
		  EXECUTE Product.uspCategoryCreate
		    @CompanyID = 1, 
			@CategoryID=@CategoryID OUTPUT,			 
			@CategoryName='CAJERO',
			@CategorySlug = 'cajero,
			@CategoryImage = NULL
			@RecordOriginID = 1,
			@RecordStateID=1,
			@CategoryCreatedUserID= 1,
			@CategoryCreatedUserName = 'administrador',
			@CategoryCreatedUserFullName = 'Joel Castillo',
			@CategoryCreatedDateTime = '2025-09-02 11:00'
		  SELECT @CategoryID					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCategoryCreate
(  @CategoryID INT OUTPUT,
   @CompanyID INT,
   @CategoryName VARCHAR(100),  
   @CategorySlug VARCHAR(100),
   @CategoryImage VARCHAR(100) = NULL,  
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CategoryCreatedUserID INT,
   @CategoryCreatedUserName VARCHAR(20),
   @CategoryCreatedUserFullName VARCHAR(80),
   @CategoryCreatedDateTime DATETIME 
)
AS
BEGIN 
  INSERT INTO Product.Category(CompanyID,CategoryName,CategorySlug,CategoryImage,RecordOriginID,RecordStateID,CategoryCreatedUserID,
         CategoryCreatedUserName,CategoryCreatedUserFullName,CategoryCreatedDateTime
	  )
  VALUES(@CompanyID,@CategoryName,@CategorySlug,@CategoryImage,@RecordOriginID,@RecordStateID,@CategoryCreatedUserID,
         @CategoryCreatedUserName,@CategoryCreatedUserFullName, @CategoryCreatedDateTime
	   )
 SET @CategoryID = SCOPE_IDENTITY()
END