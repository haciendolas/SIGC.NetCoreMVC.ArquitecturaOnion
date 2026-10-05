/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            03/09/2026
   Description:            Permite eliminar un registro en la tabla Product.CatalogPrice
   Execute:  

   EXECUTE Product.uspCatalogPriceDelete
	         @CompanyID=1,
			 @CatalogPriceID =1			    				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
alter PROCEDURE Product.uspCatalogPriceDelete
(  @CompanyID INT,
   @CatalogPriceID INT    
)
AS
BEGIN 
   DELETE FROM Product.CatalogPrice WHERE CompanyID=@CompanyID AND CatalogPriceID=@CatalogPriceID   
END