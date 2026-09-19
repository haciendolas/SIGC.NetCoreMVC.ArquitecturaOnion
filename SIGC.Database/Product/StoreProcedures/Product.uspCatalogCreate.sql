/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite crear un registro en la tabla Product.[Catalog]
   Execute:	  
		  DECLARE @CatalogID INT  
		  EXECUTE Product.uspCatalogCreate 
			@CatalogID=@CatalogID OUTPUT,
			@CompanyID=1,
			@CatalogTypeID=1,
			@CategoryID=1,
			@CatalogCode='PARACEL-001',
			@CatalogSlug='cuarderno-rallado-5o-hojas',
			@CatalogName='CUADERNO RALLADO 50 HOJAS',
			@CatalogHasVariants=1,
			@SaleConditionID=1,
			@ManufacturerID=1,	 
			@BrandID=1,
			@PharmaceuticalFormID=1,
			@CatalogBrandType='NINGUNO',
			@CatalogConcentration=NULL,
			@CatalogDescription='',
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogCreatedUserID=1,
			@CatalogCreatedUserName='administrador',
			@CatalogCreatedUserFullName='Joel Castillo Rojas',
			@CatalogCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogID					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogCreate
(  @CatalogID INT OUTPUT,
   @CompanyID INT,
   @CatalogTypeID TINYINT,
   @CategoryID INT, 
   @CatalogCode NVARCHAR(15),
   @CatalogSlug NVARCHAR(200), 
   @CatalogName NVARCHAR(200), 
   @CatalogHasVariants BIT,
   @SaleConditionID TINYINT ,
   @ManufacturerID INT,
   @BrandID INT,
   @PharmaceuticalFormID SMALLINT,
   @CatalogBrandType NVARCHAR(15),
   @CatalogImage VARCHAR(100),
   @CatalogConcentration NVARCHAR(100),
   @CatalogDescription NVARCHAR(300),
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogCreatedUserID INT,
   @CatalogCreatedUserName NVARCHAR(20),
   @CatalogCreatedUserFullName NVARCHAR(80),
   @CatalogCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.[Catalog](CompanyID,CatalogTypeID,CategoryID,CatalogCode,CatalogSlug,CatalogName,CatalogHasVariants,SaleConditionID,
     ManufacturerID,BrandID,PharmaceuticalFormID,CatalogBrandType,CatalogConcentration,CatalogDescription,RecordOriginID,RecordStateID,CatalogCreatedUserID,
	 CatalogCreatedUserName,CatalogCreatedUserFullName,CatalogCreatedDateTime)
  VALUES(@CompanyID,@CatalogTypeID,@CategoryID,@CatalogCode,@CatalogSlug,@CatalogName,@CatalogHasVariants,@SaleConditionID,
     @ManufacturerID,@BrandID,@PharmaceuticalFormID,@CatalogBrandType,@CatalogConcentration,@CatalogDescription,@RecordOriginID,@RecordStateID,@CatalogCreatedUserID,
	 @CatalogCreatedUserName,@CatalogCreatedUserFullName,@CatalogCreatedDateTime)
 SET @CatalogID = SCOPE_IDENTITY()
END