 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            19/14/2026
-- Description:            Permite obtener listado de establecimiento de la tabla  Organization.Establishment
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Organization.uspEstablishmentList @CompanyID=1,@PersonID=1
-- ============================================================================== 
ALTER PROCEDURE Organization.uspEstablishmentList
 @CompanyID INT,
 @PersonID INT
AS
BEGIN
 SET NOCOUNT ON   
   SELECT E.EstablishmentID,E.EstablishmentName,E.EstablishmentAddress 
    FROM Organization.Establishment E WITH(NOLOCK) 
    WHERE E.CompanyID = @CompanyID
	  AND E.PersonID = @PersonID  
	  AND E.RecordStateID = 1 	         
 SET NOCOUNT OFF
END