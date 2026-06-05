 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            28/10/2025
-- Description:            Permite listar la galerias del producto de la tabla Product.CatalogGallery
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogGalleryList @CatalogID=1
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogGalleryList
(
 @CatalogID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT CG.CatalogGalleryID,GT.GalleryTypeName,CG.CatalogGalleryFileName,CG.CatalogGalleryPublication,CG.StateID 
		FROM Product.CatalogGallery CG WITH(NOLOCK)
		INNER JOIN [Product].[GalleryType] GT WITH(NOLOCK) ON CG.GalleryTypeID=GT.GalleryTypeID
		WHERE CG.CatalogID=@CatalogID
	SET NOCOUNT OFF
END