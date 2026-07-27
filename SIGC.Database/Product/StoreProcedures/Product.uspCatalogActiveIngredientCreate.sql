/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogActiveIngredient
   Execute:	  

		  DECLARE @CatalogActiveIngredientID INT  
		  EXECUTE Product.uspCatalogActiveIngredientCreate 
			@CatalogActiveIngredientID=@CatalogActiveIngredientID OUTPUT,
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

		  SELECT @CatalogActiveIngredientID					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogActiveIngredientCreate
(  @CatalogActiveIngredientID INT OUTPUT,
   @CompanyID INT,
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

 SET @CatalogActiveIngredientID = SCOPE_IDENTITY()
END