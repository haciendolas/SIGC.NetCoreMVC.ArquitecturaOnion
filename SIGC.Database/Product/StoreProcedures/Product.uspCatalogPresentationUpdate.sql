/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            08/09/2026
   Description:            Permite actualizar un registro en la tabla Product.CatalogPresentation
   Execute:	 
		  EXECUTE Product.uspCatalogPresentationUpdate 
			@CatalogPresentationID=1,
			@CompanyID=1,	
			@CatalogVariantID=1,
		    @PresentationID=1,
			@CatalogPresentationEquivalence=1,	
			@CatalogPresentationSKU = 'SRC-000001',	
			@CatalogPresentationBarcode = NULL,	
			@RecordStateID=1,
			@CatalogPresentationUpdatedUserID=1,
			@CatalogPresentationUpdatedUserName='administrador',
			@CatalogPresentationUpdatedUserFullName='Joel Castillo Rojas',
			@CatalogPresentationUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogPresentationUpdate
(  @CatalogPresentationID INT ,
   @CompanyID INT,  
   @CatalogVariantID INT,
   @PresentationID INT, 
   @CatalogPresentationEquivalence NUMERIC(10,4),  
   @CatalogPresentationSKU NVARCHAR(50),
   @CatalogPresentationBarcode NVARCHAR(50),  
   @RecordStateID TINYINT,
   @CatalogPresentationUpdatedUserID INT,
   @CatalogPresentationUpdatedUserName NVARCHAR(20),
   @CatalogPresentationUpdatedUserFullName NVARCHAR(80),
   @CatalogPresentationUpdatedDateTime DATETIME
)
AS
BEGIN 
  UPDATE Product.CatalogPresentation SET 
     CatalogVariantID=@CatalogVariantID,
	 PresentationID = @PresentationID,	 
	 CatalogPresentationEquivalence = @CatalogPresentationEquivalence,  
	 CatalogPresentationSKU = @CatalogPresentationSKU,
	 CatalogPresentationBarcode = @CatalogPresentationBarcode, 
	 RecordStateID = @RecordStateID,
	 CatalogPresentationCreatedUserID= @CatalogPresentationUpdatedUserID,
	 CatalogPresentationCreatedUserName=@CatalogPresentationUpdatedUserName,
	 CatalogPresentationCreatedUserFullName= @CatalogPresentationUpdatedUserFullName,
	 CatalogPresentationCreatedDateTime = @CatalogPresentationUpdatedDateTime  
  WHERE CompanyID = @CompanyID AND 
	   CatalogPresentationID = @CatalogPresentationID 
END