 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            27/09/2025
-- Description:            Permite obtener listado de paginas activas de la tabla  [Security].[Page]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspPageList 
-- ============================================================================== 
ALTER PROCEDURE Security.uspPageList
AS
BEGIN
	SET NOCOUNT ON
     
    SELECT P.PageID,P.PageParentID,P.PageHierarchy,P.PageName,P.PageIconName,P.PageOrder
	FROM [Security].[Page] P WITH(NOLOCK) 
	WHERE P.StateID=1 

	SET NOCOUNT OFF
END