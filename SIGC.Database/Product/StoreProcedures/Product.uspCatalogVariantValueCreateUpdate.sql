/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            23/07/2026
   Description:            Permite crear y actualizar un registro en la tabla Product.CatalogVariantValue
   Execute:	 

		  EXECUTE Product.uspCatalogVariantValueCreateUpdate 
			@CatalogVariantID=1,			
			@AttributeValueID=3,
			@CompanyID=1,		  
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogVariantValueCreatedUserID=1,
			@CatalogVariantValueCreatedUserName='administrador',
			@CatalogVariantValueCreatedUserFullName='Joel Castillo Rojas',
			@CatalogVariantValueCreatedDateTime='2025-09-02 11:00'  			   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogVariantValueCreateUpdate
(  @CatalogVariantID INT,
   @AttributeValueID SMALLINT,  
   @CompanyID INT,  
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogVariantValueCreatedUserID INT,
   @CatalogVariantValueCreatedUserName NVARCHAR(20),
   @CatalogVariantValueCreatedUserFullName NVARCHAR(80),
   @CatalogVariantValueCreatedDateTime DATETIME
)
AS
BEGIN 
   IF EXISTS(SELECT 1 FROM Product.CatalogVariantValue CVV WHERE CVV.CompanyID = @CompanyID 
                                   AND CVV.CatalogVariantID = @CatalogVariantID
								   AND CVV.AttributeValueID = @AttributeValueID)
     UPDATE Product.CatalogVariantValue SET
	      RecordStateID = @RecordStateID,
		  CatalogVariantValueUpdatedUserID = @CatalogVariantValueCreatedUserID,
		  CatalogVariantValueUpdatedUserName = @CatalogVariantValueCreatedUserName,
		  CatalogVariantValueUpdatedUserFullName = @CatalogVariantValueCreatedUserFullName,
		  CatalogVariantValueUpdatedDateTime = @CatalogVariantValueCreatedDateTime
	 WHERE CompanyID = @CompanyID 
           AND CatalogVariantID = @CatalogVariantID
		   AND AttributeValueID = @AttributeValueID
   ELSE
	  INSERT INTO Product.CatalogVariantValue(
		 CatalogVariantID,	 
		 AttributeValueID,
		 CompanyID, 
		 RecordOriginID,
		 RecordStateID,
		 CatalogVariantValueCreatedUserID,
		 CatalogVariantValueCreatedUserName,
		 CatalogVariantValueCreatedUserFullName,
		 CatalogVariantValueCreatedDateTime)
	  VALUES(
		@CatalogVariantID,	 
		@AttributeValueID,
		@CompanyID,
		@RecordOriginID,
		@RecordStateID,
		@CatalogVariantValueCreatedUserID,
		@CatalogVariantValueCreatedUserName,
		@CatalogVariantValueCreatedUserFullName,
		@CatalogVariantValueCreatedDateTime
	  )
END