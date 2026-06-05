/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            30/05/2026
   Description:            Permite cambiar el estado un registro de la tabla Organization.Establishment
   Execute:

		  EXECUTE Organization.uspEstablishmentChangeState 
		    @CompanyID=1,
			@EstablishmentID=2, 
			@RecordStateID=0,
			@EstablishmentUpdatedUserID= 1,
			@EstablishmentUpdatedUserName = 'administrador',
			@EstablishmentUpdatedUserFullName = 'Joel Castillo',
			@EstablishmentUpdatedDateTime = '2025-09-02 11:00'							   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Organization.uspEstablishmentChangeState
( 
   @CompanyID INT,
   @EstablishmentID INT,
   @RecordStateID TINYINT,
   @EstablishmentUpdatedUserID INT,
   @EstablishmentUpdatedUserName VARCHAR(20),
   @EstablishmentUpdatedUserFullName VARCHAR(80),
   @EstablishmentUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Organization.Establishment SET RecordStateID = @RecordStateID	,
						  EstablishmentUpdatedUserID = @EstablishmentUpdatedUserID,
			              EstablishmentUpdatedUserName = @EstablishmentUpdatedUserName,
						  EstablishmentUpdatedUserFullName = @EstablishmentUpdatedUserFullName,
						  EstablishmentUpdatedDateTime = @EstablishmentUpdatedDateTime                            
	       WHERE EstablishmentID = @EstablishmentID
		     AND CompanyID = @CompanyID
END