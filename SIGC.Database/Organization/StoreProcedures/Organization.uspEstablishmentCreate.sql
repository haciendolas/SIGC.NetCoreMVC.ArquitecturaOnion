/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            24/05/2026
   Description:            Permite crear un registro en la tabla Organization.Establishment
   Execute:	
		  DECLARE @EstablishmentID INT,@RetMsg VARCHAR(11)   
		  EXECUTE Organization.uspEstablishmentCreate
		    @CompanyID = 1, 
			@PersonID = 1, 
			@EstablishmentID=@EstablishmentID OUTPUT,	
			@TypeID = 1,	
			@EstablishmentCode ='0000',	 
			@EstablishmentName='ESTABLECIMIENTO 2',
			@EstablishmentAddress = 'AV MIRAFLORES - LIMA',
			@EstablishmentLogo = NULL,
			@RecordOriginID = 1,
			@RecordStateID=1,
			@EstablishmentCreatedUserID= 1,
			@EstablishmentCreatedUserName = 'administrador',
			@EstablishmentCreatedUserFullName = 'Joel Castillo',
			@EstablishmentCreatedDateTime = '2025-09-02 11:00',
			@RetMsg=@RetMsg OUTPUT	

	 SELECT @EstablishmentID AS EstablishmentID ,@RetMsg AS RetMsg				   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Organization.uspEstablishmentCreate
(  @EstablishmentID INT OUTPUT,
   @CompanyID INT,
   @PersonID INT,
   @TypeID TINYINT,
   @EstablishmentCode VARCHAR(10),  
   @EstablishmentName NVARCHAR(50),
   @EstablishmentAddress NVARCHAR(150),
   @EstablishmentLogo VARCHAR(100) = NULL,  
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @EstablishmentCreatedUserID INT,
   @EstablishmentCreatedUserName NVARCHAR(20),
   @EstablishmentCreatedUserFullName NVARCHAR(80),
   @EstablishmentCreatedDateTime DATETIME,
   @RetMsg VARCHAR(11) OUTPUT
)
AS
BEGIN 
  EXEC Organization.uspEstablishmentVerifyName
		    @CompanyID = @CompanyID,
			@PersonID = @PersonID,
			@EstablishmentID=0,
			@EstablishmentName=@EstablishmentName,		  
		    @RetMsg=@RetMsg OUTPUT		
	
 SET @EstablishmentID  = 0			
 IF(@RetMsg = 'OK')
 BEGIN
	  INSERT INTO Organization.Establishment(CompanyID,PersonID,TypeID,EstablishmentCode,
			 EstablishmentName,EstablishmentAddress,EstablishmentLogo,RecordOriginID,RecordStateID,EstablishmentCreatedUserID,
			 EstablishmentCreatedUserName,EstablishmentCreatedUserFullName,EstablishmentCreatedDateTime
		  )
	  VALUES(@CompanyID,@PersonID,@TypeID,@EstablishmentCode,
			 @EstablishmentName,@EstablishmentAddress,@EstablishmentLogo,@RecordOriginID,@RecordStateID,@EstablishmentCreatedUserID,
			 @EstablishmentCreatedUserName,@EstablishmentCreatedUserFullName,@EstablishmentCreatedDateTime
		   )
	 SET @EstablishmentID = SCOPE_IDENTITY()
 END
END