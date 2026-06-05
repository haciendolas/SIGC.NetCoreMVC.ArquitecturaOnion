USE master
GO
USE [Commerce360]
GO
CREATE SCHEMA Product
GO
CREATE TABLE Product.Category(
   CategoryID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,
   CategoryName VARCHAR(100) NOT NULL,
   CategorySlug VARCHAR(100) NOT NULL,
   CategoryImage VARCHAR(100),   
   RecordOriginID TINYINT NOT NULL, --WebPForm=1
   RecordStateID TINYINT NOT NULL,
   CategoryCreatedUserID INT NOT NULL,
   CategoryCreatedUserName VARCHAR(20) NOT NULL,
   CategoryCreatedUserFullName VARCHAR(80) NOT NULL,
   CategoryCreatedDateTime DATETIME NOT NULL,  
   CategoryUpdatedUserID INT, 
   CategoryUpdatedUserName VARCHAR(20),
   CategoryUpdatedUserFullName VARCHAR(80),
   CategoryUpdatedDateTime DATETIME,
   CONSTRAINT Category_PK_CategoryID PRIMARY KEY(CategoryID),
   CONSTRAINT Category_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO 
ALTER TABLE Product.Category ADD CompanyID INT
ALTER TABLE Product.Category ADD RecordOriginID TINYINT
ALTER TABLE Product.Category ADD RecordStateID TINYINT
ALTER TABLE Product.Category ADD CategoryCreatedUserID INT
ALTER TABLE Product.Category ADD CategoryCreatedUserName VARCHAR(20)
ALTER TABLE Product.Category ADD CategoryCreatedUserFullName VARCHAR(80)
ALTER TABLE Product.Category ADD CategoryCreatedDateTime DATETIME
ALTER TABLE Product.Category ADD CategoryUpdatedUserID INT
ALTER TABLE Product.Category ADD CategoryUpdatedUserName VARCHAR(20)
ALTER TABLE Product.Category ADD CategoryUpdatedUserFullName VARCHAR(80)
ALTER TABLE Product.Category ADD CategoryUpdatedDateTime DATETIME
ALTER TABLE Product.Category ADD CONSTRAINT Category_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))

UPDATE Product.Category 
    SET RecordOriginID=1,
       RecordStateID=1,
	   CategoryCreatedUserID=1,
	   CategoryCreatedUserName ='administrador',
	   CategoryCreatedUserFullName='Joel Castillo',
	   CategoryCreatedDateTime=getdate()

GO
CREATE TABLE Product.UnitMeasure(
  UnitMeasureID INT NOT NULL IDENTITY(1,1),
  CountryID INT NOT NULL,
  UnitMeasureCode VARCHAR(10) NOT NULL,
  UnitMeasureName VARCHAR(20) NOT NULL,
  UnitMeasureFactorConversion NUMERIC(10,6),
  RecordOriginID TINYINT NOT NULL,
  RecordStateID TINYINT NOT NULL,
  UnitMeasureCreatedUserID INT NOT NULL,
  UnitMeasureCreatedUserName VARCHAR(20) NOT NULL,
  UnitMeasureCreatedUserFullName VARCHAR(80) NOT NULL,
  UnitMeasureCreatedDateTime DATETIME NOT NULL,  
  UnitMeasureUpdatedUserID INT, 
  UnitMeasureUpdatedUserName VARCHAR(20),
  UnitMeasureUpdatedUserFullName VARCHAR(80),
  UnitMeasureUpdatedDateTime DATETIME,
  CONSTRAINT UnitMeasure_PK_UnitMeasureID PRIMARY KEY(UnitMeasureID),
  CONSTRAINT UnitMeasure_UQ_CountryIDAndUnitMeasureCode UNIQUE (CountryID, UnitMeasureCode),
  CONSTRAINT UnitMeasure_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
ALTER TABLE Product.UnitMeasure ADD CountryID INT
ALTER TABLE Product.UnitMeasure ADD UnitMeasureFactorConversion NUMERIC(10,6)
ALTER TABLE Product.UnitMeasure ADD RecordOriginID TINYINT
ALTER TABLE Product.UnitMeasure ADD RecordStateID TINYINT
ALTER TABLE Product.UnitMeasure ADD UnitMeasureCreatedUserID INT
ALTER TABLE Product.UnitMeasure ADD UnitMeasureCreatedUserName VARCHAR(20)
ALTER TABLE Product.UnitMeasure ADD UnitMeasureCreatedUserFullName VARCHAR(80)
ALTER TABLE Product.UnitMeasure ADD UnitMeasureCreatedDateTime DATETIME
ALTER TABLE Product.UnitMeasure ADD UnitMeasureUpdatedUserID INT
ALTER TABLE Product.UnitMeasure ADD UnitMeasureUpdatedUserName VARCHAR(20)
ALTER TABLE Product.UnitMeasure ADD UnitMeasureUpdatedUserFullName VARCHAR(80)
ALTER TABLE Product.UnitMeasure ADD UnitMeasureUpdatedDateTime DATETIME
ALTER TABLE Product.UnitMeasure ADD CONSTRAINT UnitMeasure_UQ_CountryIDAndUnitMeasureCode UNIQUE (CountryID, UnitMeasureCode)
ALTER TABLE Product.UnitMeasure ADD CONSTRAINT UnitMeasure_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
GO

UPDATE Product.UnitMeasure 
    SET RecordOriginID=1,
       RecordStateID=1,
	   UnitMeasureCreatedUserID=1,
	   UnitMeasureCreatedUserName ='administrador',
	   UnitMeasureCreatedUserFullName='Joel Castillo',
	   UnitMeasureCreatedDateTime=getdate(),
	   CountryID = 38

CREATE TABLE Product.CatalogType(
   CatalogTypeID TINYINT NOT NULL, --1=PRODUCTO,2=SERVICIO,3=CONCEPTO,4=ACTIVO FIJO
   CatalogTypeName VARCHAR(20) NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CONSTRAINT CatalogType_PK_CatalogTypeID PRIMARY KEY(CatalogTypeID),
   CONSTRAINT CatalogType_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)) 
)
GO
--Principo Activo
CREATE TABLE Product.ActiveIngredient( 
   ActiveIngredientID INT NOT NULL IDENTITY(1,1),
   ActiveIngredientName VARCHAR(30) NOT NULL,
   ActiveIngredienDescription VARCHAR(150),
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   ActiveIngredientCreatedUserID INT NOT NULL,
   ActiveIngredientCreatedUserName VARCHAR(20) NOT NULL,
   ActiveIngredientCreatedUserFullName VARCHAR(80) NOT NULL,
   ActiveIngredientCreatedDateTime DATETIME NOT NULL,  
   ActiveIngredientUpdatedUserID INT, 
   ActiveIngredientUpdatedUserName VARCHAR(20),
   ActiveIngredientUpdatedUserFullName VARCHAR(80),
   ActiveIngredientUpdatedDateTime DATETIME,
   CONSTRAINT ActiveIngredient_PK_ActiveIngredientID PRIMARY KEY(ActiveIngredientID),
   CONSTRAINT ActiveIngredient_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)) 
)
GO
--forma farmacéutica
CREATE TABLE Product.PharmaceuticalForm( 
   PharmaceuticalFormID SMALLINT NOT NULL IDENTITY(1,1),
   PharmaceuticalFormName VARCHAR(50) NOT NULL,
   PharmaceuticalFormDescription VARCHAR(150),
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   PharmaceuticalFormCreatedUserID INT NOT NULL,
   PharmaceuticalFormCreatedUserName VARCHAR(20) NOT NULL,
   PharmaceuticalFormCreatedUserFullName VARCHAR(80) NOT NULL,
   PharmaceuticalFormCreatedDateTime DATETIME NOT NULL,  
   PharmaceuticalFormUpdatedUserID INT, 
   PharmaceuticalFormUpdatedUserName VARCHAR(20),
   PharmaceuticalFormUpdatedUserFullName VARCHAR(80),
   PharmaceuticalFormUpdatedDateTime DATETIME,
   CONSTRAINT PharmaceuticalForm_PK_PharmaceuticalFormID PRIMARY KEY(PharmaceuticalFormID),
   CONSTRAINT PharmaceuticalForm_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)) 
)
GO
--Tipo de prescripción
CREATE TABLE Product.PrescriptionType( 
   PrescriptionTypeID TINYINT NOT NULL IDENTITY(1,1),
   PrescriptionTypeName VARCHAR(30) NOT NULL, --Medicamento de venta libre,Medicamento que requiere receta,Medicamento con control especial (narcóticos, psicotrópicos),Suplementos o vitaminas de venta libre
   PrescriptionTypeDescription VARCHAR(150),
   StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
   CONSTRAINT PrescriptionType_PK_PrescriptionTypeID PRIMARY KEY(PrescriptionTypeID)
)
GO
CREATE TABLE Product.Brand(
   BrandID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,
   BrandName NVARCHAR(50) NOT NULL,
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   BrandCreatedUserID INT NOT NULL,
   BrandCreatedUserName VARCHAR(20) NOT NULL,
   BrandCreatedUserFullName VARCHAR(80) NOT NULL,
   BrandCreatedDateTime DATETIME NOT NULL,  
   BrandUpdatedUserID INT, 
   BrandUpdatedUserName VARCHAR(20),
   BrandUpdatedUserFullName VARCHAR(80),
   BrandUpdatedDateTime DATETIME,
   CONSTRAINT Brand_PK_BrandID PRIMARY KEY(BrandID),
   CONSTRAINT Brand_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.[Catalog](
  CatalogID INT NOT NULL IDENTITY(1,1),
  CompanyID INT NOT NULL,
  CatalogTypeID TINYINT NOT NULL, 
  CategoryID INT NOT NULL, 
  UnitMeasureID INT NOT NULL,
  CatalogSlug VARCHAR(200) NOT NULL,
  CatalogName VARCHAR(200) NOT NULL, 
  PrescriptionTypeID TINYINT,
  BrandID INT, --Proveedor/Laboratorio proviene de otra base de datos
  CatalogConcentration VARCHAR(50),
  CatalogSanitaryRegistrationNumber VARCHAR(50), 
  CatalogDescription VARCHAR(300),
  RecordOriginID TINYINT NOT NULL,
  RecordStateID TINYINT NOT NULL,
  CatalogCreatedUserID INT NOT NULL,
  CatalogCreatedUserName VARCHAR(20) NOT NULL,
  CatalogCreatedUserFullName VARCHAR(80) NOT NULL,
  CatalogCreatedDateTime DATETIME NOT NULL,  
  CatalogUpdatedUserID INT, 
  CatalogUpdatedUserName VARCHAR(20),
  CatalogUpdatedUserFullName VARCHAR(80),
  CatalogUpdatedDateTime DATETIME,
  CONSTRAINT Catalog_PK_CatalogID PRIMARY KEY(CatalogID),
  CONSTRAINT Catalog_FK_CatalogTypeID FOREIGN KEY(CatalogTypeID) REFERENCES Product.CatalogType(CatalogTypeID),
  CONSTRAINT Catalog_FK_UnitMeasureID FOREIGN KEY(UnitMeasureID) REFERENCES Product.UnitMeasure(UnitMeasureID),
  CONSTRAINT Catalog_FK_CategoryID FOREIGN KEY(CategoryID) REFERENCES Product.Category(CategoryID),
  CONSTRAINT Catalog_FK_BrandID FOREIGN KEY(BrandID) REFERENCES Product.Brand(BrandID),
  CONSTRAINT Catalog_FK_PrescriptionTypeID FOREIGN KEY(PrescriptionTypeID) REFERENCES Product.PrescriptionType(PrescriptionTypeID),
  CONSTRAINT Catalog_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
 ALTER TABLE Product.[Catalog] ADD CompanyID INT
 ALTER TABLE Product.[Catalog] ADD CatalogTypeID TINYINT
 ALTER TABLE Product.[Catalog] ADD PrescriptionTypeID TINYINT
 ALTER TABLE Product.[Catalog] ADD BrandID INT
 ALTER TABLE Product.[Catalog] ADD CatalogConcentration VARCHAR(50)
 ALTER TABLE Product.[Catalog] ADD CatalogSanitaryRegistrationNumber VARCHAR(50)
 ALTER TABLE Product.[Catalog] ADD RecordOriginID TINYINT
 ALTER TABLE Product.[Catalog] ADD RecordStateID TINYINT
 ALTER TABLE Product.[Catalog] ADD CatalogCreatedUserID INT
 ALTER TABLE Product.[Catalog] ADD CatalogCreatedUserName VARCHAR(20)
 ALTER TABLE Product.[Catalog] ADD CatalogCreatedUserFullName VARCHAR(80)
 ALTER TABLE Product.[Catalog] ADD CatalogCreatedDateTime DATETIME
 ALTER TABLE Product.[Catalog] ADD CatalogUpdatedUserID INT
 ALTER TABLE Product.[Catalog] ADD CatalogUpdatedUserName VARCHAR(20)
 ALTER TABLE Product.[Catalog] ADD CatalogUpdatedUserFullName VARCHAR(80)
 ALTER TABLE Product.[Catalog] ADD CatalogUpdatedDateTime DATETIME
 ALTER TABLE Product.[Catalog] ADD CONSTRAINT Catalog_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))

 UPDATE Product.[Catalog]
       SET RecordOriginID=1,
		   RecordStateID=1,
		   CatalogCreatedUserID=1,
		   CatalogCreatedUserName ='administrador',
		   CatalogCreatedUserFullName='Joel Castillo',
		   CatalogCreatedDateTime=getdate(),
		   CompanyID =1 ,
		   CatalogTypeID = 1
GO
CREATE TABLE Product.CatalogActiveIngredient(
   CatalogActiveIngredientID INT NOT NULL IDENTITY(1,1),
   CatalogID INT NOT NULL,
   ActiveIngredientID INT NOT NULL,
   CatalogActiveIngredientQuantity NUMERIC(5,2) NOT NULL,--0.5,20.00
   UnitMeasureID INT NOT NULL --Unidad,mg,mg/mL
   CONSTRAINT CatalogActiveIngredient_PK_CatalogActiveIngredientID PRIMARY KEY(CatalogActiveIngredientID),
   CONSTRAINT CatalogActiveIngredient_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
   CONSTRAINT CatalogActiveIngredient_FK_ActiveIngredientID FOREIGN KEY(ActiveIngredientID) REFERENCES Product.ActiveIngredient(ActiveIngredientID),
   CONSTRAINT CatalogActiveIngredient_FK_UnitMeasureID FOREIGN KEY(UnitMeasureID) REFERENCES Product.UnitMeasure(UnitMeasureID) 
)
GO
CREATE TABLE Product.Presentation(
   PresentationID INT NOT NULL IDENTITY(1,1),
   UnitMeasureID INT NOT NULL,
   PharmaceuticalFormID SMALLINT,
   PresentationName VARCHAR(50) NOT NULL,
   PresentationEquivalence NUMERIC(6,2) NOT NULL,
   StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
   CONSTRAINT Presentation_PK_PresentationID PRIMARY KEY(PresentationID),
   CONSTRAINT Presentation_FK_UnitMeasureID FOREIGN KEY(UnitMeasureID) REFERENCES Product.UnitMeasure(UnitMeasureID),
   CONSTRAINT Presentation_FK_PharmaceuticalFormID FOREIGN KEY(PharmaceuticalFormID) REFERENCES Product.PharmaceuticalForm(PharmaceuticalFormID)
)
GO 
CREATE TABLE Product.CatalogPresentation(
   CatalogPresentationID INT NOT NULL IDENTITY(1,1),  
   CatalogID INT NOT NULL,
   PresentationID INT NOT NULL,
   CatalogPresentationIsDefault BIT NOT NULL DEFAULT 0,
   CatalogPresentationEquivalence NUMERIC(6,2) NOT NULL,
   CatalogPresentationVariantName VARCHAR(20) NOT NULL,	
   CatalogPresentationSKU VARCHAR(20),
   CatalogPresentationQRCode VARCHAR(100),   
   StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
   CONSTRAINT CatalogPresentation_PK_CatalogPresentationID PRIMARY KEY(CatalogPresentationID),
   CONSTRAINT CatalogPresentation_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
   CONSTRAINT CatalogPresentation_FK_PresentationID FOREIGN KEY(PresentationID) REFERENCES Product.Presentation(PresentationID)  
)
GO
CREATE UNIQUE INDEX CatalogPresentation_UQ_CatalogPresentationIsDefault
              ON Product.CatalogPresentation(CatalogID) 
              WHERE CatalogPresentationIsDefault = 1

GO 
CREATE TABLE Product.CatalogStock(
    CatalogStockID INT NOT NULL IDENTITY(1,1),
	CatalogPresentationID INT NOT NULL,
	EstablishmentID INT NOT NULL, --Biene de otra BD
	CatalogStockInitialQuantity NUMERIC(12,6),
	CatalogStockCurrentQuantity NUMERIC(12,6),
	CatalogStockPhysicalQuantity NUMERIC(12,6),
	CatalogStockMinimumQuantity NUMERIC(12,6),
	CatalogStockMaximumQuantity NUMERIC(12,6),
    StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
    CONSTRAINT CatalogStock_PK_CatalogStockID PRIMARY KEY(CatalogStockID),
    CONSTRAINT CatalogStock_FK_CatalogPresentationID FOREIGN KEY(CatalogPresentationID) REFERENCES Product.CatalogPresentation(CatalogPresentationID)
)
GO

CREATE TABLE Product.PriceType( 
   PriceTypeID TINYINT NOT NULL,
   PriceTypeName VARCHAR(30) NOT NULL,
   StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
   CONSTRAINT PriceType_PK_PriceTypeID PRIMARY KEY(PriceTypeID)
)
GO
CREATE TABLE Product.CatalogPrice(
    CatalogPriceID INT NOT NULL IDENTITY(1,1),
	CatalogPresentationID INT NOT NULL,
	EstablishmentID INT NOT NULL, --Biene de otra BD
	PriceTypeID TINYINT NOT NULL,
	CurrencyTypeID TINYINT NOT NULL, --Biene de otra BD
	CatalogPriceSale NUMERIC(12,6) NOT NULL,
	--CatalogPriceBase NUMERIC(12,6) NOT NULL,
	StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
	CONSTRAINT CatalogPrice_PK_CatalogPriceID PRIMARY KEY(CatalogPriceID),
	CONSTRAINT CatalogPrice_FK_PriceTypeID FOREIGN KEY(PriceTypeID) REFERENCES Product.PriceType(PriceTypeID),
	CONSTRAINT CatalogPrice_FK_CatalogID FOREIGN KEY(CatalogPresentationID) REFERENCES Product.CatalogPresentation(CatalogPresentationID)
)
/*
 Trabaja con stock (IsStockManaged)
   👉 Significa: el producto TIENE inventario

       Existe cantidad disponible
       Se puede contar, ajustar, auditar
       Puede estar en 0, 10, 100 unidades

 Afecta stock (IsAffectStock)
  👉 Significa: las operaciones con este producto MUEVEN el inventario

    Al vender → descuenta
    Al comprar → suma
    Al devolver → ajusta
*/
GO
CREATE TABLE Product.CatalogConfiguration(
    CatalogConfigurationID INT NOT NULL IDENTITY(1,1),
	CatalogID INT NOT NULL,
	EstablishmentID INT NOT NULL, --Biene de otra BD
	CatalogConfigurationIsStockManaged BIT NOT NULL DEFAULT 0,
    CatalogConfigurationIsAffectStock BIT NOT NULL DEFAULT 0,
    StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2))
    CONSTRAINT CatalogConfiguration_PK_CatalogConfigurationID PRIMARY KEY(CatalogConfigurationID),
    CONSTRAINT CatalogConfiguration_PK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID)
)
GO
CREATE TABLE Product.CatalogTax(
   CatalogTaxID INT NOT NULL IDENTITY(1,1),
   CatalogID INT NOT NULL,  
   EstablishmentID INT NOT NULL, --Biene de otra BD 
   TaxID SMALLINT NOT NULL,--Biene de otra DB
   CalculationTypeID TINYINT NOT NULL,--Biene de otra DB 1=Porcentaje ,2 = Monto por unidad
   CatalogTaxValor NUMERIC(5,2) NOT NULL, --Valor puedes de 18 si CalculationTypeID=1 es porcentaje,
   TaxDirectionID TINYINT NOT NULL,  --1071 Impuesto para Venta ,1072 Impuesto para compra
   TaxAffectationTypeID TINYINT NOT NULL,  --Gravado=1,Exonerado=2,Inafecto=3,Gratuita=4
   CONSTRAINT CatalogTax_PK_CatalogTaxID PRIMARY KEY(CatalogTaxID),
   CONSTRAINT CatalogTax_PK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID)
)
GO
CREATE TABLE Product.GalleryType(
  GalleryTypeID TINYINT NOT NULL,
  GalleryTypeName VARCHAR(20) NOT NULL,
  StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
  CONSTRAINT GalleryType_PK_GalleryTypeID PRIMARY KEY(GalleryTypeID),
)
GO
CREATE TABLE Product.CatalogGallery(
  CatalogGalleryID INT NOT NULL IDENTITY(1,1),
  CatalogID INT NOT NULL, 
  GalleryTypeID TINYINT NOT NULL, 
  CatalogGalleryFileName VARCHAR(100) NOT NULL ,
  CatalogGalleryPublication DATETIME,
  StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
  CONSTRAINT CatalogGallery_PK_CatalogGalleryID PRIMARY KEY(CatalogGalleryID),
  CONSTRAINT CatalogGallery_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
  CONSTRAINT CatalogGallery_FK_GalleryTypeID FOREIGN KEY(GalleryTypeID) REFERENCES Product.GalleryType(GalleryTypeID)
)