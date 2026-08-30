
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            29/12/2025
   Description:            Permite verificar un registro de la columna CategoryName en la tabla Product.Category
   Execute:	
		  DECLARE @RetMsg VARCHAR(11)  
		  EXECUTE Product.uspCategoryVerifyName
		    @CompanyID = 1,
			@CategoryID=1,
			@CategoryName='Bebidas',		  
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Product.uspCategoryVerifyName
   @CompanyID INT,
   @CategoryID INT,   
   @CategoryName VARCHAR(100),
   @RetMsg VARCHAR(11) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'
	IF EXISTS(SELECT C.CategoryID FROM Product.Category C WHERE C.CategoryName=@CategoryName AND C.CompanyID=@CompanyID
	     AND C.CategoryID<>@CategoryID AND C.RecordStateID<>2
	)
	BEGIN	  
	  SET @RetMsg = 'NAME_EXISTS'
	END	 
  SET NOCOUNT OFF;
END