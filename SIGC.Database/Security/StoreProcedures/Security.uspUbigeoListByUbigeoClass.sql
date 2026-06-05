-- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            24/10/2025
-- Description:            Permite obtener listado de ubigeo activos de la tabla [Security].Ubigeo
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspUbigeoListByUbigeoClass @UbigeoClass=91
-- ============================================================================== 

ALTER PROC [Security].uspUbigeoListByUbigeoClass
(  @UbigeoClass INT 
)
AS
BEGIN	 
	SET NOCOUNT ON
	   SELECT U.UbigeoID,U.UbigeoCode,U.UbigeoName FROM [Security].Ubigeo U WITH(NOLOCK) WHERE UbigeoClass=@UbigeoClass 
	   AND U.StateID=1 
	SET NOCOUNT OFF
END