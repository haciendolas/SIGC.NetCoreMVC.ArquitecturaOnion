/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            04/09/2026
   Description:            Permite eliminar registros en la tabla Product.CatalogVariantValue
   Execute:                
   
   DECLARE @AttributeValueListID Product.ttAttributeValueListID;

   INSERT INTO @AttributeValueListID (Id) VALUES(3),(5);

   EXECUTE Product.uspCatalogVariantValueDelete
	         @CompanyID=1,
			 @CatalogVariantID =1,
			 @AttributeValueListID = @AttributeValueListID; 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogVariantValueDelete
(  @CompanyID INT,
   @CatalogVariantID INT,
   @AttributeValueListID Product.ttAttributeValueListID READONLY  
)
AS
BEGIN 
   DELETE FROM Product.CatalogVariantValue WHERE CompanyID=@CompanyID AND CatalogVariantID=@CatalogVariantID
   AND AttributeValueID NOT IN(SELECT Id from @AttributeValueListID)
END