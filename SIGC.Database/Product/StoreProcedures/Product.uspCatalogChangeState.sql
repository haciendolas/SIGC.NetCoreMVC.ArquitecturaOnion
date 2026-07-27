/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite cambiar el estado un registro de la tabla Product.[Catalog]
   Execute:                
         EXECUTE Product.uspCatalogChangeState 
				 @CatalogID=2,
				 @CompanyID = 1,
				 @RecordStateID=0,
				 @CatalogUpdatedUserID=1, 
				 @CatalogUpdatedUserName='administrador',
				 @CatalogUpdatedUserFullName='Joel Castillo Rojas',
				 @CatalogUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogChangeState
( 
   @CatalogID INT,
   @CompanyID INT,
   @RecordStateID TINYINT,
   @CatalogUpdatedUserID INT, 
   @CatalogUpdatedUserName VARCHAR(20),
   @CatalogUpdatedUserFullName VARCHAR(80),
   @CatalogUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Product.[Catalog] 
	     SET RecordStateID = @RecordStateID,
	        CatalogUpdatedUserID = @CatalogUpdatedUserID,   
			CatalogUpdatedUserName = @CatalogUpdatedUserName,  
			CatalogUpdatedUserFullName = @CatalogUpdatedUserFullName,
			CatalogUpdatedDateTime = @CatalogUpdatedDateTime                    
	       WHERE CatalogID = @CatalogID AND CompanyID = @CompanyID
END