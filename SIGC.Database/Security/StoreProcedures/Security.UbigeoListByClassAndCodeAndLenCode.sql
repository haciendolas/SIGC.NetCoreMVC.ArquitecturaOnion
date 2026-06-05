-- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            24/10/2025
-- Description:            Permite obtener listado de ubigeo activos de la tabla [Security].Ubigeo
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec [Security].uspUbigeoListByClassAndCodeAndLenCode @UbigeoClass=92,@UbigeoCode='02',@LenUbigeoCode=4
-- ============================================================================== 

CREATE PROC [Security].uspUbigeoListByClassAndCodeAndLenCode
(  @UbigeoClass INT ,
   @UbigeoCode VARCHAR(25),
   @LenUbigeoCode INT
)
AS
BEGIN	 
	SET NOCOUNT ON
	   SELECT U.UbigeoID, U.UbigeoCode, U.UbigeoName FROM [Security].Ubigeo U WITH(NOLOCK) 
	   WHERE U.UbigeoClass=@UbigeoClass
	     AND U.UbigeoCode LIKE @UbigeoCode+'%'
	     AND LEN(U.UbigeoCode)= @LenUbigeoCode
		 AND U.StateID = 1
	SET NOCOUNT OFF
END