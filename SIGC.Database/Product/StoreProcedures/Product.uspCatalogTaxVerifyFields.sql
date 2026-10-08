/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            07/10/2026
   Description:            Permite verificar un registro de las columnas en la tabla Product.CatalogTax
   Execute:	
		  DECLARE @RetMsg VARCHAR(20)  
		  EXECUTE Product.uspCatalogTaxVerifyFields
		    @CatalogTaxID=0,
			@CompanyID = 1,
			@CatalogID=1,			
			@TaxID = 1,
			@TaxAffectationTypeID = 10,		 		  
		    @RetMsg=@RetMsg OUTPUT	
									 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 CREATE PROCEDURE Product.uspCatalogTaxVerifyFields
   @CatalogTaxID INT, 
   @CompanyID INT,  
   @CatalogID INT,
   @TaxID SMALLINT,
   @TaxAffectationTypeID TINYINT,   
   @RetMsg VARCHAR(20) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
     SET @RetMsg='OK'  
    
	 IF EXISTS(SELECT 1 FROM Product.CatalogTax CT WHERE CT.CompanyID = @CompanyID AND
				  CT.CatalogID=@CatalogID AND 
				  CT.TaxID=@TaxID AND
				  CT.TaxAffectationTypeID=@TaxAffectationTypeID AND
				  CT.CatalogTaxID<>@CatalogTaxID AND
				  CT.RecordStateID<>2
	  )
	   BEGIN 
		SET @RetMsg = 'FIELDS_EXISTS'
	   END
  SET NOCOUNT OFF
END