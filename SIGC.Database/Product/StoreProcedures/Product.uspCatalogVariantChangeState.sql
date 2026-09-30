/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            27/09/2026
   Description:            Permite cambiar el estado un registro de la tabla Product.CatalogVariant
   Execute:                
         EXECUTE Product.uspCatalogVariantChangeState 
				 @CatalogVariantID=2,
				 @CompanyID = 1,
				 @RecordStateID=1,
				 @CatalogVariantUpdatedUserID=1, 
				 @CatalogVariantUpdatedUserName='administrador',
				 @CatalogVariantUpdatedUserFullName='Joel Castillo Rojas',
				 @CatalogVariantUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogVariantChangeState
( 
   @CatalogVariantID INT,
   @CompanyID INT,
   @RecordStateID TINYINT,
   @CatalogVariantUpdatedUserID INT, 
   @CatalogVariantUpdatedUserName VARCHAR(20),
   @CatalogVariantUpdatedUserFullName VARCHAR(80),
   @CatalogVariantUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Product.CatalogVariant 
	     SET RecordStateID = @RecordStateID,
	        CatalogVariantUpdatedUserID = @CatalogVariantUpdatedUserID,   
			CatalogVariantUpdatedUserName = @CatalogVariantUpdatedUserName,  
			CatalogVariantUpdatedUserFullName = @CatalogVariantUpdatedUserFullName,
			CatalogVariantUpdatedDateTime = @CatalogVariantUpdatedDateTime                    
	       WHERE CatalogVariantID = @CatalogVariantID AND CompanyID = @CompanyID
END