/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            28/10/2025
   Description:            Permite crear un registro en la tabla Product.CatalogGallery
   Execute:
	 
		  DECLARE @CatalogGalleryID INT  
		  EXECUTE Product.uspCatalogGalleryCreate 
			@CatalogGalleryID=@CatalogGalleryID OUTPUT,		
			@CatalogID = 1,
			@GalleryTypeID = 1,
			@CatalogGalleryFileName='1.png'
			@CatalogGalleryPublication=NULL,
			@StateID=1,
		  SELECT @CatalogGalleryID	 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogGalleryCreate
(  @CatalogGalleryID INT OUTPUT,
   @CatalogID INT,
   @GalleryTypeID TINYINT,
   @CatalogGalleryFileName VARCHAR(100),  
   @CatalogGalleryPublication DATETIME,  
   @StateID TINYINT
)
AS
BEGIN 
   INSERT INTO Product.CatalogGallery(CatalogID,GalleryTypeID,CatalogGalleryFileName,CatalogGalleryPublication,StateID)
   VALUES(@CatalogID,@GalleryTypeID,@CatalogGalleryFileName,@CatalogGalleryPublication,@StateID)  
   SET @CatalogGalleryID = SCOPE_IDENTITY()
END