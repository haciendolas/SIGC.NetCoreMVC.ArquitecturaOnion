/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            25/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogLot
   Execute:	  

		  DECLARE @CatalogPriceID INT  
		  EXECUTE Product.uspCatalogPriceCreate 
			@CatalogPriceID=@CatalogPriceID OUTPUT,
			@CompanyID=1,	
			@CatalogPresentationID=2,		  		
			@EstablishmentID=1,
			@PriceTypeID=1,	
			@CurrencyTypeID=1,
			@CatalogPriceAmount = 100,		
			@CatalogPriceIsTaxIncluded = 1,
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogPriceCreatedUserID=1,
			@CatalogPriceCreatedUserName='administrador',
			@CatalogPriceCreatedUserFullName='Joel Castillo Rojas',
			@CatalogPriceCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogPriceID AS CatalogPriceID	 	   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogPriceCreate
(  @CatalogPriceID INT OUTPUT,
   @CompanyID INT,  
   @CatalogPresentationID INT,
   @EstablishmentID INT,
   @PriceTypeID TINYINT,
   @CurrencyTypeID TINYINT, 
   @CatalogPriceAmount NUMERIC(12,6),
   @CatalogPriceIsTaxIncluded BIT,
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogPriceCreatedUserID INT,
   @CatalogPriceCreatedUserName NVARCHAR(20),
   @CatalogPriceCreatedUserFullName NVARCHAR(80),
   @CatalogPriceCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogPrice(
     CompanyID,	 
	 CatalogPresentationID,
	 EstablishmentID,
	 PriceTypeID,
	 CurrencyTypeID,
	 CatalogPriceAmount,
	 CatalogPriceIsTaxIncluded,
     RecordOriginID,
	 RecordStateID,
	 CatalogPriceCreatedUserID,
	 CatalogPriceCreatedUserName,
	 CatalogPriceCreatedUserFullName,
	 CatalogPriceCreatedDateTime)
  VALUES(
     @CompanyID,	 
	 @CatalogPresentationID,
	 @EstablishmentID,
	 @PriceTypeID,
	 @CurrencyTypeID,
	 @CatalogPriceAmount,
	 @CatalogPriceIsTaxIncluded,
     @RecordOriginID,
	 @RecordStateID,
	 @CatalogPriceCreatedUserID,
	 @CatalogPriceCreatedUserName,
	 @CatalogPriceCreatedUserFullName,
	 @CatalogPriceCreatedDateTime
  )

 SET @CatalogPriceID = SCOPE_IDENTITY()
END