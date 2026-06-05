/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            05/11/2025
   Description:            Permite cambiar el estado un registro de la tabla Product.CatalogGallery
   Execute:                EXECUTE Product.uspCatalogGalleryChangeState @CatalogGalleryID=2,@StateID=0	              	 
								   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogGalleryChangeState
( 
   @CatalogGalleryID INT,
   @StateID TINYINT  
)
AS
BEGIN 
    UPDATE Product.CatalogGallery SET StateID = @StateID	                            
	       WHERE CatalogGalleryID = @CatalogGalleryID
END