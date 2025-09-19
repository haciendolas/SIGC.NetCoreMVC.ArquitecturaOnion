/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            19/09/2025
   Description:            Permite cambiar el estado un registro de la tabla Security.[Role]
   Execute:
		/* 		 
		  EXECUTE Security.uspRoleChangeState  	 
			@CompanyID=1,
			@RoleID=2,
			@RoleCreatedUserID=1,
			@RoleCreatedDateTime='2025-09-02 11:00',
			@StateID=0		 
		*/						   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Security.uspRoleChangeState
(   
   @CompanyID INT,
   @RoleID INT,
   @StateID SMALLINT,  
   @RoleCreatedUserID INT,
   @RoleCreatedDateTime DATETIME
   
)
AS
BEGIN 
    UPDATE Security.[Role] SET StateID = @StateID ,
	                           RoleUpdatedDateTime = @RoleCreatedDateTime,
							   RoleUpdatedUserID = @RoleCreatedUserID
	       WHERE CompanyID=@CompanyID AND RoleID = @RoleID
END