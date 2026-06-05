/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            27/09/2025
   Description:            Permite actualizar un registro en la tabla Product.[Catalog]
   Execute:
	 	 
		  EXECUTE Product.uspCatalogUpdate 
			@CatalogID=1,
			@CategoryID=1,
			@UnitMeasureID=1,			 
			@CatalogName='COCACOLA 3 L',
			@CatalogSlug='cocacola_3_l',
			@CatalogSalePrice=12,
			@CatalogDiscount=NULL,
			@CatalogUnitInStock=20,
			@CatalogDescription='COCACOLA 3 L',
			@RecordStateID=1,
			@CatalogUpdatedUserID=1,
			@CatalogUpdatedUserName='administrador',
			@CatalogUpdatedUserFullName='Joel Castillo Rojas',
			@CatalogUpdatedDateTime='2025-09-02 11:00' 
		  SELECT @CategoryID		 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogUpdate
(  @CatalogID INT,
   @CatalogTypeID TINYINT,
   @CategoryID INT,
   @UnitMeasureID INT,
   @CatalogName VARCHAR(200),  
   @CatalogSlug VARCHAR(200), 
   @CatalogSalePrice NUMERIC(10,2) ,
   @CatalogDiscount NUMERIC(10,2),
   @CatalogUnitInStock NUMERIC(10,2),
   @CatalogDescription VARCHAR(300),
   @RecordStateID TINYINT,
   @CatalogUpdatedUserID INT, 
   @CatalogUpdatedUserName VARCHAR(20),
   @CatalogUpdatedUserFullName VARCHAR(80),
   @CatalogUpdatedDateTime DATETIME
)
AS
BEGIN 
  UPDATE Product.[Catalog] SET CategoryID = @CategoryID,
                               CatalogTypeID = @CatalogTypeID,
							   UnitMeasureID = @UnitMeasureID,
							   CatalogName = @CatalogName,
							   CatalogSlug = @CatalogSlug,
							   CatalogSalePrice = @CatalogSalePrice,
							   CatalogDiscount = @CatalogDiscount,
							   CatalogUnitInStock = @CatalogUnitInStock,
							   CatalogDescription = @CatalogDescription,
							   RecordStateID = @RecordStateID,
							   CatalogUpdatedUserID = @CatalogUpdatedUserID,   
							   CatalogUpdatedUserName = @CatalogUpdatedUserName,  
							   CatalogUpdatedUserFullName = @CatalogUpdatedUserFullName,
							   CatalogUpdatedDateTime = @CatalogUpdatedDateTime  
         WHERE CatalogID=@CatalogID
END