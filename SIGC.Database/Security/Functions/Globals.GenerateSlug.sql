ALTER FUNCTION Globals.GenerateSlug(
    @text NVARCHAR(200)
) 
RETURNS NVARCHAR(200)
AS
BEGIN
    DECLARE @slug NVARCHAR(200);
    DECLARE @originalSlug NVARCHAR(200);
    DECLARE @counter INT = 1;

    -- 1. Convertir a minúsculas
    SET @slug = LOWER(@text);

    -- 2. Reemplazar acentos
    SET @slug = REPLACE(@slug, N'á','a');
    SET @slug = REPLACE(@slug, N'é','e');
    SET @slug = REPLACE(@slug, N'í','i');
    SET @slug = REPLACE(@slug, N'ó','o');
    SET @slug = REPLACE(@slug, N'ú','u');
    SET @slug = REPLACE(@slug, N'ñ','n');

    -- 3. Reemplazar espacios por guiones
    SET @slug = REPLACE(@slug, ' ', '-');

    -- 4. Eliminar cualquier carácter que no sea letra, número o guión
    DECLARE @i INT = 1;
    WHILE @i <= LEN(@slug)
    BEGIN
        IF SUBSTRING(@slug, @i, 1) NOT LIKE '[a-z0-9-]'
            SET @slug = STUFF(@slug, @i, 1, '');
        ELSE
            SET @i = @i + 1;
    END

    -- 5. Eliminar guiones duplicados
    WHILE CHARINDEX('--', @slug) > 0
        SET @slug = REPLACE(@slug, '--', '-');

    -- 6. Quitar guiones al inicio y al final
    WHILE LEFT(@slug, 1) = '-' SET @slug = SUBSTRING(@slug, 2, LEN(@slug));
    WHILE RIGHT(@slug, 1) = '-' SET @slug = SUBSTRING(@slug, 1, LEN(@slug)-1);

    -- Guardar versión original
    SET @originalSlug = @slug;

    -- 7. Asegurar que sea único
    WHILE EXISTS(SELECT 1 FROM Product.Catalog WHERE CatalogSlug = @slug)
    BEGIN
        SET @slug = @originalSlug + '-' + CAST(@counter AS NVARCHAR(10));
        SET @counter = @counter + 1;
    END

    RETURN @slug;
END
