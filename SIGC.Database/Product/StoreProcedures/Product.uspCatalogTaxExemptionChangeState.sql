/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            10/10/2026
   Description:            Permite cambiar el estado un registro de la tabla Product.CatalogTaxExemption
   Execute:                
         EXECUTE Product.uspCatalogTaxExemptionChangeState 
				 @CatalogTaxExemptionID=2,
				 @CompanyID = 1,
				 @RecordStateID=1,
				 @CatalogTaxExemptionUpdatedUserID=1, 
				 @CatalogTaxExemptionUpdatedUserName='administrador',
				 @CatalogTaxExemptionUpdatedUserFullName='Joel Castillo Rojas',
				 @CatalogTaxExemptionUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogTaxExemptionChangeState
( 
   @CatalogTaxExemptionID INT,
   @CompanyID INT,
   @RecordStateID TINYINT,
   @CatalogTaxExemptionUpdatedUserID INT, 
   @CatalogTaxExemptionUpdatedUserName VARCHAR(20),
   @CatalogTaxExemptionUpdatedUserFullName VARCHAR(80),
   @CatalogTaxExemptionUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Product.CatalogTaxExemption 
	     SET RecordStateID = @RecordStateID,
	        CatalogTaxExemptionUpdatedUserID = @CatalogTaxExemptionUpdatedUserID,   
			CatalogTaxExemptionUpdatedUserName = @CatalogTaxExemptionUpdatedUserName,  
			CatalogTaxExemptionUpdatedUserFullName = @CatalogTaxExemptionUpdatedUserFullName,
			CatalogTaxExemptionUpdatedDateTime = @CatalogTaxExemptionUpdatedDateTime                    
	       WHERE CatalogTaxExemptionID = @CatalogTaxExemptionID AND CompanyID = @CompanyID
END