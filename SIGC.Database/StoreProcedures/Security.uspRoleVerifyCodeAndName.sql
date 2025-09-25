
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            20/09/2025
   Description:            Permite verificar un registro de la columnas RoleCode y RoleName en la tabla Security.[Role]
   Execute:
		/*  
		  DECLARE @RetMsg VARCHAR(11)  
		  EXECUTE Security.uspRoleVerifyCodeAndName		
			@CompanyID=1,
			@RoleID=0,
			@RoleCode='03',
			@RoleName='VENDEDOR',		 
		    @RetMsg=@RetMsg OUTPUT
							 
		  SELECT @RetMsg
		*/						   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Security.uspRoleVerifyCodeAndName
   @RoleID INT ,
   @CompanyID INT,
   @RoleCode VARCHAR(5),
   @RoleName VARCHAR(50),
   @RetMsg VARCHAR(11) OUTPUT
AS
BEGIN   
   SET NOCOUNT ON;
    SET @RetMsg='OK'

	IF EXISTS(SELECT RoleID FROM Security.[Role] R WITH(NOLOCK) WHERE R.RoleCode=@RoleCode
	    AND R.RoleID<>@RoleID AND R.CompanyID=@CompanyID
    )
	BEGIN	  
	  SET @RetMsg='CODE_EXISTS'	  
	  RETURN 
	END

    IF EXISTS(SELECT RoleID FROM Security.[Role] R WITH(NOLOCK) WHERE R.RoleName=@RoleName
	    AND RoleID<>@RoleID AND R.CompanyID=@CompanyID
    )
	BEGIN	  
	  SET @RetMsg='NAME_EXISTS'	
	  RETURN 
	END

	 SET NOCOUNT OFF;
END
