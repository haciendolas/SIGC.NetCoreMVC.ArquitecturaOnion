/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            06/12/2025
   Description:            Permite actualizar un registro en la tabla [Security].[User]
   Execute: 
		  
		  EXECUTE [Security].uspUserUpdate 
			@UserID=2,
			@UserFirstName='JUAN CARLOS',
			@UserLastName='ISLA SUARES',
			@UserName='JCARLOS',
			@UserPassword='123456',
			@UserMail = 'jcarlos@hotmail.com',
			@UserPhoto = NULL,
			@StateID=1,
			@UserUpdatedDateTime='2025-09-02 11:00',
			@UserUpdatedUserID=1 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE [Security].uspUserUpdate
(  @UserID INT,
   @UserFirstName VARCHAR(50),
   @UserLastName VARCHAR(30),
   @UserName VARCHAR(15),
   @UserPassword VARCHAR(20),
   @UserMail VARCHAR(100),
   @UserPhoto VARCHAR(100) ,  
   @StateID SMALLINT,
   @UserUpdatedUserID INT,
   @UserUpdatedDateTime DATETIME 
)
AS
BEGIN 
  UPDATE [Security].[User] 
      SET UserFirstName = @UserFirstName,
		  UserLastName = @UserLastName,
		  UserName = @UserName,
		  UserPassword = @UserPassword,
		  UserMail = @UserMail, 
		  UserPhoto = @UserPhoto,
		  StateID = @StateID, 
		  UserUpdatedUserID =@UserUpdatedUserID, 
		  UserUpdatedDateTime = @UserUpdatedDateTime
	WHERE UserID=@UserID 
END