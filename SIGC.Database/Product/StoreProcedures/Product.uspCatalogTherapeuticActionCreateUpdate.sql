/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            21/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogTherapeuticAction
   Execute:
		  
		  EXECUTE Product.uspCatalogTherapeuticActionCreateUpdate 		 
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
CREATE PROCEDURE Product.uspCatalogTherapeuticActionCreateUpdate
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
  IF EXISTS(SELECT 1 FROM Product.CatalogTherapeuticAction CTA WHERE CTA.CompanyID = @CompanyID 
                                   AND CTA.CatalogID = @CatalogID 
								   AND CTA.TherapeuticActionID = @TherapeuticActionID)

    UPDATE Product.CatalogTherapeuticAction SET	 
	   RecordStateID = @RecordStateID,
	   CatalogTherapeuticActionUpdatedUserID = @CatalogTherapeuticActionCreatedUserID,
	   CatalogTherapeuticActionUpdatedUserName = @CatalogTherapeuticActionCreatedUserName,
	   CatalogTherapeuticActionUpdatedUserFullName = @CatalogTherapeuticActionCreatedUserFullName,
	   CatalogTherapeuticActionUpdatedDateTime = @CatalogTherapeuticActionCreatedDateTime
	   WHERE CompanyID = @CompanyID AND
              CatalogID = @CatalogID AND
			  TherapeuticActionID = @TherapeuticActionID
  ELSE 
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