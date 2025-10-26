CREATE SCHEMA Globals
GO
 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            22/10/2025
-- Description:            Convertir un cadena de texto con caracter de separación a un listado
-- Update:				   Joel Castillo Rojas    
-- Exec                    SELECT * from Globals.fnConvertToTable('joel,jose',',')
-- ============================================================================== 

ALTER FUNCTION Globals.fnConvertToTable(@list nvarchar(MAX),@Separador as char(1))
   RETURNS @tbl TABLE (Item varchar(20) NOT NULL) AS
BEGIN
   DECLARE @pos        int,
           @nextpos    int,
           @valuelen   int

   SELECT @pos = 0, @nextpos = 1

   WHILE @nextpos > 0
   BEGIN
      SELECT @nextpos = charindex(@Separador, @list, @pos + 1)
      SELECT @valuelen = CASE WHEN @nextpos > 0
                              THEN @nextpos
                              ELSE len(@list) + 1
                         END - @pos - 1
      INSERT @tbl (Item)
         VALUES ( substring(@list, @pos + 1, @valuelen))
      
      SELECT @pos = @nextpos
   END
  RETURN
END