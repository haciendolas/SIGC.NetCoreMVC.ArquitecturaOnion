 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            25/05/2026
-- Description:            Permite obtener un establecimiento de la tabla Organization.Establishment
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Organization.uspEstablishmentGet  @CompanyID=1 ,@EstablishmentID=5
-- ============================================================================== 
CREATE PROCEDURE Organization.uspEstablishmentGet(
   @CompanyID INT,
   @EstablishmentID INT 
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT E.EstablishmentID,E.TypeID,E.EstablishmentCode,E.EstablishmentName,E.EstablishmentAddress,E.EstablishmentLogo,E.RecordStateID		 
		FROM Organization.Establishment E WITH(NOLOCK)		 
		WHERE E.CompanyID = @CompanyID AND E.EstablishmentID=@EstablishmentID 
	SET NOCOUNT OFF
END 