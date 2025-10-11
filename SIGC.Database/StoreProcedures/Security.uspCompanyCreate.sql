/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            11/10/2025
   Description:            Permite crear un registro en la tabla  [Security].Company
   Execute:
		/*  
		  DECLARE @CompanyID INT  
		  EXECUTE Security.uspCompanyCreate 
			@CompanyID=@CompanyID OUTPUT,
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
			@CompanyCreatedDateTime='2025-09-02 11:00'
			@CompanyCreatedUserID=1
					 
		  SELECT @CompanyID
		*/						   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE [Security].uspCompanyCreate
(  @CompanyID INT OUTPUT,   
   @CompanyTradeName VARCHAR(100),
   @CompanySocialReason VARCHAR(150),
   @CompanyDocumentNumber VARCHAR(11),
   @CompanyBirthDate DATE,
   @CountryID INT,
   @CompanyAddress VARCHAR(200),
   @TaxpayerTypeID SMALLINT,
   @SectorID SMALLINT,
   @CompanyCorporateEmail VARCHAR(150),
   @CompanyMobile VARCHAR(15),
   @CompanyPhone VARCHAR(15),
   @CompanyLogo VARCHAR(100),
   @StateID SMALLINT,
   @CompanyCreatedUserID INT,
   @CompanyCreatedDateTime DATETIME 
)
AS
BEGIN 
  INSERT INTO [Security].Company(CompanyTradeName, CompanySocialReason, CompanyDocumentNumber, CompanyBirthDate, CountryID, CompanyAddress, TaxpayerTypeID, SectorID, CompanyCorporateEmail, CompanyMobile, CompanyPhone, CompanyLogo, StateID, CompanyCreatedDateTime, CompanyCreatedUserID)
  VALUES(@CompanyTradeName, @CompanySocialReason, @CompanyDocumentNumber, @CompanyBirthDate, @CountryID, @CompanyAddress, @TaxpayerTypeID, @SectorID, @CompanyCorporateEmail, @CompanyMobile, @CompanyPhone, @CompanyLogo, @StateID, @CompanyCreatedDateTime, @CompanyCreatedUserID)
  SET @CompanyID = SCOPE_IDENTITY()
END