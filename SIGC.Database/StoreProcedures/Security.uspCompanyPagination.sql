 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            11/10/2025
-- Description:            Permite obtener listado paginado de Compañias por filtros tabla [Security].Company
-- Update:				   Joel Castillo Rojas    
-- Exec                    
        /*
		DECLARE @RecordsTotal INT
		Exec [Security].uspCompanyPagination 
		@CompanyIDRegister = 1,
		@TaxpayerTypeID = NULL,
		@RubroID = NULL,
		@CompanyDocumentNumber = NULL,
		@CompanySocialReason = NULL,
		@StateID=1,
		@PageNumber=1,
		@PageSize=10,
		@RecordsTotal=@RecordsTotal OUTPUT

		SELECT  @RecordsTotal AS 'RecordsTotal'
		*/
-- ============================================================================== 
ALTER PROCEDURE [Security].uspCompanyPagination(
   @CompanyIDRegister INT,
   @TaxpayerTypeID SMALLINT,
   @RubroID SMALLINT,
   @CompanyDocumentNumber VARCHAR(11),
   @CompanySocialReason VARCHAR(150), 
   @StateID SMALLINT=1,
   @PageNumber INT=1,
   @PageSize INT=10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON

    SET @RecordsTotal = (SELECT COUNT(C.CompanyID) FROM [Security].Company C WITH(NOLOCK)
						 INNER JOIN [Security].CompanyRegister CR WITH(NOLOCK) ON C.CompanyID = CR.CompanyID
						 WHERE CR.CompanyIDRegister=@CompanyIDRegister AND C.StateID<>2
	                     )

    SELECT C.CompanyID,TaxpayerType.ConstantName AS TaxpayerTypeName,C.CompanyDocumentNumber,C.CompanySocialReason,Rubro.ConstantName AS RubroName,
	     C.StateID,Country.UbigeoName AS CountryName,	 
		 ISNULL(C.CompanyUpdatedDateTime,C.CompanyCreatedDateTime) AS CompanyLastUpdatedDateTime,
		 U.UserName AS CompanyLastUpdatedUserName,
		 COUNT(*) OVER() AS RecordsFiltered
	 FROM [Security].Company C WITH(NOLOCK) 
	  INNER JOIN [Security].CompanyRegister CR WITH(NOLOCK) ON C.CompanyID = CR.CompanyID
	  INNER JOIN [Security].[User] U WITH(NOLOCK) ON ISNULL(C.CompanyUpdatedUserID,C.CompanyCreatedUserID)=U.UserID
	  INNER JOIN [Security].Ubigeo Country WITH(NOLOCK) ON C.CountryID = Country.UbigeoID
	  INNER JOIN [Security].Constant TaxpayerType WITH(NOLOCK) ON C.TaxpayerTypeID=TaxpayerType.ConstantID AND TaxpayerType.ConstantClass=1030
	  INNER JOIN [Security].Constant Rubro WITH(NOLOCK) ON C.RubroID=Rubro.ConstantID AND Rubro.ConstantClass=1034
	 WHERE CR.CompanyIDRegister=@CompanyIDRegister AND C.StateID!=2 
	   AND C.TaxpayerTypeID = CASE WHEN @TaxpayerTypeID IS NULL OR @TaxpayerTypeID=0 THEN C.TaxpayerTypeID ELSE @TaxpayerTypeID END 
	   AND C.RubroID=CASE WHEN @RubroID IS NULL OR @RubroID=0 THEN C.RubroID ELSE @RubroID END   
	   AND C.CompanyDocumentNumber LIKE '%'+ ISNULL(@CompanyDocumentNumber,'') + '%'
	   AND C.CompanySocialReason LIKE '%' + ISNULL(@CompanySocialReason,'') + '%'
	   AND C.StateID = CASE WHEN @StateID=10 THEN C.StateID ELSE @StateID END 
	 ORDER BY C.CompanyID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF 
END