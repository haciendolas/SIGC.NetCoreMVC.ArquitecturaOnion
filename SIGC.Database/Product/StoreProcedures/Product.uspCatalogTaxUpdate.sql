/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            09/10/2026
   Description:            Permite actualizar un registro en la tabla Product.CatalogTax
   Execute:	  		 
		  EXECUTE Product.uspCatalogTaxUpdate 
			@CatalogTaxID=1,
			@CompanyID=1,	
			@CatalogID=1,
		    @TaxID=1, 
			@TaxAffectationTypeID = 10,
			@RecordStateID=1,
			@CatalogTaxUpdatedUserID=1,
			@CatalogTaxUpdatedUserName='administrador',
			@CatalogTaxUpdatedUserFullName='Joel Castillo Rojas',
			@CatalogTaxUpdatedDateTime='2025-09-02 11:00' 		 			   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogTaxUpdate
(  @CatalogTaxID INT,
   @CompanyID INT,  
   @CatalogID INT,
   @TaxID SMALLINT,    
   @TaxAffectationTypeID TINYINT,    
   @RecordStateID TINYINT,
   @CatalogTaxUpdatedUserID INT,
   @CatalogTaxUpdatedUserName NVARCHAR(20),
   @CatalogTaxUpdatedUserFullName NVARCHAR(80),
   @CatalogTaxUpdatedDateTime DATETIME
)
AS
BEGIN 
   UPDATE Product.CatalogTax SET 
	 CatalogID = @CatalogID,
	 TaxID = @TaxID, 
	 TaxAffectationTypeID = @TaxAffectationTypeID,     
	 RecordStateID = @RecordStateID,
	 CatalogTaxUpdatedUserID = @CatalogTaxUpdatedUserID,
	 CatalogTaxUpdatedUserName = @CatalogTaxUpdatedUserName,
	 CatalogTaxUpdatedUserFullName = @CatalogTaxUpdatedUserFullName,
	 CatalogTaxUpdatedDateTime = @CatalogTaxUpdatedDateTime
   WHERE CompanyID=@CompanyID AND
	     CatalogTaxID=@CatalogTaxID 
END