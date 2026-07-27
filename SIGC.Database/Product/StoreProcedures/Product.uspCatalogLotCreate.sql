/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            25/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogLot
   Execute:	  

		  DECLARE @CatalogLotID INT  
		  EXECUTE Product.uspCatalogLotCreate 
			@CatalogLotID=@CatalogLotID OUTPUT,
			@CompanyID=1,	
			@CatalogVariantID=1,		  		
			@CatalogLotNumber='LOT-001',
			@CatalogLotManufacturingDate='2026-07-25',	
			@CatalogLotExpirationDate = '2028-07-25',		 
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogLotCreatedUserID=1,
			@CatalogLotCreatedUserName='administrador',
			@CatalogLotCreatedUserFullName='Joel Castillo Rojas',
			@CatalogLotCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogLotID		   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogLotCreate
(  @CatalogLotID INT OUTPUT,
   @CompanyID INT,  
   @CatalogVariantID INT,
   @CatalogLotNumber NVARCHAR(50),
   @CatalogLotManufacturingDate DATE,
   @CatalogLotExpirationDate DATE,  
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogLotCreatedUserID INT,
   @CatalogLotCreatedUserName NVARCHAR(20),
   @CatalogLotCreatedUserFullName NVARCHAR(80),
   @CatalogLotCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogLot(
     CompanyID,	 
	 CatalogVariantID,
	 CatalogLotNumber,
	 CatalogLotManufacturingDate,
	 CatalogLotExpirationDate,
     RecordOriginID,
	 RecordStateID,
	 CatalogLotCreatedUserID,
	 CatalogLotCreatedUserName,
	 CatalogLotCreatedUserFullName,
	 CatalogLotCreatedDateTime)
  VALUES(
    @CompanyID,	 
	@CatalogVariantID,
	@CatalogLotNumber,
	@CatalogLotManufacturingDate,
	@CatalogLotExpirationDate,
    @RecordOriginID,
	@RecordStateID,
	@CatalogLotCreatedUserID,
	@CatalogLotCreatedUserName,
	@CatalogLotCreatedUserFullName,
	@CatalogLotCreatedDateTime
  )

 SET @CatalogLotID = SCOPE_IDENTITY()
END