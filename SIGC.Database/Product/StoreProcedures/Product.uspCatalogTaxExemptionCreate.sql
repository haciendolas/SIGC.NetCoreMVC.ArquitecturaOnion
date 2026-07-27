/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogTaxExemption
   Execute:	  

		  DECLARE @CatalogTaxExemptionID INT  
		  EXECUTE Product.uspCatalogTaxExemptionCreate 
			@CatalogTaxExemptionID=@CatalogTaxExemptionID OUTPUT,
			@CompanyID=1,
			@EstablishmentID=1,	
			@CatalogID=2,
		    @TaxID=1, 
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogTaxExemptionCreatedUserID=1,
			@CatalogTaxExemptionCreatedUserName='administrador',
			@CatalogTaxExemptionCreatedUserFullName='Joel Castillo Rojas',
			@CatalogTaxExemptionCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogTaxExemptionID					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogTaxExemptionCreate
(  @CatalogTaxExemptionID INT OUTPUT,
   @CompanyID INT, 
   @EstablishmentID INT, 
   @CatalogID INT,
   @TaxID SMALLINT, 
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogTaxExemptionCreatedUserID INT,
   @CatalogTaxExemptionCreatedUserName NVARCHAR(20),
   @CatalogTaxExemptionCreatedUserFullName NVARCHAR(80),
   @CatalogTaxExemptionCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogTaxExemption(
     CompanyID,	 
	 EstablishmentID,
	 CatalogID,
	 TaxID, 
     RecordOriginID,
	 RecordStateID,
	 CatalogTaxExemptionCreatedUserID,
	 CatalogTaxExemptionCreatedUserName,
	 CatalogTaxExemptionCreatedUserFullName,
	 CatalogTaxExemptionCreatedDateTime)
  VALUES(
    @CompanyID,	 
	@EstablishmentID,
	@CatalogID,
	@TaxID, 
    @RecordOriginID,
	@RecordStateID,
	@CatalogTaxExemptionCreatedUserID,
	@CatalogTaxExemptionCreatedUserName,
	@CatalogTaxExemptionCreatedUserFullName,
	@CatalogTaxExemptionCreatedDateTime
  )

 SET @CatalogTaxExemptionID = SCOPE_IDENTITY()
END