/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            24/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogPresentation
   Execute:	  

		  DECLARE @CatalogPresentationID INT  
		  EXECUTE Product.uspCatalogPresentationCreate 
			@CatalogPresentationID=@CatalogPresentationID OUTPUT,
			@CompanyID=1,	
			@CatalogVariantID=1,
		    @PresentationID=1,		
			@CatalogPresentationIsDefault=1,
			@CatalogPresentationEquivalence=1,	
			@CatalogPresentationSKU = 'SRC-000001',	
			@CatalogPresentationBarcode = NULL,	 
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogPresentationCreatedUserID=1,
			@CatalogPresentationCreatedUserName='administrador',
			@CatalogPresentationCreatedUserFullName='Joel Castillo Rojas',
			@CatalogPresentationCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogPresentationID					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogPresentationCreate
(  @CatalogPresentationID INT OUTPUT,
   @CompanyID INT,  
   @CatalogVariantID INT,
   @PresentationID INT,
   @CatalogPresentationIsDefault BIT,
   @CatalogPresentationEquivalence NUMERIC(10,4),  
   @CatalogPresentationSKU NVARCHAR(50),
   @CatalogPresentationBarcode NVARCHAR(50), 
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogPresentationCreatedUserID INT,
   @CatalogPresentationCreatedUserName NVARCHAR(20),
   @CatalogPresentationCreatedUserFullName NVARCHAR(80),
   @CatalogPresentationCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogPresentation(
     CompanyID,	 
	 CatalogVariantID,
	 PresentationID,
	 CatalogPresentationIsDefault,
	 CatalogPresentationEquivalence,  
	 CatalogPresentationSKU,
	 CatalogPresentationBarcode,
     RecordOriginID,
	 RecordStateID,
	 CatalogPresentationCreatedUserID,
	 CatalogPresentationCreatedUserName,
	 CatalogPresentationCreatedUserFullName,
	 CatalogPresentationCreatedDateTime)
  VALUES(
    @CompanyID,	 
	@CatalogVariantID,
	@PresentationID,
	@CatalogPresentationIsDefault,
	@CatalogPresentationEquivalence,  
	@CatalogPresentationSKU,
	@CatalogPresentationBarcode,
    @RecordOriginID,
	@RecordStateID,
	@CatalogPresentationCreatedUserID,
	@CatalogPresentationCreatedUserName,
	@CatalogPresentationCreatedUserFullName,
	@CatalogPresentationCreatedDateTime
  )

 SET @CatalogPresentationID = SCOPE_IDENTITY()
END