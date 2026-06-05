/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            24/09/2025
   Description:            Permite cambiar el estado un registro de la tabla Product.Category
   Execute:

		  EXECUTE Product.uspCategoryChangeState 
		    @CompanyID=1,
			@CategoryID=2, 
			@RecordStateID=0,
			@CategoryUpdatedUserID= 1,
			@CategoryUpdatedUserName = 'administrador',
			@CategoryUpdatedUserFullName = 'Joel Castillo',
			@CategoryUpdatedDateTime = '2025-09-02 11:00'							   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCategoryChangeState
( 
   @CompanyID INT,
   @CategoryID INT,
   @RecordStateID TINYINT,
   @CategoryUpdatedUserID INT,
   @CategoryUpdatedUserName VARCHAR(20),
   @CategoryUpdatedUserFullName VARCHAR(80),
   @CategoryUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Product.Category SET RecordStateID = @RecordStateID	,
						  CategoryUpdatedUserID = @CategoryUpdatedUserID,
			              CategoryUpdatedUserName = @CategoryUpdatedUserName,
						  CategoryUpdatedUserFullName = @CategoryUpdatedUserFullName,
						  CategoryUpdatedDateTime = @CategoryUpdatedDateTime                            
	       WHERE CategoryID = @CategoryID
		     AND CompanyID =@CompanyID
END