/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            06/09/2026
   Description:            Permite eliminar un registro en la tabla Product.CatalogTaxExemption
   Execute:  

   EXECUTE Product.uspCatalogTaxExemptionDelete
	         @CompanyID=1,
			 @CatalogTaxExemptionID =1			    				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogTaxExemptionDelete
(  @CompanyID INT,
   @CatalogTaxExemptionID INT    
)
AS
BEGIN 
   DELETE FROM Product.CatalogTaxExemption WHERE CompanyID=@CompanyID AND CatalogTaxExemptionID=@CatalogTaxExemptionID
END