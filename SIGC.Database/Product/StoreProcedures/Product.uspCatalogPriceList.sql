 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            03/10/2026
-- Description:            Permite listar precios por catalog de la tabla Product.CatalogPrice
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogPriceList @CompanyID=1,@CatalogID=42
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogPriceList(
 @CompanyID INT,
 @CatalogID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT CP.EstablishmentID, E.EstablishmentName,CP.CatalogPriceID,CP.CatalogPresentationID,P.PresentationName AS CatalogPresentationName,
		CP.PriceTypeID,PT.PriceTypeName,CP.CurrencyTypeID,CT.ConstantName AS CurrencyTypeName,
		CP.CatalogPriceAmount,CP.CatalogPriceIsTaxIncluded,CP.RecordStateID
	    FROM Product.CatalogPrice CP  WITH(NOLOCK) 
		INNER JOIN Product.PriceType PT WITH(NOLOCK) ON CP.PriceTypeID=PT.PriceTypeID AND PT.RecordStateID<>2
		INNER JOIN [Security].Constant CT WITH(NOLOCK) ON CP.CurrencyTypeID=CT.ConstantID AND CT.ConstantClass=1040 AND CT.StateID<>2
		INNER JOIN Organization.Establishment E WITH(NOLOCK) ON CP.EstablishmentID=E.EstablishmentID AND CP.CompanyID=E.CompanyID AND E.RecordStateID<>2
		INNER JOIN Product.CatalogPresentation CPres WITH(NOLOCK) ON CP.CatalogPresentationID = CPres.CatalogPresentationID AND CP.CompanyID=CPres.CompanyID AND CPres.RecordStateID<>2
		INNER JOIN Product.Presentation P WITH(NOLOCK) ON CPres.PresentationID=P.PresentationID AND CPres.CompanyID=P.CompanyID AND P.RecordStateID<>2
		INNER JOIN Product.CatalogVariant CV WITH(NOLOCK) ON CPres.CatalogVariantID=CV.CatalogVariantID AND CPres.CompanyID=CV.CompanyID AND CV.RecordStateID<>2
		WHERE CP.CompanyID=@CompanyID 
		AND CV.CatalogID=@CatalogID
		AND CP.RecordStateID<>2		 
	SET NOCOUNT OFF
END