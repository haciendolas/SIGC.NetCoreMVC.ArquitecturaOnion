 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            24/10/2025
-- Description:            Permite listar departemento,provincia y distrito por continente y pais 
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec [Security].uspUbigeoListSearch  92,''
-- ============================================================================== 

ALTER PROC [Security].uspUbigeoListSearch(
  @UbigeoClassContinent INT, 
  @UbigeoName VARCHAR(100) = '' 
)
AS
BEGIN
	SET NOCOUNT ON;
	   SELECT TOP 500 Dep.UbigeoName AS DepartmentName,Prov.UbigeoName AS ProvinceName,
	   Dist.UbigeoID AS DistrictID,Dist.UbigeoCode AS DistrictCode,Dist.UbigeoName AS DistrictName
	   FROM [Security].Ubigeo Dep WITH(NOLOCK)
	   INNER JOIN [Security].Ubigeo Prov WITH(NOLOCK) ON Dep.UbigeoCode = SUBSTRING(Prov.UbigeoCode,1,len(Prov.UbigeoCode)-2)  AND len(Prov.UbigeoCode) = 4
	   INNER JOIN [Security].Ubigeo Dist WITH(NOLOCK) ON Prov.UbigeoCode = SUBSTRING(Dist.UbigeoCode,1,len(Dist.UbigeoCode)-2) AND len(Dist.UbigeoCode) = 6
	   WHERE Dep.UbigeoClass=@UbigeoClassContinent AND len(Dep.UbigeoCode)=2
	   AND (Dep.UbigeoName LIKE @UbigeoName+'%' OR 
	        Prov.UbigeoName LIKE @UbigeoName+'%' OR 
	        Dist.UbigeoName LIKE @UbigeoName+'%'
	   )
	SET NOCOUNT OFF;
END