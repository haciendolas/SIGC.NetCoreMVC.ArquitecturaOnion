
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            30/11/2025
   Description:            Permite verificar un registro de la columnas UserName y UserMail en la tabla [Security].[User]
   Execute:	
		  DECLARE @RetMsg VARCHAR(20)  
		  EXECUTE [Security].uspUserVerifyNameAndMail
			@UserID=1,
			@UserName='administrador',
			@UserMail='jcastillorro@hotmail.com',		 
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE [Security].uspUserVerifyNameAndMail
   @UserID INT,   
   @UserName VARCHAR(15),
   @UserMail VARCHAR(100),
   @RetMsg VARCHAR(20) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'
	IF EXISTS(SELECT UserID FROM [Security].[User] U WITH(NOLOCK) WHERE U.UserName=@UserName AND U.UserID<>@UserID)
	BEGIN	  
	  SET @RetMsg = 'USER_EXISTS'
	END

	IF(@UserMail IS NOT NULL OR @UserMail<>'')
	  BEGIN
	    IF EXISTS(SELECT U.UserID FROM [Security].[User] U WITH(NOLOCK) WHERE U.UserMail=@UserMail AND U.UserID<>@UserID)
		BEGIN	
		  IF(@RetMsg = 'USER_EXISTS')	    
			SET @RetMsg ='USER_AND_MAIL_EXISTS'
		  ELSE
			SET @RetMsg='MAIL_EXISTS'	   
		END 
	  END
  SET NOCOUNT OFF;
END