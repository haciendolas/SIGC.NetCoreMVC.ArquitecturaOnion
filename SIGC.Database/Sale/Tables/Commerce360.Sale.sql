GO
USE [Commerce360]
GO
CREATE SCHEMA Sale
GO
/*

Valor (ENUM)	Descripción clara
WebPForm	Registro creado desde el formulario web.
WebExcel	Registro cargado desde un archivo Excel en la web.
MobileForm	Registro ingresado desde formulario en app móvil.
MobileExcel	Registro cargado desde archivo Excel en la app móvil.
*/
CREATE TABLE Sale.Document(
	 DocumentID BIGINT NOT NULL IDENTITY(1,1),
	 CompanyID INT NOT NULL,
	 DocumentTypeID SMALLINT NOT NULL,
	 DocumentSerie VARCHAR(4) NOT NULL,
	 DocumentCorrelative VARCHAR(20) NOT NULL,
	 DocumentCode AS CONCAT(DocumentSerie,'-',DocumentCorrelative) PERSISTED,	
	 CustomerID INT NOT NULL,
	 CustomerFullName VARCHAR(150) NOT NULL,
	 CustomerMobilePhone VARCHAR(15),
	 CustomerAddress VARCHAR(150) NULL,
	 DocumentIssueDate DATE NOT NULL,	
	 DocumentIssueTime TIME NOT NULL,	
	 DocumentDueDate DATE NULL,
	 CurrencyTypeID TINYINT NOT NULL,
	 DocumentExchangeRate NUMERIC(4,2) NOT NULL,
	 DocumentTotalAmount NUMERIC(12,6) NOT NULL,
	 DocumentTotalEquivalent  AS (DocumentTotalAmount*DocumentExchangeRate) PERSISTED,	
	 RecordOriginID TINYINT NOT NULL,
	 DocumentGlosa VARCHAR(400),
	 DocumentStateID TINYINT NOT NULL,---PENDIENTE=1,Enviado=2,Entregado=3
	 RecordStateID TINYINT NOT NULL,	
     DocumentCreatedUserID INT NOT NULL,
     DocumentCreatedUserName VARCHAR(20) NOT NULL,
     DocumentCreatedUserFullName VARCHAR(80) NOT NULL,
	 DocumentCreatedDateTime DATETIME NOT NULL,   
     DocumentUpdatedUserID INT, 
     DocumentUpdatedUserName VARCHAR(20),
     DocumentUpdatedUserFullName VARCHAR(80),
	 DocumentUpdatedDateTime DATETIME,
	 CONSTRAINT Document_PK_DocumentID PRIMARY KEY(DocumentID),
	 CONSTRAINT Document_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO 
 CREATE TABLE Sale.DocumentStateHistory(
    DocumentStateHistoryID BIGINT NOT NULL IDENTITY(1,1),
	DocumentID BIGINT NOT NULL,
	DocumentStateID TINYINT NOT NULL,	
    DocumentStateHistoryCreatedUserID INT NOT NULL,
    DocumentStateHistoryCreatedUserName VARCHAR(20) NOT NULL,
    DocumentStateHistoryCreatedUserFullName VARCHAR(80) NOT NULL,
	DocumentStateHistoryCreatedDateTime DATETIME NOT NULL,
	CONSTRAINT DocumentStateHistory_PK_DocumentStateHistoryID PRIMARY KEY(DocumentStateHistoryID),
	CONSTRAINT DocumentStateHistory_FK_DocumentID FOREIGN KEY(DocumentID) REFERENCES Sale.Document(DocumentID)
 )
GO
CREATE TABLE Sale.DocumentItem(
	 DocumentItemID BIGINT NOT NULL IDENTITY(1,1),
	 DocumentID BIGINT NOT NULL,
	 DocumentItemRow SMALLINT NOT NULL,
	 CatalogID INT,
	 CatalogName VARCHAR(200) NOT NULL,
	 DocumentItemAdditionalInformation VARCHAR(300),
	 DocumentItemSalePrice NUMERIC(12,6) NOT NULL,
	 DocumentItemBasePrice NUMERIC(12,6) NOT NULL,
	 DocumentItemQuantity NUMERIC(12,6) NOT NULL,
	 DocumentItemWeight NUMERIC(10,3),
	 DocumentItemSubTotalAmount AS (DocumentItemBasePrice *  DocumentItemQuantity) PERSISTED,
	 DiscountTypeID SMALLINT,
	 DocumentItemDiscountValue NUMERIC(5,2),
	 DocumentItemDiscountAmount NUMERIC(12,6),
	 DocumentItemSubTotalNet NUMERIC(12,6) NOT NULL,
	 DocumentItemTaxRate NUMERIC(5,2) NOT NULL,
	 DocumentItemTaxAmount NUMERIC(12,6),
	 DocumentItemTotalAmount NUMERIC(12,6) NOT NULL,
	 CONSTRAINT DocumentItem_PK_DocumentItemID PRIMARY KEY(DocumentItemID),
	 CONSTRAINT DocumentItem_FK_DocumentID FOREIGN KEY(DocumentID) REFERENCES Sale.Document(DocumentID)
)