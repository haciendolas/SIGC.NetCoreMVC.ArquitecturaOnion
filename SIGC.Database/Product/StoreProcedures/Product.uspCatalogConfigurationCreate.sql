/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogConfiguration
   Execute:	  

		  DECLARE @CatalogConfigurationID INT  
		  EXECUTE Product.uspCatalogConfigurationCreate 
			@CatalogConfigurationID=@CatalogConfigurationID OUTPUT,
			@CompanyID=1,
		    @EstablishmentID=1,
			@CatalogID=1,		
			@CatalogConfigurationIsStockManaged=1,
			@CatalogConfigurationIsAffectStock=0,			 
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogConfigurationCreatedUserID=1,
			@CatalogConfigurationCreatedUserName='administrador',
			@CatalogConfigurationCreatedUserFullName='Joel Castillo Rojas',
			@CatalogConfigurationCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogConfigurationID					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogConfigurationCreate
(  @CatalogConfigurationID INT OUTPUT,
   @CompanyID INT,
   @EstablishmentID INT, 
   @CatalogID INT,  
   @CatalogConfigurationIsStockManaged BIT,
   @CatalogConfigurationIsAffectStock BIT,  
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogConfigurationCreatedUserID INT,
   @CatalogConfigurationCreatedUserName NVARCHAR(20),
   @CatalogConfigurationCreatedUserFullName NVARCHAR(80),
   @CatalogConfigurationCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogConfiguration(
     CompanyID,
	 EstablishmentID,
	 CatalogID,
	 CatalogConfigurationIsStockManaged,
	 CatalogConfigurationIsAffectStock,  
     RecordOriginID,
	 RecordStateID,
	 CatalogConfigurationCreatedUserID,
	 CatalogConfigurationCreatedUserName,
	 CatalogConfigurationCreatedUserFullName,
	 CatalogConfigurationCreatedDateTime)
  VALUES(
    @CompanyID,
	@EstablishmentID,
    @CatalogID,
	@CatalogConfigurationIsStockManaged,
	@CatalogConfigurationIsAffectStock,  
    @RecordOriginID,
	@RecordStateID,
	@CatalogConfigurationCreatedUserID,
	@CatalogConfigurationCreatedUserName,
	@CatalogConfigurationCreatedUserFullName,
	@CatalogConfigurationCreatedDateTime
  )

 SET @CatalogConfigurationID = SCOPE_IDENTITY()
END