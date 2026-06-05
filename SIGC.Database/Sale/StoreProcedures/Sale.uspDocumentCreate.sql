/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            27/11/2025
   Description:            Permite crear un registro en la tabla Sale.Document
   Execute:
	 
		  DECLARE @DocumentID BIGINT
		  EXECUTE Sale.uspDocumentCreate 
			@DocumentID=@DocumentID OUTPUT,		   
			@CompanyID = 1,
			@DocumentTypeID=1,	
			@DocumentSerie='PON1',	
			@DocumentCorrelative='00000001',		 
			@CustomerID = 1,
			@CustomerFullName='JOEL CASTILLO ROJAS',
			@CustomerMobilePhone='99999999',
			@CustumerAddress='AV LOS INCAS',
			@DocumentIssueDate='20251204',
			@DocumentIssueTime='21:52:00',
			@DocumentDueDate=NULL,
			@CurrencyTypeID=1,
			@DocumentExchangeRate=1.0,
			@DocumentTotalAmount=100,
			@DocumentGlosa='MI GLOSA',
			@RecordStateID=1,
			@RecordOriginID = 1

		  SELECT @DocumentID	 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Sale.uspDocumentCreate
(  @DocumentID BIGINT OUTPUT,
   @CompanyID INT,
   @DocumentTypeID SMALLINT,
   @DocumentSerie VARCHAR(4),   
   @DocumentCorrelative VARCHAR(20), 
   @CustomerID INT,
   @CustomerFullName VARCHAR(150),
   @CustomerMobilePhone VARCHAR(15),
   @CustomerAddress VARCHAR(150),
   @DocumentIssueDate DATE,
   @DocumentIssueTime TIME(7),
   @DocumentDueDate DATE,
   @CurrencyTypeID TINYINT,
   @DocumentExchangeRate NUMERIC(4,2),
   @DocumentTotalAmount NUMERIC(12,6),
   @DocumentGlosa VARCHAR(400), 
   @RecordStateID TINYINT,
   @RecordOriginID TINYINT = 1, -- WebPForm
   @DocumentStateID TINYINT,
   @DocumentCreatedUserID INT ,
   @DocumentCreatedUserName VARCHAR(20) ,
   @DocumentCreatedUserFullName VARCHAR(80),
   @DocumentCreatedDateTime DATETIME   
)
AS
BEGIN
  IF @DocumentSerie = 'PON1' --Se usa para pedido online
    BEGIN	 
	  DECLARE @DocumentCorrelativeMax INT = (SELECT CAST(ISNULL(MAX(D.DocumentCorrelative),0) AS BIGINT)+1 FROM Sale.Document D WHERE D.DocumentSerie=@DocumentSerie)  
      SET @DocumentCorrelative = FORMAT(@DocumentCorrelativeMax,'00000000000000000000')	 
	END 

  INSERT INTO Sale.Document(CompanyID,DocumentTypeID,DocumentSerie,DocumentCorrelative,CustomerID,CustomerFullName,
     CustomerMobilePhone,CustomerAddress,DocumentIssueDate,DocumentIssueTime,DocumentDueDate,CurrencyTypeID,
	 DocumentExchangeRate,DocumentTotalAmount,RecordOriginID,DocumentGlosa,DocumentStateID,RecordStateID,
	 DocumentCreatedUserID,DocumentCreatedUserName,DocumentCreatedUserFullName,DocumentCreatedDateTime
	 )
  VALUES(@CompanyID,@DocumentTypeID,@DocumentSerie,@DocumentCorrelative,@CustomerID,@CustomerFullName,
    @CustomerMobilePhone, @CustomerAddress,@DocumentIssueDate,@DocumentIssueTime,@DocumentDueDate,@CurrencyTypeID,
	@DocumentExchangeRate,@DocumentTotalAmount,@RecordOriginID,@DocumentGlosa,@DocumentStateID,@RecordStateID,
	@DocumentCreatedUserID,@DocumentCreatedUserName,@DocumentCreatedUserFullName,@DocumentCreatedDateTime)
	  
  SET @DocumentID = SCOPE_IDENTITY()

END