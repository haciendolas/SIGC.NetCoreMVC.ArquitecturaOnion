/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            09/10/2026
   Description:            Permite verificar un registro de las columnas en la tabla Product.CatalogTaxExemption
   Execute:	
		  DECLARE @RetMsg VARCHAR(20)  
		  EXECUTE Product.uspCatalogTaxExemptionVerifyFields
		    @CatalogTaxExemptionID=1,
			@CompanyID = 1,
			@EstablishmentID=1,			
			@CatalogTaxID = 1,		 	  
		    @RetMsg=@RetMsg OUTPUT										 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 CREATE PROCEDURE Product.uspCatalogTaxExemptionVerifyFields
   @CatalogTaxExemptionID INT, 
   @CompanyID INT,  
   @EstablishmentID INT,
   @CatalogTaxID SMALLINT,    
   @RetMsg VARCHAR(20) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
     SET @RetMsg='OK'  
    
	 IF EXISTS(SELECT 1 FROM Product.CatalogTaxExemption CTE WHERE CTE.CompanyID = @CompanyID AND
				  CTE.EstablishmentID=@EstablishmentID AND 
				  CTE.CatalogTaxID=@CatalogTaxID AND				 
				  CTE.CatalogTaxExemptionID<>@CatalogTaxExemptionID AND
				  CTE.RecordStateID<>2
	  )
	   BEGIN 
		SET @RetMsg = 'FIELDS_EXISTS'
	   END
  SET NOCOUNT OFF
END