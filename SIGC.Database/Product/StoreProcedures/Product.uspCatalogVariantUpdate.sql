/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            04/09/2026
   Description:             Permite actualizar un registro en la tabla Product.CatalogVariant
   Execute:	  		 
		  EXECUTE Product.uspCatalogVariantUpdate 
			@CatalogVariantID=1,
			@CompanyID=1,	
			@CatalogID=1,
		    @CatalogVariantName='Default',
			@CatalogVariantSKU = '2323231212',		 
			@RecordStateID=1,
			@CatalogVariantUpdatedUserID=1,
			@CatalogVariantUpdatedUserName='administrador',
			@CatalogVariantUpdatedUserFullName='Joel Castillo Rojas',
			@CatalogVariantUpdatedDateTime='2025-09-02 11:00'  

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogVariantUpdate
(  @CatalogVariantID INT,
   @CompanyID INT,  
   @CatalogID INT, 
   @CatalogVariantName NVARCHAR(100), 
   @CatalogVariantSKU NVARCHAR(50),   
   @RecordStateID TINYINT,
   @CatalogVariantUpdatedUserID INT,
   @CatalogVariantUpdatedUserName NVARCHAR(20),
   @CatalogVariantUpdatedUserFullName NVARCHAR(80),
   @CatalogVariantUpdatedDateTime DATETIME
)
AS
BEGIN 
  UPDATE Product.CatalogVariant SET
     CatalogID = @CatalogID,
	 CatalogVariantName = @CatalogVariantName, 
	 CatalogVariantSKU = @CatalogVariantSKU,     
	 RecordStateID = @RecordStateID,
	 CatalogVariantUpdatedUserID = @CatalogVariantUpdatedUserID,
	 CatalogVariantUpdatedUserName = @CatalogVariantUpdatedUserName,
	 CatalogVariantUpdatedUserFullName = @CatalogVariantUpdatedUserFullName,
	 CatalogVariantUpdatedDateTime = @CatalogVariantUpdatedDateTime
 WHERE CompanyID = @CompanyID AND
	   CatalogVariantID = @CatalogVariantID 
END