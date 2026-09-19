/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            23/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogVariant
   Execute:	  

		  DECLARE @CatalogVariantID INT  
		  EXECUTE Product.uspCatalogVariantCreate 
			@CatalogVariantID=@CatalogVariantID OUTPUT,
			@CompanyID=1,	
			@CatalogID=1,
		    @CatalogVariantName='Default',
			@CatalogVariantSKU = '2323121212121',
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogVariantCreatedUserID=1,
			@CatalogVariantCreatedUserName='administrador',
			@CatalogVariantCreatedUserFullName='Joel Castillo Rojas',
			@CatalogVariantCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogVariantID					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogVariantCreate
(  @CatalogVariantID INT OUTPUT,
   @CompanyID INT,  
   @CatalogID INT, 
   @CatalogVariantName NVARCHAR(100), 
   @CatalogVariantSKU NVARCHAR(50), 
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogVariantCreatedUserID INT,
   @CatalogVariantCreatedUserName NVARCHAR(20),
   @CatalogVariantCreatedUserFullName NVARCHAR(80),
   @CatalogVariantCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogVariant(
     CompanyID,	 
	 CatalogID,
	 CatalogVariantName, 
	 CatalogVariantSKU,
     RecordOriginID,
	 RecordStateID,
	 CatalogVariantCreatedUserID,
	 CatalogVariantCreatedUserName,
	 CatalogVariantCreatedUserFullName,
	 CatalogVariantCreatedDateTime)
  VALUES(
    @CompanyID,	 
	@CatalogID,
	@CatalogVariantName,
	@CatalogVariantSKU,
    @RecordOriginID,
	@RecordStateID,
	@CatalogVariantCreatedUserID,
	@CatalogVariantCreatedUserName,
	@CatalogVariantCreatedUserFullName,
	@CatalogVariantCreatedDateTime
  )

 SET @CatalogVariantID = SCOPE_IDENTITY()
END