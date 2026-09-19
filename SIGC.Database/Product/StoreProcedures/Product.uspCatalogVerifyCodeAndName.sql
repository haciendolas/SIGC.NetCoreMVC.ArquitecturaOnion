
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite verificar un registro de la columna CatalogCode y CatalogName en la tabla Product.[Catalog]
   Execute:	
		  DECLARE @RetMsg VARCHAR(20)  
		  EXECUTE Product.uspCatalogVerifyCodeAndName
			@CatalogID=1,
			@CompanyID = 1,
			@CatalogCode =NULL,
			@CatalogName='CUADERNO RALLADO 50 HOJAS',		  
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Product.uspCatalogVerifyCodeAndName
   @CatalogID INT, 
   @CompanyID INT,  
   @CatalogCode NVARCHAR(15),
   @CatalogName NVARCHAR(200),
   @RetMsg VARCHAR(20) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'

	IF(@CatalogCode IS NOT NULL AND @CatalogCode<>'')
	BEGIN
		IF EXISTS(SELECT C.CatalogID FROM Product.[Catalog] C WHERE C.CompanyID = @CompanyID AND
			C.CatalogCode=@CatalogCode AND C.CatalogID<>@CatalogID AND C.RecordStateID<>2
		)
		BEGIN	  
		  SET @RetMsg = 'CODE_EXISTS'
		END	  
	END

	IF EXISTS(SELECT C.CatalogID FROM Product.[Catalog] C WHERE C.CompanyID = @CompanyID AND
		  C.CatalogName=@CatalogName AND C.CatalogID<>@CatalogID AND C.RecordStateID<>2
		)
		BEGIN	 
		  IF(@RetMsg = 'CODE_EXISTS') 
		      SET @RetMsg = 'CODE_AND_NAME_EXISTS'
		  ELSE
		      SET @RetMsg = 'NAME_EXISTS'
	   END	 
 
  SET NOCOUNT OFF;
END