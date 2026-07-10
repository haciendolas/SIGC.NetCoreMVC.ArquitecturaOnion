 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            17/09/2025
-- Description:            Permite obtener listado paginado de tabla Product.[Catalog]
-- Update:				   Joel Castillo Rojas    
-- Execute                   
/* 
    DECLARE @RecordsTotal INT 
    Exec Product.uspCatalogPagination 
									@CompanyID=1,
									@CatalogTypeID=1,
									@CatalogName='',
									@RecordStateID=10,
									@CategoryID=NULL,
									@ManufacturerID=NULL,
									@BrandID=NULL,
									@PageNumber=1,
									@PageSize=100,
									@RecordsTotal=@RecordsTotal OUTPUT

	SELECT @RecordsTotal AS RecordsTotal 
*/
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogPagination(
   @CompanyID INT,
   @CatalogTypeID TINYINT,
   @CatalogName VARCHAR(200),
   @RecordStateID TINYINT, 
   @CategoryID INT,
   @ManufacturerID INT,
   @BrandID INT,  
   @PageNumber INT=1,
   @PageSize INT=10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON

    IF @PageNumber < 1 SET @PageNumber = 1;
    IF @PageSize <= 0 SET @PageSize = 10;

    SET @RecordsTotal = (SELECT COUNT(C.CatalogID) FROM Product.[Catalog] C WITH(NOLOCK) WHERE C.CompanyID=@CompanyID AND C.RecordStateID<>2)
	 
    SELECT C.CatalogID,C.CatalogName,C.CatalogDescription,
	      T.CatalogTypeName,		 
		  CA.CategoryName,	
		  CV.CatalogVariantName,
		  CV.UnitMeasureName,
		  CV.PresentationName,	 
		  ISNULL(B.BrandName,'NO ESPECIFICA') AS BrandName,
		  ISNULL(M.ManufacturerName,'NO ESPECIFICA') AS  ManufacturerName,
		  ActiveIngredient=STUFF(
		     (SELECT ',' + AI.ActiveIngredientName +' :'+ CONVERT(VARCHAR(10),CAI.CatalogActiveIngredientQuantity) 
			 FROM Product.CatalogActiveIngredient CAI WITH(NOLOCK)
			 INNER JOIN Product.ActiveIngredient AI WITH(NOLOCK) ON CAI.ActiveIngredientID=AI.ActiveIngredientID
		     WHERE CAI.CompanyID=C.CompanyID AND CAI.CatalogID=C.CatalogID
			 FOR XML PATH(''),TYPE
			 ).value('.','NVARCHAR(MAX)')
		   ,1 ,1,''),
		  TherapeuticAction= STUFF(
			  (SELECT ',' + TA.TherapeuticActionName FROM Product.CatalogTherapeuticAction CTA WITH(NOLOCK)
			   INNER JOIN Product.TherapeuticAction TA WITH(NOLOCK) ON CTA.TherapeuticActionID=TA.TherapeuticActionID
			   WHERE CTA.CompanyID=C.CompanyID AND CTA.CatalogID=C.CatalogID
			   FOR XML PATH(''), TYPE
			   ).value('.','NVARCHAR(MAX)')
		   ,1,1,''),
		  C.RecordStateID,
		  ISNULL(C.CatalogUpdatedDateTime,C.CatalogCreatedDateTime) AS CatalogLastUpdatedDateTime,		 
		  ISNULL(C.CatalogUpdatedUserName,C.CatalogCreatedUserName) AS CatalogLastUpdatedUserName,
		 COUNT(C.CatalogID) OVER() AS RecordsFiltered
	 FROM Product.[Catalog] C WITH(NOLOCK) 
	     INNER JOIN Product.CatalogType T WITH(NOLOCK) ON T.CatalogTypeID=T.CatalogTypeID
	     INNER JOIN Product.Category CA WITH(NOLOCK) ON C.CategoryID=CA.CategoryID AND C.CompanyID=CA.CompanyID
		 INNER JOIN (
		  SELECT CV.CompanyID,CV.CatalogID,RowNumber=ROW_NUMBER() OVER(PARTITION BY CV.CatalogVariantID ORDER BY CV.CatalogVariantID),
		   CV.CatalogVariantID,CV.CatalogVariantName,
		   P.PresentationName,CP.CatalogPresentationEquivalence,
		   U.UnitMeasureName		   
		  FROM Product.CatalogVariant CV 
		   INNER JOIN Product.CatalogPresentation CP WITH(NOLOCK) ON CV.CatalogVariantID=CP.CatalogVariantID AND CV.CompanyID=CP.CompanyID AND CP.CatalogPresentationIsDefault=1
		   INNER JOIN Product.Presentation P WITH(NOLOCK) ON CP.PresentationID=P.PresentationID AND CP.CompanyID=P.CompanyID
		   INNER JOIN Product.UnitMeasure U WITH(NOLOCK) ON P.UnitMeasureID=U.UnitMeasureID  
		 ) AS CV ON C.CatalogID=CV.CatalogID AND C.CompanyID=CV.CompanyID AND CV.RowNumber=1
		 LEFT JOIN Product.Brand B WITH(NOLOCK) ON C.BrandID=B.BrandID
		 LEFT JOIN Product.Manufacturer M WITH(NOLOCK) ON C.ManufacturerID=M.ManufacturerID
	  WHERE C.CompanyID=@CompanyID 
	       AND C.RecordStateID<>2 
		   AND (@CatalogTypeID IS NULL OR @CatalogTypeID=0 OR C.CatalogTypeID=@CatalogTypeID)
		   AND (@CatalogName IS NULL OR @CatalogName='' OR C.CatalogName LIKE '%' + @CatalogName + '%' )	 
	       AND (@RecordStateID IS NULL OR @RecordStateID=10 OR C.RecordStateID=@RecordStateID)
		   AND (@CategoryID IS NULL OR @CategoryID=0 OR C.CategoryID=@CategoryID)
		   AND (@ManufacturerID IS NULL OR @ManufacturerID=0 OR C.ManufacturerID=@ManufacturerID)
		   AND (@BrandID IS NULL OR @BrandID=0 OR C.BrandID=@BrandID)
	 ORDER BY C.CatalogID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF 
END