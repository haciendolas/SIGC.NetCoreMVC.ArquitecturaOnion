/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogTax
   Execute:	  

		  DECLARE @CatalogTaxID INT  
		  EXECUTE Product.uspCatalogTaxCreate 
			@CatalogTaxID=@CatalogTaxID OUTPUT,
			@CompanyID=1,	
			@CatalogID=1,
		    @TaxID=1,		
			@CalculationTypeID=1,
			@TaxDirectionID=1,	
			@TaxAffectationTypeID = 1,		 
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogTaxCreatedUserID=1,
			@CatalogTaxCreatedUserName='administrador',
			@CatalogTaxCreatedUserFullName='Joel Castillo Rojas',
			@CatalogTaxCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogTaxID					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogTaxCreate
(  @CatalogTaxID INT OUTPUT,
   @CompanyID INT,  
   @CatalogID INT,
   @TaxID SMALLINT,   
   @CalculationTypeID SMALLINT,
   @TaxDirectionID TINYINT,
   @TaxAffectationTypeID TINYINT, 
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogTaxCreatedUserID INT,
   @CatalogTaxCreatedUserName NVARCHAR(20),
   @CatalogTaxCreatedUserFullName NVARCHAR(80),
   @CatalogTaxCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogTax(
     CompanyID,	 
	 CatalogID,
	 TaxID,
	 CalculationTypeID,
	 TaxDirectionID,  
	 TaxAffectationTypeID,
     RecordOriginID,
	 RecordStateID,
	 CatalogTaxCreatedUserID,
	 CatalogTaxCreatedUserName,
	 CatalogTaxCreatedUserFullName,
	 CatalogTaxCreatedDateTime)
  VALUES(
    @CompanyID,	 
	@CatalogID,
	@TaxID,
	@CalculationTypeID,
	@TaxDirectionID,  
	@TaxAffectationTypeID,
    @RecordOriginID,
	@RecordStateID,
	@CatalogTaxCreatedUserID,
	@CatalogTaxCreatedUserName,
	@CatalogTaxCreatedUserFullName,
	@CatalogTaxCreatedDateTime
  )

 SET @CatalogTaxID = SCOPE_IDENTITY()
END