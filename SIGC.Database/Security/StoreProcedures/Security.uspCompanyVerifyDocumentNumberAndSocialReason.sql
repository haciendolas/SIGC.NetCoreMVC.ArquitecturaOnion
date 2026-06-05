
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            11/10/2025
   Description:            Permite verificar un registro de la columnas CompanyDocumentNumber y CompanySocialReason en la tabla [Security].Company 
   Execute:
		
		  DECLARE @RetMsg VARCHAR(25)  
		  EXECUTE [Security].uspCompanyVerifyDocumentNumberAndSocialReason		
			@CompanyID=2,		 
			@CompanyDocumentNumber='10404358086',
			@CompanySocialReason='HACIENDOLAS SAC',		 
		    @RetMsg=@RetMsg OUTPUT
							 
		  SELECT @RetMsg							   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE [Security].uspCompanyVerifyDocumentNumberAndSocialReason   
   @CompanyID INT,
   @CompanyDocumentNumber VARCHAR(11),
   @CompanySocialReason VARCHAR(150),
   @RetMsg VARCHAR(25) OUTPUT
AS
BEGIN   
   SET NOCOUNT ON;
    SET @RetMsg='OK'

	IF EXISTS(SELECT C.CompanyID FROM [Security].Company C WITH(NOLOCK) WHERE C.CompanyDocumentNumber=@CompanyDocumentNumber
	      AND C.CompanyID<>@CompanyID
    )
	BEGIN	  
	  SET @RetMsg='DOCUMENT_NUMBER_EXISTS'	  
	  RETURN 
	END

    IF EXISTS(SELECT C.CompanyID FROM [Security].Company C WITH(NOLOCK) WHERE C.CompanySocialReason=@CompanySocialReason
	    AND C.CompanyID<>@CompanyID
    )
	BEGIN	  
	  SET @RetMsg='SOCIAL_REASON_EXISTS'	
	  RETURN 
	END

	 SET NOCOUNT OFF;
END
