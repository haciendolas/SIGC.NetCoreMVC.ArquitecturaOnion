 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            06/09/2026
-- Description:            Permite listar variantes por catalog de la tabla Product.CatalogVariant
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogVariantList @CompanyID=1,@CatalogID=1
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogVariantList(
 @CompanyID INT,
 @CatalogID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT CV.CatalogVariantID,CV.CatalogVariantName,CV.CatalogVariantSKU,CV.RecordStateID AS CatalogVariantStateID,
		 'CatalogVariantValues'=  
	      '[' + ISNULL(STUFF((SELECT ','  + '{'+ 
								 '"AttributeValueID":' + CONVERT(VARCHAR(10), CVA.AttributeValueID)+','+
								 '"AttributeName":"' +ISNULL(A.AttributeName,'')+'",'+ 
								 '"AttributeValueName":"' +ISNULL(AV.AttributeValueName,'') +'"'+
							 '}'  
							 FROM Product.CatalogVariantValue CVA WITH(NOLOCK) 
							 INNER JOIN Product.AttributeValue AV WITH(NOLOCK) ON CVA.AttributeValueID=AV.AttributeValueID AND AV.RecordStateID<>2
							 INNER JOIN Product.Attribute A WITH(NOLOCK) ON A.AttributeID = AV.AttributeID AND A.RecordStateID<>2									 						 
							 WHERE CV.CatalogVariantID=CVA.CatalogVariantID AND CVA.RecordStateID<>2 
							 FOR XML PATH(''), TYPE
					    )
						.value(N'.[1]', N'varchar(max)'),1,1,''
					)
				,'')
		+']',
		UM.UnitMeasureID,UM.UnitMeasureName,
		CP.CatalogPresentationID,CP.PresentationID,P.PresentationName,CP.CatalogPresentationIsDefault,
		CP.CatalogPresentationEquivalence,CP.CatalogPresentationSKU,CP.CatalogPresentationBarcode,
		CP.RecordStateID AS CatalogPresentationStateID
	    FROM Product.CatalogVariant CV WITH(NOLOCK) 
		INNER JOIN Product.CatalogPresentation CP WITH(NOLOCK) ON CV.CatalogVariantID=CP.CatalogVariantID AND CV.CompanyID=CP.CompanyID	AND CP.RecordStateID<>2	
		INNER JOIN Product.Presentation P WITH(NOLOCK) ON CP.PresentationID=P.PresentationID AND CP.CompanyID=P.CompanyID AND CP.RecordStateID<>2
		INNER JOIN Product.UnitMeasure UM WITH(NOLOCK) ON P.UnitMeasureID = UM.UnitMeasureID AND UM.RecordStateID<>2
		WHERE P.CompanyID=@CompanyID 
		AND CV.CatalogID=@CatalogID	
		AND CV.RecordStateID<>2		
	SET NOCOUNT OFF
END