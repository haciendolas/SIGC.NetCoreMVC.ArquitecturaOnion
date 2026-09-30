/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            27/09/2026
   Description:            Permite cambiar el estado un registro de la tabla Product.CatalogPresentation
   Execute:                
         EXECUTE Product.uspCatalogPresentationChangeState 
				 @CatalogPresentationID=2,
				 @CompanyID = 1,
				 @RecordStateID=1,
				 @CatalogPresentationUpdatedUserID=1, 
				 @CatalogPresentationUpdatedUserName='administrador',
				 @CatalogPresentationUpdatedUserFullName='Joel Castillo Rojas',
				 @CatalogPresentationUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogPresentationChangeState
( 
   @CatalogPresentationID INT,
   @CompanyID INT,
   @RecordStateID TINYINT,
   @CatalogPresentationUpdatedUserID INT, 
   @CatalogPresentationUpdatedUserName VARCHAR(20),
   @CatalogPresentationUpdatedUserFullName VARCHAR(80),
   @CatalogPresentationUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Product.CatalogPresentation 
	     SET RecordStateID = @RecordStateID,
	        CatalogPresentationUpdatedUserID = @CatalogPresentationUpdatedUserID,   
			CatalogPresentationUpdatedUserName = @CatalogPresentationUpdatedUserName,  
			CatalogPresentationUpdatedUserFullName = @CatalogPresentationUpdatedUserFullName,
			CatalogPresentationUpdatedDateTime = @CatalogPresentationUpdatedDateTime                    
	       WHERE CatalogPresentationID = @CatalogPresentationID AND CompanyID = @CompanyID
END