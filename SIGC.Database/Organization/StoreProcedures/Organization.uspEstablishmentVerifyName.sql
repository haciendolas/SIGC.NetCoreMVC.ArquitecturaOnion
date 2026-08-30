
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            24/05/2026
   Description:            Permite verificar el nombre del establecimiento en la tabla Organization.Establishment
   Execute:	
		  DECLARE @RetMsg VARCHAR(11)  
		  EXECUTE Organization.uspEstablishmentVerifyName
		    @CompanyID = 1,
			@PersonID = 1,
			@EstablishmentID=4,
			@EstablishmentName='Tienda 1',		  
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Organization.uspEstablishmentVerifyName
   @CompanyID INT,
   @PersonID INT,
   @EstablishmentID INT,   
   @EstablishmentName VARCHAR(50),
   @RetMsg VARCHAR(11) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'
	IF EXISTS(SELECT E.EstablishmentID FROM Organization.Establishment E WHERE E.EstablishmentName=@EstablishmentName AND E.CompanyID=@CompanyID
	     AND E.EstablishmentID<>@EstablishmentID AND E.RecordStateID<>2
	)
	BEGIN	  
	  SET @RetMsg = 'NAME_EXISTS'
	END	 
  SET NOCOUNT OFF;
END