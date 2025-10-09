 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            08/10/2025
-- Description:            Permite obtener listado de compañias de la tabla  [Security].[Company]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspCompanyList @CompanyIDRegister=1
-- ============================================================================== 
CREATE PROCEDURE [Security].uspCompanyList
 @CompanyIDRegister INT
AS
BEGIN
	SET NOCOUNT ON
     
    SELECT C.CompanyID,C.CompanyDocumentNumber,C.CompanySocialReason	       
	FROM [Security].Company C WITH(NOLOCK) 
	INNER JOIN [Security].CompanyRegister CR WITH(NOLOCK) ON C.CompanyID=CR.CompanyID
	WHERE C.StateID=1 AND CR.CompanyIDRegister=@CompanyIDRegister

	SET NOCOUNT OFF
END