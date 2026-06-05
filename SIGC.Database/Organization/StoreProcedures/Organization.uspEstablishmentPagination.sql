 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            23/05/2026
-- Description:            Permite obtener listado paginado de tabla Organization.Establishment
-- Update:				   Joel Castillo Rojas    
-- Execute                   
/* 
    DECLARE @RecordsTotal INT 
    Exec Organization.uspEstablishmentPagination @CompanyID=1, @PersonID=1,@RecordStateID=10, @EstablishmentName='',@PageNumber=1,@PageSize=10,
	@RecordsTotal=@RecordsTotal OUTPUT

	SELECT @RecordsTotal AS RecordsTotal 
*/
-- ============================================================================== 
ALTER PROCEDURE Organization.uspEstablishmentPagination(
   @CompanyID INT,
   @PersonID INT,
   @EstablishmentName VARCHAR(50),  
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

    SET @RecordsTotal = (SELECT COUNT(E.EstablishmentID) FROM Organization.Establishment E WITH(NOLOCK) WHERE E.CompanyID=@CompanyID
	                     AND E.PersonID = @PersonID AND E.RecordStateID<>2)
	 
    SELECT E.EstablishmentID,E.EstablishmentCode,E.EstablishmentName,E.EstablishmentAddress, 		 
		  E.RecordStateID,
		  ISNULL(E.EstablishmentUpdatedDateTime,E.EstablishmentCreatedDateTime) AS EstablishmentLastUpdatedDateTime,
		  ISNULL(E.EstablishmentUpdatedUserID,E.EstablishmentCreatedUserID) AS EstablishmentLastUpdatedUserID,
		  ISNULL(E.EstablishmentUpdatedUserName,E.EstablishmentCreatedUserName) AS EstablishmentLastUpdatedUserName,
		  ISNULL(E.EstablishmentUpdatedUserFullName,E.EstablishmentCreatedUserFullName) AS EstablishmentLastUpdatedUserFullName,
		 COUNT(E.EstablishmentID) OVER() AS RecordsFiltered
	 FROM Organization.Establishment E WITH(NOLOCK)  	    
	  WHERE E.RecordStateID<>2
	       AND E.CompanyID = @CompanyID 
		   AND E.PersonID = @PersonID 	
	       AND (@RecordStateID IS NULL OR @RecordStateID=10 OR E.RecordStateID=@RecordStateID)
		   AND (@EstablishmentName IS NULL OR @EstablishmentName = '' OR E.EstablishmentName LIKE '%' + @EstablishmentName + '%' )
	 ORDER BY E.EstablishmentID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END