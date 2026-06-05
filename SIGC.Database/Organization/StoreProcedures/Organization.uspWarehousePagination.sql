 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            01/06/2026
-- Description:            Permite obtener listado paginado de tabla Organization.Warehouse
-- Update:				   Joel Castillo Rojas    
-- Execute                   
/* 
    DECLARE @RecordsTotal INT 
    Exec Organization.uspWarehousePagination @CompanyID=1,@EstablishmentID=6,@RecordStateID=10, @WarehouseName='',@PageNumber=1,@PageSize=10,
	@RecordsTotal=@RecordsTotal OUTPUT

	SELECT @RecordsTotal AS RecordsTotal 
*/
-- ============================================================================== 
ALTER PROCEDURE Organization.uspWarehousePagination(
   @CompanyID INT,   
   @EstablishmentID INT,
   @WarehouseName VARCHAR(50),  
   @RecordStateID TINYINT = 10,  
   @PageNumber INT=1,
   @PageSize INT=10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON

    IF @PageNumber < 1 SET @PageNumber = 1;
    IF @PageSize <= 0 SET @PageSize = 10;

    SET @RecordsTotal = (SELECT COUNT(W.WarehouseID) FROM Organization.Warehouse W WITH(NOLOCK) WHERE W.CompanyID=@CompanyID
	                     AND (@EstablishmentID IS NULL OR @EstablishmentID=0 OR W.EstablishmentID=@EstablishmentID)
	                     AND W.RecordStateID<>2)
	 
    SELECT W.WarehouseID,W.WarehouseCode,W.WarehouseName,
	      E.EstablishmentCode,E.EstablishmentName, W.RecordStateID,
		  ISNULL(W.WarehouseUpdatedDateTime,W.WarehouseCreatedDateTime) AS WarehouseLastUpdatedDateTime,
		  ISNULL(W.WarehouseUpdatedUserID,W.WarehouseCreatedUserID) AS WarehouseLastUpdatedUserID,
		  ISNULL(W.WarehouseUpdatedUserName,W.WarehouseCreatedUserName) AS WarehouseLastUpdatedUserName,
		  ISNULL(W.WarehouseUpdatedUserFullName,W.WarehouseCreatedUserFullName) AS WarehouseLastUpdatedUserFullName,
		 COUNT(W.WarehouseID) OVER() AS RecordsFiltered
	 FROM Organization.Warehouse W WITH(NOLOCK) 
	 INNER JOIN Organization.Establishment E WITH(NOLOCK) ON W.EstablishmentID=e.EstablishmentID   
	  WHERE W.RecordStateID<>2
	       AND W.CompanyID = @CompanyID	
		   AND (@EstablishmentID IS NULL OR @EstablishmentID=0 OR W.EstablishmentID=@EstablishmentID)	 
	       AND (@RecordStateID IS NULL OR @RecordStateID=10 OR W.RecordStateID=@RecordStateID)
		   AND (@WarehouseName IS NULL OR @WarehouseName = '' OR W.WarehouseName LIKE '%' + @WarehouseName + '%' )
	 ORDER BY W.WarehouseID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END