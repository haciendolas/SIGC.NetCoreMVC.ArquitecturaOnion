/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            25/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogAdjustment
   Execute:	  

		  DECLARE @CatalogAdjustmentID INT  
		  EXECUTE Product.uspCatalogAdjustmentCreate 
			@CatalogAdjustmentID=@CatalogAdjustmentID OUTPUT,
			@CompanyID=1,	
			@WarehouseID=1,
			@CatalogAdjustmentNumber='AJUST-0001',
			@CatalogAdjustmentDate='2026-07-25',
			@CatalogAdjustmentObservation= 'AJUSTE DE INVENTARIO POR SALDO INICIAL',
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogAdjustmentCreatedUserID=1,
			@CatalogAdjustmentCreatedUserName='administrador',
			@CatalogAdjustmentCreatedUserFullName='Joel Castillo Rojas',
			@CatalogAdjustmentCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogAdjustmentID AS CatalogAdjustmentID	 	   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogAdjustmentCreate
(  @CatalogAdjustmentID INT OUTPUT,
   @CompanyID INT,  
   @WarehouseID INT,
   @CatalogAdjustmentNumber NVARCHAR(20),
   @CatalogAdjustmentDate DATE, 
   @CatalogAdjustmentObservation NVARCHAR(300),  
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogAdjustmentCreatedUserID INT,
   @CatalogAdjustmentCreatedUserName NVARCHAR(20),
   @CatalogAdjustmentCreatedUserFullName NVARCHAR(80),
   @CatalogAdjustmentCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogAdjustment(
	   CompanyID,  
	   WarehouseID,
	   CatalogAdjustmentNumber,
	   CatalogAdjustmentDate, 
	   CatalogAdjustmentObservation,  
	   RecordOriginID,
	   RecordStateID,
	   CatalogAdjustmentCreatedUserID,
	   CatalogAdjustmentCreatedUserName,
	   CatalogAdjustmentCreatedUserFullName,
	   CatalogAdjustmentCreatedDateTime)
  VALUES(
       @CompanyID,  
	   @WarehouseID,
	   @CatalogAdjustmentNumber,
	   @CatalogAdjustmentDate, 
	   @CatalogAdjustmentObservation,  
	   @RecordOriginID,
	   @RecordStateID,
	   @CatalogAdjustmentCreatedUserID,
	   @CatalogAdjustmentCreatedUserName,
	   @CatalogAdjustmentCreatedUserFullName,
	   @CatalogAdjustmentCreatedDateTime
      )

 SET @CatalogAdjustmentID = SCOPE_IDENTITY()
END