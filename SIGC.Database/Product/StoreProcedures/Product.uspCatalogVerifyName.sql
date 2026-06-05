
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            29/12/2025
   Description:            Permite verificar un registro de la columna CatalogName en la tabla Product.[Catalog]
   Execute:	
		  DECLARE @RetMsg VARCHAR(11)  
		  EXECUTE Product.uspCatalogVerifyName
			@CatalogID=1,
			@CatalogName='LENTE OSCURO 75%  INTENSITY',		  
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 CREATE PROCEDURE Product.uspCatalogVerifyName
   @CatalogID INT,   
   @CatalogName VARCHAR(100),
   @RetMsg VARCHAR(11) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'
	IF EXISTS(SELECT C.CatalogID FROM Product.[Catalog] C WITH(NOLOCK) WHERE C.CatalogName=@CatalogName AND C.CatalogID<>@CatalogID
	  AND C.RecordStateID<>2
	)
	BEGIN	  
	  SET @RetMsg = 'NAME_EXISTS'
	END	 
  SET NOCOUNT OFF;
END