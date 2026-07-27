 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            25/07/2026
-- Description:            Permite listar atributos con su valor , solo activos de la tabla Product.Attribute y Product.AttributeValue
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspAttributeValueList @AttributeIsVariant=1
-- ============================================================================== 
ALTER PROCEDURE Product.uspAttributeValueList(
   @AttributeIsVariant BIT
)
AS
BEGIN
  SET NOCOUNT ON
	 SELECT A.AttributeID,A.AttributeName,A.AttributeIsVariant,AV.AttributeValueID,AV.AttributeValueName
	 FROM Product.AttributeValue AV WITH(NOLOCK)
	 INNER JOIN Product.Attribute A WITH(NOLOCK) ON AV.AttributeID=A.AttributeID
	 WHERE AV.RecordStateID=1 AND A.RecordStateID=1
	  AND (@AttributeIsVariant IS NULL OR A.AttributeIsVariant = @AttributeIsVariant)
	   
   SET NOCOUNT OFF
END