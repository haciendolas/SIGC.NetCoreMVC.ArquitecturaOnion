/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite verificar un registro de la columna CatalogVariantSKU y CatalogVariantName en la tabla Product.CatalogVariant
   Execute:	
		  DECLARE @RetMsg VARCHAR(20)  
		  EXECUTE Product.uspCatalogVariantVerifySKUAndName
			@CatalogVariantID=4,
			@CatalogID =27,
			@CompanyID = 1,
			@CatalogVariantSKU =NULL,
			@CatalogVariantName='Color/Rojo/Rojo/Blanco - Talla/L',		  
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Product.uspCatalogVariantVerifySKUAndName
   @CatalogVariantID INT, 
   @CatalogID INT,
   @CompanyID INT,  
   @CatalogVariantSKU NVARCHAR(50),
   @CatalogVariantName NVARCHAR(100),
   @RetMsg VARCHAR(20) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'

	IF(@CatalogVariantSKU IS NOT NULL AND @CatalogVariantSKU<>'')
	BEGIN
		IF EXISTS(SELECT 1 FROM Product.CatalogVariant CV WHERE CV.CompanyID = @CompanyID AND
			CV.CatalogVariantSKU=@CatalogVariantSKU AND 
			CV.CatalogID = @CatalogID AND 
			CV.CatalogVariantID<>@CatalogVariantID AND 
			CV.RecordStateID<>2
		)
		BEGIN	  
		  SET @RetMsg = 'SKU_EXISTS'
		END	  
	END

	IF EXISTS(SELECT 1 FROM Product.CatalogVariant CV WHERE CV.CompanyID = @CompanyID AND
		  CV.CatalogVariantName=@CatalogVariantName AND
		  CV.CatalogID = @CatalogID AND 
		  CV.CatalogVariantID<>@CatalogVariantID AND
		  CV.RecordStateID<>2
		)
		BEGIN	 
		  IF(@RetMsg = 'SKU_EXISTS') 
		      SET @RetMsg = 'SKU_AND_NAME_EXISTS'
		  ELSE
		      SET @RetMsg = 'NAME_EXISTS'
	   END	 
 
  SET NOCOUNT OFF;
END