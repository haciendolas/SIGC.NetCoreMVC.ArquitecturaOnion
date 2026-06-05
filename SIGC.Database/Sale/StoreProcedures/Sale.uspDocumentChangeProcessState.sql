/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            28/12/2025
   Description:            Permite cambiar el estado del proceso de un registro de la tabla Sale.Document
   Execute:

		  EXECUTE Sale.uspDocumentChangeProcessState 
			@DocumentID=2, 
			@DocumentStateID=2,
			@DocumentUpdatedUserID= 1,
			@DocumentUpdatedUserName = 'administrador',
			@DocumentUpdatedUserFullName = 'Joel Castillo',
			@DocumentUpdatedDateTime = '2025-09-02 11:00'							   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Sale.uspDocumentChangeProcessState
( 
   @DocumentID INT,
   @DocumentStateID TINYINT,
   @DocumentUpdatedUserID INT,
   @DocumentUpdatedUserName VARCHAR(20),
   @DocumentUpdatedUserFullName VARCHAR(80),
   @DocumentUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Sale.Document SET DocumentStateID = @DocumentStateID	,
						  DocumentUpdatedUserID = @DocumentUpdatedUserID,
			              DocumentUpdatedUserName = @DocumentUpdatedUserName,
						  DocumentUpdatedUserFullName = @DocumentUpdatedUserFullName,
						  DocumentUpdatedDateTime = @DocumentUpdatedDateTime                            
	       WHERE DocumentID = @DocumentID
END