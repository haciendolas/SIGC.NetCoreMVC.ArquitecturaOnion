/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogTherapeuticAction
   Execute:
		  
		  EXECUTE Product.uspCatalogTherapeuticActionCreate 		 
			@CompanyID=1,
			@CatalogID=1,
			@TherapeuticActionID=1,			 
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogTherapeuticActionCreatedUserID=1,
			@CatalogTherapeuticActionCreatedUserName='administrador',
			@CatalogTherapeuticActionCreatedUserFullName='Joel Castillo Rojas',
			@CatalogTherapeuticActionCreatedDateTime='2025-09-02 11:00'		 		 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogTherapeuticActionCreate
(  @CompanyID INT,
   @CatalogID INT,
   @TherapeuticActionID SMALLINT,
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogTherapeuticActionCreatedUserID INT,
   @CatalogTherapeuticActionCreatedUserName NVARCHAR(20),
   @CatalogTherapeuticActionCreatedUserFullName NVARCHAR(80),
   @CatalogTherapeuticActionCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogTherapeuticAction(
     CompanyID,
	 CatalogID,
	 TherapeuticActionID,	 
     RecordOriginID,
	 RecordStateID,
	 CatalogTherapeuticActionCreatedUserID,
	 CatalogTherapeuticActionCreatedUserName,
	 CatalogTherapeuticActionCreatedUserFullName,
	 CatalogTherapeuticActionCreatedDateTime)
  VALUES(
    @CompanyID,
	@CatalogID,
	@TherapeuticActionID,
    @RecordOriginID,
	@RecordStateID,
	@CatalogTherapeuticActionCreatedUserID,
	@CatalogTherapeuticActionCreatedUserName,
	@CatalogTherapeuticActionCreatedUserFullName,
	@CatalogTherapeuticActionCreatedDateTime
  ) 
END