/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            30/11/2025
   Description:            Permite crear un registro en la tabla [Security].[User]
   Execute: 
		  DECLARE @UserID INT  
		  EXECUTE [Security].uspUserCreate 
			@UserID=@UserID OUTPUT,
			@UserFirstName='JUAN CARLOS',
			@UserLastName='ISLA SUARES',
			@UserName='JCARLOS',
			@UserPassword='123456',
			@UserMail = 'jcarlos@hotmail.com',
			@UserPhoto = NULL,
			@StateID=1,
			@UserCreatedDateTime='2025-09-02 11:00',
			@UserCreatedUserID=1					 
		  SELECT @UserID 	   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE [Security].uspUserCreate
(  @UserID INT OUTPUT,
   @UserFirstName VARCHAR(50),
   @UserLastName VARCHAR(30),
   @UserName VARCHAR(15),
   @UserPassword VARCHAR(20),
   @UserMail VARCHAR(100),
   @UserPhoto VARCHAR(100) ,  
   @StateID SMALLINT,
   @UserCreatedUserID INT,
   @UserCreatedDateTime DATETIME 
)
AS
BEGIN 
  INSERT INTO [Security].[User](UserFirstName, UserLastName, UserName, UserPassword, UserMail, UserPhoto, StateID, UserCreatedDateTime, UserCreatedUserID)
  VALUES(@UserFirstName, @UserLastName, @UserName, @UserPassword, @UserMail, @UserPhoto, @StateID, @UserCreatedDateTime, @UserCreatedUserID)
 SET @UserID = SCOPE_IDENTITY()
END