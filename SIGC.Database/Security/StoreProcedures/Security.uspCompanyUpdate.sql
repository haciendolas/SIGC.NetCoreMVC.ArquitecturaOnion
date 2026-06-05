/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            11/10/2025
   Description:            Permite actualizar un registro en la tabla [Security].Company
   Execute:
	 	 
		  EXECUTE Security.uspCompanyCreate 
			@CompanyID=1,
			@CompanyTradeName='Empresa 1',
			@CompanySocialReason='Empresa 1',
			@CompanyDocumentNumber='12345678909',
			@CompanyBirthDate='20250110',
			@CountryID=1,
			@CompanyAddress='AV MIRAFLORES - LIMA 22'
			@TaxpayerTypeID=1,
			@SectorID =1,
			@CompanyCorporateEmail ='empresa@net.com'
			@CompanyMobile= NULL,
			@CompanyPhone = NULL,
			@CompanyLogo= NULL
			@StateID = 1
			@CompanyUpdatedDateTime='2025-09-02 11:00'
			@CompanyUpdatedUserID=1  	 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE [Security].uspCompanyUpdate
(  @CompanyID INT,   
   @CompanyTradeName VARCHAR(100),
   @CompanySocialReason VARCHAR(150),
   @CompanyDocumentNumber VARCHAR(11),
   @CompanyBirthDate DATE,
   @CountryID INT,
   @CompanyAddress VARCHAR(200),
   @TaxpayerTypeID SMALLINT,
   @RubroID SMALLINT,
   @CompanyCorporateEmail VARCHAR(150),
   @CompanyMobile VARCHAR(15),
   @CompanyPhone VARCHAR(15),
   @CompanyLogo VARCHAR(100),
   @StateID SMALLINT,
   @CompanyUpdatedUserID INT,
   @CompanyUpdatedDateTime DATETIME 
)
AS
BEGIN 
  UPDATE [Security].Company 
     SET CompanyTradeName = @CompanyTradeName,
          CompanySocialReason = @CompanySocialReason,
		  CompanyDocumentNumber = @CompanyDocumentNumber, 
		  CompanyBirthDate = @CompanyBirthDate, 
		  CountryID = @CountryID, 
		  CompanyAddress=@CompanyAddress,
		  TaxpayerTypeID=@TaxpayerTypeID,
		  RubroID = @RubroID, 
		  CompanyCorporateEmail = @CompanyCorporateEmail,
		  CompanyMobile = @CompanyMobile,
		  CompanyPhone = @CompanyPhone,
		  CompanyLogo = @CompanyLogo, 
		  StateID = @StateID,
		  CompanyUpdatedDateTime = @CompanyUpdatedDateTime,
		  CompanyUpdatedUserID = @CompanyUpdatedUserID
  WHERE CompanyID=@CompanyID
END