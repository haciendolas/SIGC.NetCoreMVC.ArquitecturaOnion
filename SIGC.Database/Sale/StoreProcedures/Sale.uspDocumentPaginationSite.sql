 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            09/12/2025
-- Description:            Permite obtener listado paginado de tabla Sale.Document
-- Update:				   Joel Castillo Rojas    
-- Execute                   
/* 
    DECLARE @RecordsTotal INT 
    Exec Sale.uspDocumentPaginationSite 
	         @CompanyID=1,
	         @CustomerID=0,
			 @DocumentSerie='',
			 @DocumentCorrelative='',
			 @DocumentIssueDate= NULL,
			 @DocumentStateID = 1,
			 @PageNumber=1,@PageSize=100,
			 @RecordsTotal=@RecordsTotal OUTPUT

	SELECT @RecordsTotal AS RecordsTotal

*/
-- ============================================================================== 
ALTER PROCEDURE Sale.uspDocumentPaginationSite(
   @CompanyID INT,
   @CustomerID INT,
   @DocumentSerie VARCHAR(4),
   @DocumentCorrelative VARCHAR(20),  
   @DocumentIssueDate DATE,
   @DocumentStateID TINYINT, 
   @PageNumber INT=1,
   @PageSize INT=10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON

    SET @RecordsTotal = (SELECT COUNT(D.DocumentID) FROM Sale.Document D WITH(NOLOCK) WHERE D.RecordStateID<>2 
	                     AND D.CompanyID=@CompanyID
	                     AND ((@CustomerID IS NULL OR @CustomerID=0) OR D.CustomerID=@CustomerID)
						 )
	 
    SELECT D.DocumentID,D.DocumentSerie,D.DocumentCorrelative,(CONVERT(DATETIME, D.DocumentIssueDate) + CONVERT(DATETIME,D.DocumentIssueTime)) AS DocumentIssueDateTime,
	      D.CustomerFullName,D.CustomerAddress,D.CustomerMobilePhone,
		  D.CurrencyTypeID,D.DocumentExchangeRate,D.DocumentTotalAmount,
		  D.DocumentGlosa,D.DocumentStateID,
		 COUNT(*) OVER() AS RecordsFiltered
	 FROM Sale.Document D WITH(NOLOCK) 
	  WHERE D.RecordStateID<>2
	       AND D.CompanyID=@CompanyID
	       AND ((@CustomerID IS NULL OR @CustomerID=0) OR D.CustomerID=@CustomerID)
		   AND ((@DocumentStateID IS NULL OR @DocumentStateID = 0) OR D.DocumentStateID=@DocumentStateID)
	       AND ((@DocumentCorrelative IS NULL OR @DocumentCorrelative = '') OR D.DocumentCorrelative LIKE '%' + @DocumentCorrelative + '%' )
		   AND ((@DocumentCorrelative IS NULL OR @DocumentCorrelative = '') OR D.DocumentCorrelative LIKE '%' + @DocumentCorrelative + '%' )
		   AND (@DocumentIssueDate IS NULL OR (D.DocumentIssueDate = @DocumentIssueDate))
	 ORDER BY D.DocumentID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END