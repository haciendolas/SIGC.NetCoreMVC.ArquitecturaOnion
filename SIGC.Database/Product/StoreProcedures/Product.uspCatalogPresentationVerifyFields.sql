/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            05/09/2026
   Description:            Permite verificar un registro de las columnas en la tabla Product.CatalogPresentation
   Execute:	
		  DECLARE @RetMsg VARCHAR(25)  
		  EXECUTE Product.uspCatalogPresentationVerifyFields
		    @CatalogPresentationID=0,
			@CatalogVariantID=1,
			@CompanyID = 1,
			@PresentationID = 1,
			@CatalogPresentationSKU = 'SRC-000001',
			@CatalogPresentationBarcode='CUADERNO RALLADO 50 HOJAS',		  
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Product.uspCatalogPresentationVerifyFields
   @CatalogPresentationID INT, 
   @CompanyID INT,  
   @CatalogVariantID INT,
   @PresentationID INT,
   @CatalogPresentationSKU NVARCHAR(50),
   @CatalogPresentationBarcode NVARCHAR(50),
   @RetMsg VARCHAR(25) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'

	IF(@CatalogPresentationSKU IS NOT NULL AND @CatalogPresentationSKU<>'')
	BEGIN
		IF EXISTS(SELECT 1 FROM Product.CatalogPresentation CP WHERE CP.CompanyID = @CompanyID AND
			CP.CatalogPresentationSKU = @CatalogPresentationSKU AND 
			CP.RecordStateID<>2
		)
		BEGIN	  
		  SET @RetMsg = 'SKU_EXISTS'
		END	  
	END

	IF(@CatalogPresentationBarcode IS NOT NULL AND @CatalogPresentationBarcode<>'')
	BEGIN
		   IF EXISTS(SELECT 1 FROM Product.CatalogPresentation CP WHERE CP.CompanyID = @CompanyID AND
			  CP.CatalogPresentationBarcode=@CatalogPresentationBarcode AND
			  CP.RecordStateID<>2
			)
			BEGIN	 
			  IF(@RetMsg = 'SKU_EXISTS') 
				  SET @RetMsg = 'SKU_AND_BARCODE_EXISTS'
			  ELSE
				  SET @RetMsg = 'BARCODE_EXISTS'
		   END	 
     END

	 IF(@RetMsg = 'OK')
		 IF EXISTS(SELECT 1 FROM Product.CatalogPresentation CP WHERE CP.CompanyID = @CompanyID AND
				  CP.CatalogVariantID=@CatalogVariantID AND CP.PresentationID=@PresentationID AND
				  CP.CatalogPresentationID<>@CatalogPresentationID AND
				  CP.RecordStateID<>2
		  )
			BEGIN 
				 SET @RetMsg = 'PRESENTATION_EXISTS'
			END 

  SET NOCOUNT OFF
END