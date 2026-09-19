/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogActiveIngredient
   Execute:	  
		
		  EXECUTE Product.uspCatalogActiveIngredientCreateUpdate			 
			@CompanyID=1,
			@CatalogID=1,
			@ActiveIngredientID=1,
			@CatalogActiveIngredientQuantity=500,
			@UnitMeasureID=1,
			@CatalogActiveIngredientLabel='500 mg',
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogActiveIngredientCreatedUserID=1,
			@CatalogActiveIngredientCreatedUserName='administrador',
			@CatalogActiveIngredientCreatedUserFullName='Joel Castillo Rojas',
			@CatalogActiveIngredientCreatedDateTime='2025-09-02 11:00'			   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogActiveIngredientCreateUpdate
(  @CompanyID INT,
   @CatalogID INT,
   @ActiveIngredientID INT,  
   @CatalogActiveIngredientQuantity NUMERIC(8,3),
   @UnitMeasureID INT,   
   @CatalogActiveIngredientLabel NVARCHAR(100),
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogActiveIngredientCreatedUserID INT,
   @CatalogActiveIngredientCreatedUserName NVARCHAR(20),
   @CatalogActiveIngredientCreatedUserFullName NVARCHAR(80),
   @CatalogActiveIngredientCreatedDateTime DATETIME
)
AS
BEGIN 
  IF EXISTS(SELECT 1 FROM Product.CatalogActiveIngredient CAI WHERE CAI.CompanyID = @CompanyID 
							AND CAI.CatalogID = @CatalogID 
							AND CAI.ActiveIngredientID = @ActiveIngredientID)
	  BEGIN
	    UPDATE Product.CatalogActiveIngredient SET
		 CatalogActiveIngredientQuantity = @CatalogActiveIngredientQuantity,
		 UnitMeasureID = @UnitMeasureID,
		 CatalogActiveIngredientLabel = @CatalogActiveIngredientLabel,
		 RecordStateID = @RecordStateID,
		 CatalogActiveIngredientUpdatedUserID = @CatalogActiveIngredientCreatedUserID,
		 CatalogActiveIngredientUpdatedUserName = @CatalogActiveIngredientCreatedUserName,
		 CatalogActiveIngredientUpdatedUserFullName = @CatalogActiveIngredientCreatedUserFullName,
		 CatalogActiveIngredientUpdatedDateTime = @CatalogActiveIngredientCreatedDateTime
		 WHERE CompanyID = @CompanyID 
			   AND CatalogID = @CatalogID 
			   AND ActiveIngredientID = @ActiveIngredientID
	  END
  ELSE
	 BEGIN
	  INSERT INTO Product.CatalogActiveIngredient(
		 CompanyID,
		 CatalogID,
		 ActiveIngredientID,
		 CatalogActiveIngredientQuantity,
		 UnitMeasureID, 
		 CatalogActiveIngredientLabel,  
		 RecordOriginID,
		 RecordStateID,
		 CatalogActiveIngredientCreatedUserID,
		 CatalogActiveIngredientCreatedUserName,
		 CatalogActiveIngredientCreatedUserFullName,
		 CatalogActiveIngredientCreatedDateTime)
	  VALUES(
		@CompanyID,
		@CatalogID,
		@ActiveIngredientID,
		@CatalogActiveIngredientQuantity,
		@UnitMeasureID,   
		@CatalogActiveIngredientLabel,
		@RecordOriginID,
		@RecordStateID,
		@CatalogActiveIngredientCreatedUserID,
		@CatalogActiveIngredientCreatedUserName,
		@CatalogActiveIngredientCreatedUserFullName,
		@CatalogActiveIngredientCreatedDateTime
	  )
    END
END