/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite actualizar un registro en la tabla Product.[Catalog]
   Execute:	 	 
		  EXECUTE Product.uspCatalogUpdate 
			@CatalogID= 1 ,
			@CompanyID=1,
			@CatalogTypeID=1,
			@CategoryID=1,
			@CatalogCode='PARACEL-001',
			@CatalogSlug='cuarderno-rallado-5o-hojas',
			@CatalogName='CUADERNO RALLADO 50 HOJAS',
			@PrescriptionTypeID=1,
			@ManufacturerID=1,	 
			@BrandID=1,
			@PharmaceuticalFormID=1,
			@CatalogBrandType='NINGUNO',
			@CatalogDescription='',	
			@RecordStateID=1,
			@CatalogUpdatedUserID=1,
			@CatalogUpdatedUserName='administrador',
			@CatalogUpdatedUserFullName='Joel Castillo Rojas',
			@CatalogUpdatedDateTime='2025-09-02 11:00' 	 	 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogUpdate
(  @CatalogID INT,
   @CompanyID INT,
   @CatalogTypeID TINYINT,
   @CategoryID INT, 
   @CatalogCode NVARCHAR(15),
   @CatalogSlug NVARCHAR(200), 
   @CatalogName NVARCHAR(200), 
   @PrescriptionTypeID TINYINT ,
   @ManufacturerID INT,
   @BrandID INT,
   @PharmaceuticalFormID SMALLINT,
   @CatalogBrandType NVARCHAR(15),
   @CatalogDescription NVARCHAR(300),
   @RecordStateID TINYINT,
   @CatalogUpdatedUserID INT, 
   @CatalogUpdatedUserName VARCHAR(20),
   @CatalogUpdatedUserFullName VARCHAR(80),
   @CatalogUpdatedDateTime DATETIME
)
AS
BEGIN 
  UPDATE Product.[Catalog] SET CatalogTypeID = @CatalogTypeID,
							   CategoryID = @CategoryID,
							   CatalogCode = @CatalogCode,
							   CatalogSlug = @CatalogSlug,
							   CatalogName = @CatalogName,							
							   PrescriptionTypeID = @PrescriptionTypeID,
							   ManufacturerID = @ManufacturerID,
							   BrandID = @BrandID,
							   PharmaceuticalFormID = @PharmaceuticalFormID,
							   CatalogBrandType = @CatalogBrandType,
							   CatalogDescription = @CatalogDescription,
							   RecordStateID = @RecordStateID,
							   CatalogUpdatedUserID = @CatalogUpdatedUserID,   
							   CatalogUpdatedUserName = @CatalogUpdatedUserName,  
							   CatalogUpdatedUserFullName = @CatalogUpdatedUserFullName,
							   CatalogUpdatedDateTime = @CatalogUpdatedDateTime  
         WHERE CatalogID=@CatalogID AND CompanyID=@CompanyID
END