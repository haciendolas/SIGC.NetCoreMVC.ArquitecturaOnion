/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            25/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogAdjustmentDetail
   Execute:	  
		 
		  EXECUTE Product.uspCatalogAdjustmentDetailCreate 			 
			@CompanyID=1,	
			@CatalogAdjustmentID=1,
			@CatalogVariantID=1,
			@CatalogLotID=1,
			@CatalogAdjustmentDetailSystemQuantity=12,
			@CatalogAdjustmentDetailPhysicalQuantity=12,
			@CatalogAdjustmentDetailObservation= NULL,
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogAdjustmentDetailCreatedUserID=1,
			@CatalogAdjustmentDetailCreatedUserName='administrador',
			@CatalogAdjustmentDetailCreatedUserFullName='Joel Castillo Rojas',
			@CatalogAdjustmentDetailCreatedDateTime='2025-09-02 11:00'   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogAdjustmentDetailCreate
(  @CompanyID INT,
   @CatalogAdjustmentID INT,
   @CatalogVariantID INT,
   @CatalogLotID INT,	
   @CatalogAdjustmentDetailSystemQuantity NUMERIC(12,6),
   @CatalogAdjustmentDetailPhysicalQuantity NUMERIC(12,6), 
   @CatalogAdjustmentDetailObservation NVARCHAR(200),
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogAdjustmentDetailCreatedUserID INT,
   @CatalogAdjustmentDetailCreatedUserName NVARCHAR(20),
   @CatalogAdjustmentDetailCreatedUserFullName NVARCHAR(80),
   @CatalogAdjustmentDetailCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogAdjustmentDetail(
	   CompanyID,
	   CatalogAdjustmentID,
	   CatalogVariantID,
	   CatalogLotID,	
	   CatalogAdjustmentDetailSystemQuantity,
	   CatalogAdjustmentDetailPhysicalQuantity, 
	   CatalogAdjustmentDetailObservation,
	   RecordOriginID,
	   RecordStateID,
	   CatalogAdjustmentDetailCreatedUserID,
	   CatalogAdjustmentDetailCreatedUserName,
	   CatalogAdjustmentDetailCreatedUserFullName,
	   CatalogAdjustmentDetailCreatedDateTime)
  VALUES(
       @CompanyID ,
	   @CatalogAdjustmentID ,
	   @CatalogVariantID ,
	   @CatalogLotID,	
	   @CatalogAdjustmentDetailSystemQuantity,
	   @CatalogAdjustmentDetailPhysicalQuantity, 
	   @CatalogAdjustmentDetailObservation,
	   @RecordOriginID,
	   @RecordStateID,
	   @CatalogAdjustmentDetailCreatedUserID,
	   @CatalogAdjustmentDetailCreatedUserName,
	   @CatalogAdjustmentDetailCreatedUserFullName,
	   @CatalogAdjustmentDetailCreatedDateTime
      )
END