 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            11/10/2025
-- Description:            Permite obtener una compañia y sus pagina asignada por @CompanyID  de la tabla [Security].Company
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec [Security].uspCompanyGet  @CompanyID=1
-- ============================================================================== 
ALTER PROCEDURE [Security].uspCompanyGet(
   @CompanyID INT 
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT C.CompanyID,C.CompanyTradeName,C.CompanySocialReason,C.CompanyDocumentNumber,
		C.CompanyBirthDate,C.CountryID,C.CompanyAddress,C.TaxpayerTypeID,C.RubroID,
		C.CompanyCorporateEmail,C.CompanyMobile,C.CompanyPhone,C.CompanyLogo,C.StateID,
		'PageCompany'=  
			  '[' + ISNULL(STUFF((SELECT ','  + '{'+ 			                         
									 '"PageID":' + CONVERT(VARCHAR(10), PC.PageID)+''+
								 '}'  
								 FROM [Security].PageCompany PC							 						 
								 WHERE PC.CompanyID=C.CompanyID	 AND PC.StateID=1			      
								 FOR XML PATH(''), TYPE
							)
							.value(N'.[1]', N'varchar(max)'),1,1,''
						)
					,'')
			+']'
		FROM [Security].Company C		 
		WHERE C.CompanyID=@CompanyID 
	SET NOCOUNT OFF
END