/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            03/10/2026
   Description:            Permite cambiar el estado un registro de la tabla Product.CatalogPrice
   Execute:                
         EXECUTE Product.uspCatalogPriceChangeState 
				 @CatalogPriceID=2,
				 @CompanyID = 1,
				 @RecordStateID=1,
				 @CatalogPriceUpdatedUserID=1, 
				 @CatalogPriceUpdatedUserName='administrador',
				 @CatalogPriceUpdatedUserFullName='Joel Castillo Rojas',
				 @CatalogPriceUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogPriceChangeState
( 
   @CatalogPriceID INT,
   @CompanyID INT,
   @RecordStateID TINYINT,
   @CatalogPriceUpdatedUserID INT, 
   @CatalogPriceUpdatedUserName VARCHAR(20),
   @CatalogPriceUpdatedUserFullName VARCHAR(80),
   @CatalogPriceUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Product.CatalogPrice 
	     SET RecordStateID = @RecordStateID,
	        CatalogPriceUpdatedUserID = @CatalogPriceUpdatedUserID,   
			CatalogPriceUpdatedUserName = @CatalogPriceUpdatedUserName,  
			CatalogPriceUpdatedUserFullName = @CatalogPriceUpdatedUserFullName,
			CatalogPriceUpdatedDateTime = @CatalogPriceUpdatedDateTime                    
	       WHERE CatalogPriceID = @CatalogPriceID AND CompanyID = @CompanyID
END