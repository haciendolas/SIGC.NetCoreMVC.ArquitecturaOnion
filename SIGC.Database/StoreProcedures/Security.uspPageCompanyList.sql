 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            27/09/2025
-- Description:            Permite obtener listado de paginas activas de la tabla  [Security].[Page]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspPageCompanyList @CompanyID=1
-- ============================================================================== 
ALTER PROCEDURE Security.uspPageCompanyList
  @CompanyID INT
AS
BEGIN
	SET NOCOUNT ON ;

	WITH PageCompanyCT AS (
		SELECT DISTINCT P.PageID, P.PageHierarchy, P.PageParentID
		FROM Security.PageCompany PC WITH(NOLOCK) 
		INNER JOIN Security.Page P WITH(NOLOCK) ON PC.PageID = P.PageID AND P.StateID = 1
		WHERE PC.CompanyID = @CompanyID		     
	),

	RecursivePageCT AS (   
		SELECT 
			P.PageID,
		    P.PageParentID,
			P.PageHierarchy,   
			P.PageName,
		    P.PageIconName,			
			P.PageOrder
		FROM [Security].[Page] P WITH(NOLOCK)
		INNER JOIN PageCompanyCT PC WITH(NOLOCK) ON P.PageID = PC.PageID

		UNION ALL

		-- Recursividad hacia los padres
			SELECT 
			P.PageID,
		    P.PageParentID,
			P.PageHierarchy,   
			P.PageName,
		    P.PageIconName,		 
			P.PageOrder				
			FROM [Security].[Page] P WITH(NOLOCK)
			INNER JOIN RecursivePageCT RP ON P.PageID = RP.PageParentID	 
	)

	SELECT DISTINCT
			RP.PageID,
		    RP.PageParentID,
			RP.PageHierarchy,   
			RP.PageName,
		    RP.PageIconName,			 
			RP.PageOrder
	FROM RecursivePageCT RP ORDER BY RP.PageHierarchy,RP.PageOrder

	SET NOCOUNT OFF;
END