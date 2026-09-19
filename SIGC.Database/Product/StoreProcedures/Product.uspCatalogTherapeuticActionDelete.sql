/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            29/08/2026
   Description:            Permite eliminar registros en la tabla Product.CatalogTherapeuticAction
   Execute:                
   
   DECLARE @TherapeuticActionListID Product.ttTherapeuticActionListID;

   INSERT INTO @TherapeuticActionListID (Id) VALUES(3),(5),(8);

   EXECUTE Product.uspCatalogTherapeuticActionDelete
	         @CompanyID=1,
			 @CatalogID =1,
			 @TherapeuticActionListID = @TherapeuticActionListID; 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogTherapeuticActionDelete
(  @CompanyID INT,
   @CatalogID INT,
   @TherapeuticActionListID Product.ttTherapeuticActionListID READONLY  
)
AS
BEGIN 
   DELETE FROM Product.CatalogTherapeuticAction WHERE CompanyID=@CompanyID AND CatalogID=@CatalogID
   AND TherapeuticActionID NOT IN(SELECT Id from @TherapeuticActionListID)
END