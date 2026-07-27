
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite verificar un registro de la columna CatalogName en la tabla Product.[Catalog]
   Execute:	
		  DECLARE @RetMsg VARCHAR(11)  
		  EXECUTE Product.uspCatalogVerifyName
			@CatalogID=1,
			@CompanyID = 1,
			@CatalogName='CUADERNO RALLADO 50 HOJAS',		  
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Product.uspCatalogVerifyName
   @CatalogID INT, 
   @CompanyID INT,  
   @CatalogName VARCHAR(100),
   @RetMsg VARCHAR(11) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'
	IF EXISTS(SELECT C.CatalogID FROM Product.[Catalog] C WITH(NOLOCK) WHERE C.CompanyID = @CompanyID AND
	  C.CatalogName=@CatalogName AND C.CatalogID<>@CatalogID
	  AND C.RecordStateID<>2
	)
	BEGIN	  
	  SET @RetMsg = 'NAME_EXISTS'
	END	 
  SET NOCOUNT OFF;
END