/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            30/05/2026
   Description:            Permite actualizar un registro en la tabla Organization.Establishment
   Execute:	
		  DECLARE @RetMsg VARCHAR(11)   
		  EXECUTE Organization.uspEstablishmentUpdate
		    @CompanyID = 1, 
			@PersonID = 1, 
			@EstablishmentID=1,	
			@TypeID = 1,	
			@EstablishmentCode ='0000',	 
			@EstablishmentName='ESTABLECIMIENTO 2',
			@EstablishmentAddress = 'AV MIRAFLORES - LIMA',
			@EstablishmentLogo = NULL,			 
			@RecordStateID=1,
			@EstablishmentUpdatedUserID= 1,
			@EstablishmentUpdatedUserName = 'administrador',
			@EstablishmentUpdatedUserFullName = 'Joel Castillo',
			@EstablishmentUpdatedDateTime = '2025-09-02 11:00',
			@RetMsg=@RetMsg OUTPUT	

	 SELECT @EstablishmentID AS EstablishmentID ,@RetMsg AS RetMsg				   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Organization.uspEstablishmentUpdate
(  @EstablishmentID INT,
   @CompanyID INT,
   @PersonID INT,
   @TypeID TINYINT,
   @EstablishmentCode VARCHAR(10),  
   @EstablishmentName NVARCHAR(50),
   @EstablishmentAddress NVARCHAR(50),
   @EstablishmentLogo VARCHAR(100) = NULL,
   @RecordStateID TINYINT,
   @EstablishmentUpdatedUserID INT,
   @EstablishmentUpdatedUserName NVARCHAR(20),
   @EstablishmentUpdatedUserFullName NVARCHAR(80),
   @EstablishmentUpdatedDateTime DATETIME,
   @RetMsg VARCHAR(11) OUTPUT
)
AS
BEGIN 
    EXEC Organization.uspEstablishmentVerifyName
		    @CompanyID = @CompanyID,
			@PersonID = @PersonID,
			@EstablishmentID=@EstablishmentID,
			@EstablishmentName=@EstablishmentName,		  
		    @RetMsg=@RetMsg OUTPUT
 	
	 IF(@RetMsg = 'OK')
	 BEGIN
		  UPDATE Organization.Establishment SET	  
				 PersonID = @PersonID,
				 TypeID = @TypeID,
				 EstablishmentCode = @EstablishmentCode,
				 EstablishmentName = @EstablishmentName,
				 EstablishmentAddress = @EstablishmentAddress,
				 EstablishmentLogo = @EstablishmentLogo,
				 RecordStateID = @RecordStateID,
				 EstablishmentUpdatedUserID = @EstablishmentUpdatedUserID,
				 EstablishmentUpdatedUserName = @EstablishmentUpdatedUserName,
				 EstablishmentUpdatedUserFullName = @EstablishmentUpdatedUserFullName,
				 EstablishmentUpdatedDateTime = @EstablishmentUpdatedDateTime		  
		  WHERE CompanyID = @CompanyID AND
	   			EstablishmentID= @EstablishmentID	 
	 END
END 