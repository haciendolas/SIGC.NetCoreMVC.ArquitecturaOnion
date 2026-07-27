/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            23/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogVariantValue
   Execute:	 

		  EXECUTE Product.uspCatalogVariantValueCreate 
			@CatalogVariantID=1,			
			@AttributeValueID=1,
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
CREATE PROCEDURE Product.uspCatalogVariantValueCreate
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