/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            29/08/2026
   Description:            Permite eliminar registros en la tabla Product.CatalogActiveIngredient
   Execute:                
   
   DECLARE @CatalogActiveIngredientListID Product.ttCatalogActiveIngredientListID;

   INSERT INTO @CatalogActiveIngredientListID (Id) VALUES(3),(5),(8);

   EXECUTE Product.uspCatalogActiveIngredientDelete
	         @CompanyID=1,
			 @CatalogID =1,
			 @CatalogActiveIngredientListID = @CatalogActiveIngredientListID; 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogActiveIngredientDelete
(  @CompanyID INT,
   @CatalogID INT,
   @CatalogActiveIngredientListID Product.ttCatalogActiveIngredientListID READONLY  
)
AS
BEGIN 
   DELETE FROM Product.CatalogActiveIngredient WHERE CompanyID=@CompanyID AND CatalogID=@CatalogID
   AND ActiveIngredientID NOT IN(SELECT Id from @CatalogActiveIngredientListID)
END