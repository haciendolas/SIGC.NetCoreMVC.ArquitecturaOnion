/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            27/09/2025
   Description:            Permite crear un registro en la tabla Product.[Catalog]
   Execute:	  
		  DECLARE @CatalogID INT  
		  EXECUTE Product.uspCatalogCreate 
			@CatalogID=@CatalogID OUTPUT,
			@CompanyID=1,
			@CategoryID=1,	
			@UnitMeasureID=1,		 
			@CatalogName='CUADERNO RALLADO 50 HOJAS',
			@CatalogSlug='cuarderno-rallado-5o-hojas,
			@CatalogSalePrice=10,
			@CatalogDiscount=1,
			@CatalogUnitInStock=15,
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
   @UnitMeasureID INT,
   @CatalogName VARCHAR(200),  
   @CatalogSlug VARCHAR(200), 
   @CatalogSalePrice NUMERIC(10,2) ,
   @CatalogDiscount NUMERIC(10,2),
   @CatalogUnitInStock NUMERIC(10,2),
   @CatalogDescription VARCHAR(300),
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogCreatedUserID INT,
   @CatalogCreatedUserName VARCHAR(20),
   @CatalogCreatedUserFullName VARCHAR(80),
   @CatalogCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.[Catalog](CompanyID,CatalogTypeID,CategoryID,UnitMeasureID,CatalogSlug,CatalogName,CatalogSalePrice,
     CatalogDiscount,CatalogUnitInStock,CatalogDescription,RecordOriginID,RecordStateID,CatalogCreatedUserID,
	 CatalogCreatedUserName,CatalogCreatedUserFullName,CatalogCreatedDateTime)
  VALUES(@CompanyID,@CatalogTypeID,@CategoryID,@UnitMeasureID,@CatalogSlug,@CatalogName,@CatalogSalePrice,
     @CatalogDiscount,@CatalogUnitInStock,@CatalogDescription,@RecordOriginID,@RecordStateID,@CatalogCreatedUserID,
	 @CatalogCreatedUserName,@CatalogCreatedUserFullName,@CatalogCreatedDateTime)
 SET @CatalogID = SCOPE_IDENTITY()
END