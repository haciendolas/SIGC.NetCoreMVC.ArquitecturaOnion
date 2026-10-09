/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            06/10/2026
   Description:            Permite cambiar el estado un registro de la tabla Product.CatalogTax
   Execute:                
         EXECUTE Product.uspCatalogTaxChangeState 
				 @CatalogTaxID=2,
				 @CompanyID = 1,
				 @RecordStateID=1,
				 @CatalogTaxUpdatedUserID=1, 
				 @CatalogTaxUpdatedUserName='administrador',
				 @CatalogTaxUpdatedUserFullName='Joel Castillo Rojas',
				 @CatalogTaxUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogTaxChangeState
( 
   @CatalogTaxID INT,
   @CompanyID INT,
   @RecordStateID TINYINT,
   @CatalogTaxUpdatedUserID INT, 
   @CatalogTaxUpdatedUserName VARCHAR(20),
   @CatalogTaxUpdatedUserFullName VARCHAR(80),
   @CatalogTaxUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Product.CatalogTax 
	     SET RecordStateID = @RecordStateID,
	        CatalogTaxUpdatedUserID = @CatalogTaxUpdatedUserID,   
			CatalogTaxUpdatedUserName = @CatalogTaxUpdatedUserName,  
			CatalogTaxUpdatedUserFullName = @CatalogTaxUpdatedUserFullName,
			CatalogTaxUpdatedDateTime = @CatalogTaxUpdatedDateTime                    
	       WHERE CatalogTaxID = @CatalogTaxID AND CompanyID = @CompanyID
END