USE master
GO
USE [Commerce360]
GO
CREATE SCHEMA Product
GO
CREATE TABLE Product.Category(
   CategoryID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,
   CategoryName NVARCHAR(100) NOT NULL,
   CategorySlug NVARCHAR(100) NOT NULL,
   CategoryImage VARCHAR(100),   
   RecordOriginID TINYINT NOT NULL, --WebPForm=1
   RecordStateID TINYINT NOT NULL,
   CategoryCreatedUserID INT NOT NULL,
   CategoryCreatedUserName NVARCHAR(20) NOT NULL,
   CategoryCreatedUserFullName NVARCHAR(80) NOT NULL,
   CategoryCreatedDateTime DATETIME NOT NULL,  
   CategoryUpdatedUserID INT, 
   CategoryUpdatedUserName NVARCHAR(20),
   CategoryUpdatedUserFullName NVARCHAR(80),
   CategoryUpdatedDateTime DATETIME,
   CONSTRAINT Category_PK_CategoryID PRIMARY KEY(CategoryID),
   CONSTRAINT Category_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.UnitMeasure(
  UnitMeasureID INT NOT NULL IDENTITY(1,1),
  CountryID INT NOT NULL,
  UnitMeasureCode VARCHAR(10) NOT NULL,
  UnitMeasureName NVARCHAR(20) NOT NULL,
  UnitMeasureFactorConversion NUMERIC(10,6),
  RecordOriginID TINYINT NOT NULL,
  RecordStateID TINYINT NOT NULL,
  UnitMeasureCreatedUserID INT NOT NULL,
  UnitMeasureCreatedUserName NVARCHAR(20) NOT NULL,
  UnitMeasureCreatedUserFullName NVARCHAR(80) NOT NULL,
  UnitMeasureCreatedDateTime DATETIME NOT NULL,  
  UnitMeasureUpdatedUserID INT, 
  UnitMeasureUpdatedUserName NVARCHAR(20),
  UnitMeasureUpdatedUserFullName NVARCHAR(80),
  UnitMeasureUpdatedDateTime DATETIME,
  CONSTRAINT UnitMeasure_PK_UnitMeasureID PRIMARY KEY(UnitMeasureID),
  CONSTRAINT UnitMeasure_UQ_CountryIDAndUnitMeasureCode UNIQUE (CountryID, UnitMeasureCode),
  CONSTRAINT UnitMeasure_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.Presentation(
   PresentationID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,
   UnitMeasureID INT NOT NULL, 
   PresentationName NVARCHAR(50) NOT NULL,
   PresentationEquivalence NUMERIC(6,2) NOT NULL,   
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   PresentationCreatedUserID INT NOT NULL,
   PresentationCreatedUserName NVARCHAR(20) NOT NULL,
   PresentationCreatedUserFullName NVARCHAR(80) NOT NULL,
   PresentationCreatedDateTime DATETIME NOT NULL,  
   PresentationUpdatedUserID INT, 
   PresentationUpdatedUserName NVARCHAR(20),
   PresentationUpdatedUserFullName NVARCHAR(80),
   PresentationUpdatedDateTime DATETIME,
   CONSTRAINT Presentation_PK_PresentationID PRIMARY KEY(PresentationID),
   CONSTRAINT Presentation_FK_UnitMeasureID FOREIGN KEY(UnitMeasureID) REFERENCES Product.UnitMeasure(UnitMeasureID),
   CONSTRAINT Presentation_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.CatalogType(
   CatalogTypeID TINYINT NOT NULL, --1=PRODUCTO,2=SERVICIO,3=CONCEPTO,4=ACTIVO FIJO
   CatalogTypeName NVARCHAR(50) NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CONSTRAINT CatalogType_PK_CatalogTypeID PRIMARY KEY(CatalogTypeID),
   CONSTRAINT CatalogType_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)) 
)
GO
--Tipo de prescripción
CREATE TABLE Product.PrescriptionType( 
   PrescriptionTypeID TINYINT NOT NULL IDENTITY(1,1),
   PrescriptionTypeName NVARCHAR(30) NOT NULL, --Medicamento de venta libre,Medicamento que requiere receta,Medicamento con control especial (narcóticos, psicotrópicos),Suplementos o vitaminas de venta libre
   PrescriptionTypeDescription NVARCHAR(150),
   RecordStateID TINYINT NOT NULL,
   CONSTRAINT PrescriptionType_PK_PrescriptionTypeID PRIMARY KEY(PrescriptionTypeID),
   CONSTRAINT PrescriptionType_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)) 
)
GO
CREATE TABLE Product.Manufacturer ( -- Fabricante oh Laboratorio
   ManufacturerID INT NOT NULL IDENTITY(1,1),   
   ManufacturerName NVARCHAR(50) NOT NULL,
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   ManufacturerCreatedUserID INT NOT NULL,
   ManufacturerCreatedUserName NVARCHAR(20) NOT NULL,
   ManufacturerCreatedUserFullName NVARCHAR(80) NOT NULL,
   ManufacturerCreatedDateTime DATETIME NOT NULL,  
   ManufacturerUpdatedUserID INT, 
   ManufacturerUpdatedUserName NVARCHAR(20),
   ManufacturerUpdatedUserFullName NVARCHAR(80),
   ManufacturerUpdatedDateTime DATETIME,
   CONSTRAINT Manufacturer_PK_ManufacturerID PRIMARY KEY(ManufacturerID),
   CONSTRAINT Manufacturer_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.Brand(
   BrandID INT NOT NULL IDENTITY(1,1),   
   BrandName NVARCHAR(50) NOT NULL,
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   BrandCreatedUserID INT NOT NULL,
   BrandCreatedUserName NVARCHAR(20) NOT NULL,
   BrandCreatedUserFullName NVARCHAR(80) NOT NULL,
   BrandCreatedDateTime DATETIME NOT NULL,  
   BrandUpdatedUserID INT, 
   BrandUpdatedUserName NVARCHAR(20),
   BrandUpdatedUserFullName NVARCHAR(80),
   BrandUpdatedDateTime DATETIME,
   CONSTRAINT Brand_PK_BrandID PRIMARY KEY(BrandID),
   CONSTRAINT Brand_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.Attribute(
     AttributeID TINYINT NOT NULL,
	 AttributeName NVARCHAR(50) NOT NULL,
	 AttributeIsVariantAttribute BIT NOT NULL,
	 RecordOriginID TINYINT NOT NULL,
	 RecordStateID TINYINT NOT NULL,
	 AttributeCreatedUserID INT NOT NULL,
	 AttributeCreatedUserName NVARCHAR(20) NOT NULL,
     AttributeCreatedUserFullName NVARCHAR(80) NOT NULL,
     AttributeCreatedDateTime DATETIME NOT NULL,  
     AttributeUpdatedUserID INT, 
     AttributeUpdatedUserName NVARCHAR(20),
     AttributeUpdatedUserFullName NVARCHAR(80),
     AttributeUpdatedDateTime DATETIME,
	 CONSTRAINT Attribute_PK_AttributeID PRIMARY KEY(AttributeID),
	 CONSTRAINT Attribute_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.AttributeValue(
     AttributeValueID SMALLINT NOT NULL IDENTITY(1,1),
     AttributeID TINYINT NOT NULL,
	 AttributeValueName NVARCHAR(50) NOT NULL,
	 RecordOriginID TINYINT NOT NULL,
	 RecordStateID TINYINT NOT NULL,
	 AttributeValueCreatedUserID INT NOT NULL,
	 AttributeValueCreatedUserName NVARCHAR(20) NOT NULL,
     AttributeValueCreatedUserFullName NVARCHAR(80) NOT NULL,
     AttributeValueCreatedDateTime DATETIME NOT NULL,  
     AttributeValueUpdatedUserID INT, 
     AttributeValueUpdatedUserName NVARCHAR(20),
     AttributeValueUpdatedUserFullName NVARCHAR(80),
     AttributeValueUpdatedDateTime DATETIME,
	 CONSTRAINT AttributeValue_PK_AttributeValueID PRIMARY KEY(AttributeValueID),
	 CONSTRAINT AttributeValue_FK_AttributeID FOREIGN KEY(AttributeID) REFERENCES Product.Attribute(AttributeID),
	 CONSTRAINT AttributeValue_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.PharmaceuticalForm( 
   PharmaceuticalFormID SMALLINT NOT NULL IDENTITY(1,1),
   PharmaceuticalFormName NVARCHAR(50) NOT NULL,
   PharmaceuticalFormDescription NVARCHAR(150),
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   PharmaceuticalFormCreatedUserID INT NOT NULL,
   PharmaceuticalFormCreatedUserName NVARCHAR(20) NOT NULL,
   PharmaceuticalFormCreatedUserFullName NVARCHAR(80) NOT NULL,
   PharmaceuticalFormCreatedDateTime DATETIME NOT NULL,  
   PharmaceuticalFormUpdatedUserID INT, 
   PharmaceuticalFormUpdatedUserName NVARCHAR(20),
   PharmaceuticalFormUpdatedUserFullName NVARCHAR(80),
   PharmaceuticalFormUpdatedDateTime DATETIME,
   CONSTRAINT PharmaceuticalForm_PK_PharmaceuticalFormID PRIMARY KEY(PharmaceuticalFormID),
   CONSTRAINT PharmaceuticalForm_UQ_PharmaceuticalFormName UNIQUE(PharmaceuticalFormName),
   CONSTRAINT PharmaceuticalForm_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)) 
)
GO
CREATE TABLE Product.ActiveIngredient( 
   ActiveIngredientID INT NOT NULL IDENTITY(1,1),
   ActiveIngredientName NVARCHAR(50) NOT NULL,
   ActiveIngredientDescription NVARCHAR(150),
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   ActiveIngredientCreatedUserID INT NOT NULL,
   ActiveIngredientCreatedUserName NVARCHAR(20) NOT NULL,
   ActiveIngredientCreatedUserFullName NVARCHAR(80) NOT NULL,
   ActiveIngredientCreatedDateTime DATETIME NOT NULL,  
   ActiveIngredientUpdatedUserID INT, 
   ActiveIngredientUpdatedUserName NVARCHAR(20),
   ActiveIngredientUpdatedUserFullName NVARCHAR(80),
   ActiveIngredientUpdatedDateTime DATETIME,
   CONSTRAINT ActiveIngredient_PK_ActiveIngredientID PRIMARY KEY(ActiveIngredientID),
   CONSTRAINT ActiveIngredient_UQ_ActiveIngredientName UNIQUE(ActiveIngredientName), 
   CONSTRAINT ActiveIngredient_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)) 
)
GO
CREATE TABLE Product.TherapeuticAction(
    TherapeuticActionID SMALLINT NOT NULL IDENTITY(1,1),
    TherapeuticActionName NVARCHAR(80) NOT NULL,
    TherapeuticActionDescription NVARCHAR(200) NULL,
    RecordOriginID TINYINT NOT NULL,
    RecordStateID TINYINT NOT NULL,
    TherapeuticActionCreatedUserID INT NOT NULL,
    TherapeuticActionCreatedUserName NVARCHAR(20) NOT NULL,
    TherapeuticActionCreatedUserFullName NVARCHAR(80) NOT NULL,
    TherapeuticActionCreatedDateTime DATETIME NOT NULL,
    TherapeuticActionUpdatedUserID INT NULL,
    TherapeuticActionUpdatedUserName NVARCHAR(20) NULL,
    TherapeuticActionUpdatedUserFullName NVARCHAR(80) NULL,
    TherapeuticActionUpdatedDateTime DATETIME NULL,
    CONSTRAINT TherapeuticAction_PK_TherapeuticActionID PRIMARY KEY(TherapeuticActionID),
    CONSTRAINT TherapeuticAction_UQ_TherapeuticActionName UNIQUE(TherapeuticActionName),
    CONSTRAINT TherapeuticAction_CHK_RecordStateID CHECK (RecordStateID IN (0,1,2))
)
GO
CREATE TABLE Product.[Catalog](
  CatalogID INT NOT NULL IDENTITY(1,1),
  CompanyID INT NOT NULL,
  CatalogTypeID TINYINT NOT NULL, 
  CategoryID INT NOT NULL, 
  CatalogSlug NVARCHAR(200) NOT NULL,
  CatalogName NVARCHAR(200) NOT NULL, 
  PrescriptionTypeID TINYINT,
  ManufacturerID INT,
  BrandID INT, --Proveedor/Laboratorio proviene de otra base de datos
  --CatalogConcentration NVARCHAR(50),
  --CatalogSanitaryRegistrationNumber NVARCHAR(50)
  PharmaceuticalFormID SMALLINT, 
  CatalogDescription NVARCHAR(300),
  CatalogHasVariants BIT NOT NULL,
  CatalogBrandType NVARCHAR(15) NOT NULL,
  RecordOriginID TINYINT NOT NULL,
  RecordStateID TINYINT NOT NULL,
  CatalogCreatedUserID INT NOT NULL,
  CatalogCreatedUserName NVARCHAR(20) NOT NULL,
  CatalogCreatedUserFullName NVARCHAR(80) NOT NULL,
  CatalogCreatedDateTime DATETIME NOT NULL,  
  CatalogUpdatedUserID INT, 
  CatalogUpdatedUserName NVARCHAR(20),
  CatalogUpdatedUserFullName NVARCHAR(80),
  CatalogUpdatedDateTime DATETIME,
  CONSTRAINT Catalog_PK_CatalogID PRIMARY KEY(CatalogID),
  CONSTRAINT Catalog_FK_CatalogTypeID FOREIGN KEY(CatalogTypeID) REFERENCES Product.CatalogType(CatalogTypeID),  
  CONSTRAINT Catalog_FK_CategoryID FOREIGN KEY(CategoryID) REFERENCES Product.Category(CategoryID),
  CONSTRAINT Catalog_FK_ManufacturerID FOREIGN KEY(ManufacturerID) REFERENCES Product.Manufacturer(ManufacturerID),
  CONSTRAINT Catalog_FK_BrandID FOREIGN KEY(BrandID) REFERENCES Product.Brand(BrandID),
  CONSTRAINT Catalog_FK_PrescriptionTypeID FOREIGN KEY(PrescriptionTypeID) REFERENCES Product.PrescriptionType(PrescriptionTypeID),
  CONSTRAINT Catalog_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)),
  CONSTRAINT Catalog_CHK_CatalogBrandType CHECK(CatalogBrandType IN('NINGUNO','GENERICO','COMERCIAL'))
)
GO
CREATE TABLE Product.CatalogActiveIngredient(
   CatalogActiveIngredientID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,
   CatalogID INT NOT NULL,
   ActiveIngredientID INT NOT NULL,
   CatalogActiveIngredientQuantity NUMERIC(5,2) NOT NULL,--0.5,20.00
   UnitMeasureID INT NOT NULL, --Unidad,mg,mg/mL
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CatalogActiveIngredientCreatedUserID INT NOT NULL,
   CatalogActiveIngredientCreatedUserName NVARCHAR(20) NOT NULL,
   CatalogActiveIngredientCreatedUserFullName NVARCHAR(80) NOT NULL,
   CatalogActiveIngredientCreatedDateTime DATETIME NOT NULL,  
   CatalogActiveIngredientUpdatedUserID INT, 
   CatalogActiveIngredientUpdatedUserName NVARCHAR(20),
   CatalogActiveIngredientUpdatedUserFullName NVARCHAR(80),
   CatalogActiveIngredientUpdatedDateTime DATETIME,
   CONSTRAINT CatalogActiveIngredient_PK_CatalogActiveIngredientID PRIMARY KEY(CatalogActiveIngredientID),
   CONSTRAINT CatalogActiveIngredient_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
   CONSTRAINT CatalogActiveIngredient_FK_ActiveIngredientID FOREIGN KEY(ActiveIngredientID) REFERENCES Product.ActiveIngredient(ActiveIngredientID),
   CONSTRAINT CatalogActiveIngredient_FK_UnitMeasureID FOREIGN KEY(UnitMeasureID) REFERENCES Product.UnitMeasure(UnitMeasureID) 
)
GO 
CREATE TABLE Product.CatalogTherapeuticAction(  
   CompanyID INT NOT NULL, 
   CatalogID INT NOT NULL,
   TherapeuticActionID SMALLINT NOT NULL,
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CatalogTherapeuticActionCreatedUserID INT NOT NULL,
   CatalogTherapeuticActionCreatedUserName NVARCHAR(20) NOT NULL,
   CatalogTherapeuticActionCreatedUserFullName NVARCHAR(80) NOT NULL,
   CatalogTherapeuticActionCreatedDateTime DATETIME NOT NULL,  
   CatalogTherapeuticActionUpdatedUserID INT, 
   CatalogTherapeuticActionUpdatedUserName NVARCHAR(20),
   CatalogTherapeuticActionUpdatedUserFullName NVARCHAR(80),
   CatalogTherapeuticActionUpdatedDateTime DATETIME, 
   CONSTRAINT CatalogTherapeuticAction_PK_CatalogID_TherapeuticActionID PRIMARY KEY(CatalogID,TherapeuticActionID),
   CONSTRAINT CatalogTherapeuticAction_FK_CatalogID_ FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
   CONSTRAINT CatalogTherapeuticAction_FK_TherapeuticActionID FOREIGN KEY(TherapeuticActionID) REFERENCES Product.TherapeuticAction(TherapeuticActionID),
)
GO 

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
	CompanyID INT NOT NULL,
	EstablishmentID INT NOT NULL, --Biene de otra BD
	CatalogID INT NOT NULL,	
	CatalogConfigurationIsStockManaged BIT NOT NULL DEFAULT 0,
    CatalogConfigurationIsAffectStock BIT NOT NULL DEFAULT 0,
    RecordOriginID TINYINT NOT NULL,
    RecordStateID TINYINT NOT NULL,
    CatalogConfigurationCreatedUserID INT NOT NULL,
    CatalogConfigurationCreatedUserName NVARCHAR(20) NOT NULL,
    CatalogConfigurationCreatedUserFullName NVARCHAR(80) NOT NULL,
    CatalogConfigurationCreatedDateTime DATETIME NOT NULL,
    CatalogConfigurationUpdatedUserID INT,
    CatalogConfigurationUpdatedUserName NVARCHAR(20),
    CatalogConfigurationUpdatedUserFullName NVARCHAR(80),
    CatalogConfigurationUpdatedDateTime DATETIME,
    CONSTRAINT CatalogConfiguration_PK_CatalogConfigurationID PRIMARY KEY(CatalogConfigurationID),
    CONSTRAINT CatalogConfiguration_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
    CONSTRAINT CatalogConfiguration_CHK_RecordStateID  CHECK (RecordStateID IN (0,1,2))
)
GO
CREATE TABLE Product.CatalogTax(
   CatalogTaxID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,
   CatalogID INT NOT NULL, 
   TaxID SMALLINT NOT NULL,--Biene de otra DB
   CalculationTypeID TINYINT NOT NULL,--Biene de otra DB 1=Porcentaje ,2 = Monto por unidad    
   TaxDirectionID TINYINT NOT NULL,  --1071 Impuesto para Venta ,1072 Impuesto para compra
   TaxAffectationTypeID TINYINT NOT NULL,  --Gravado=1,Exonerado=2,Inafecto=3,Gratuita=4
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CatalogTaxCreatedUserID INT NOT NULL,
   CatalogTaxCreatedUserName NVARCHAR(20) NOT NULL,
   CatalogTaxCreatedUserFullName NVARCHAR(80) NOT NULL,
   CatalogTaxCreatedDateTime DATETIME NOT NULL,
   CatalogTaxUpdatedUserID INT,
   CatalogTaxUpdatedUserName NVARCHAR(20),
   CatalogTaxUpdatedUserFullName NVARCHAR(80),
   CatalogTaxUpdatedDateTime DATETIME,
   CONSTRAINT CatalogTax_PK_CatalogTaxID PRIMARY KEY(CatalogTaxID),
   CONSTRAINT CatalogTax_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
   CONSTRAINT CatalogTax_CHK_RecordStateID CHECK(RecordStateID IN (0,1,2))
)
GO
CREATE TABLE Product.CatalogTaxExemption(
   CatalogTaxExemptionID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,
   EstablishmentID  INT NOT NULL,
   CatalogID INT NOT NULL,  --Biene de otra DB
   TaxID SMALLINT NOT NULL,--Biene de otra DB 
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CatalogTaxExemptionCreatedUserID INT NOT NULL,
   CatalogTaxExemptionCreatedUserName NVARCHAR(20) NOT NULL,
   CatalogTaxExemptionCreatedUserFullName NVARCHAR(80) NOT NULL,
   CatalogTaxExemptionCreatedDateTime DATETIME NOT NULL,
   CatalogTaxExemptionUpdatedUserID INT,
   CatalogTaxExemptionUpdatedUserName NVARCHAR(20),
   CatalogTaxExemptionUpdatedUserFullName NVARCHAR(80),
   CatalogTaxExemptionUpdatedDateTime DATETIME,
   CONSTRAINT CatalogTaxExemption_PK_CatalogTaxExemptionID PRIMARY KEY(CatalogTaxExemptionID),
   CONSTRAINT CatalogTaxExemption_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
   CONSTRAINT CatalogTaxExemption_CHK_RecordStateID CHECK(RecordStateID IN (0,1,2))
)
GO
CREATE TABLE Product.CatalogAttributeValue(
     CatalogID INT NOT NULL,
	 AttributeValueID SMALLINT NOT NULL,
	 CompanyID INT NOT NULL,
	 RecordOriginID TINYINT NOT NULL,
	 RecordStateID TINYINT NOT NULL,
	 CatalogAttributeValueCreatedUserID INT NOT NULL,
	 CatalogAttributeValueCreatedUserName NVARCHAR(20) NOT NULL,
     CatalogAttributeValueCreatedUserFullName NVARCHAR(80) NOT NULL,
     CatalogAttributeValueCreatedDateTime DATETIME NOT NULL,  
     CatalogAttributeValueUpdatedUserID INT, 
     CatalogAttributeValueUpdatedUserName NVARCHAR(20),
     CatalogAttributeValueUpdatedUserFullName NVARCHAR(80),
     CatalogAttributeValueUpdatedDateTime DATETIME,
	 CONSTRAINT CatalogAttributeValue_PK_CatalogID_AttributeValueID PRIMARY KEY(CatalogID,AttributeValueID),
	 CONSTRAINT CatalogAttributeValue_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
	 CONSTRAINT CatalogAttributeValue_FK_AttributeValueID FOREIGN KEY(AttributeValueID) REFERENCES Product.AttributeValue(AttributeValueID),
	 CONSTRAINT CatalogAttributeValue_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.CatalogVariant(
    CatalogVariantID INT NOT NULL IDENTITY(1,1),
	CompanyID INT NOT NULL,
	CatalogID INT NOT NULL,
	CatalogVariantName NVARCHAR(50) NOT NULL,
    RecordOriginID TINYINT NOT NULL,
	RecordStateID TINYINT NOT NULL,
	CatalogVariantCreatedUserID INT NOT NULL,
	CatalogVariantCreatedUserName NVARCHAR(20) NOT NULL,
    CatalogVariantCreatedUserFullName NVARCHAR(80) NOT NULL,
    CatalogVariantCreatedDateTime DATETIME NOT NULL,  
    CatalogVariantUpdatedUserID INT, 
    CatalogVariantUpdatedUserName NVARCHAR(20),
    CatalogVariantUpdatedUserFullName NVARCHAR(80),
    CatalogVariantUpdatedDateTime DATETIME,
	CONSTRAINT CatalogVariant_PK_CatalogVariantID PRIMARY KEY(CatalogVariantID),
    CONSTRAINT CatalogVariant_FK_CatalogID FOREIGN KEY(CatalogID) REFERENCES Product.[Catalog](CatalogID),
    CONSTRAINT CatalogVariant_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.CatalogVariantValue(
     CatalogVariantID INT NOT NULL,
	 AttributeValueID SMALLINT NOT NULL,
	 CompanyID INT NOT NULL,
	 RecordOriginID TINYINT NOT NULL,
	 RecordStateID TINYINT NOT NULL,
	 CatalogVariantValueCreatedUserID INT NOT NULL,
	 CatalogVariantValueCreatedUserName NVARCHAR(20) NOT NULL,
     CatalogVariantValueCreatedUserFullName NVARCHAR(80) NOT NULL,
     CatalogVariantValueCreatedDateTime DATETIME NOT NULL,  
     CatalogVariantValueUpdatedUserID INT, 
     CatalogVariantValueUpdatedUserName NVARCHAR(20),
     CatalogVariantValueUpdatedUserFullName NVARCHAR(80),
     CatalogVariantValueUpdatedDateTime DATETIME,
	 CONSTRAINT CatalogVariantValue_PK_CatalogVariantID_AttributeValueID PRIMARY KEY(CatalogVariantID,AttributeValueID),
	 CONSTRAINT CatalogVariantValue_FK_CatalogVariantID FOREIGN KEY(CatalogVariantID) REFERENCES Product.CatalogVariant(CatalogVariantID),
	 CONSTRAINT CatalogVariantValue_FK_AttributeValueID FOREIGN KEY(AttributeValueID) REFERENCES Product.AttributeValue(AttributeValueID),
	 CONSTRAINT CatalogVariantValue_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE TABLE Product.CatalogPresentation(
   CatalogPresentationID INT NOT NULL IDENTITY(1,1),  
   CompanyID INT NOT NULL,
   CatalogVariantID INT NOT NULL,
   PresentationID INT NOT NULL, 
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CatalogPresentationIsDefault BIT NOT NULL DEFAULT 0,
   CatalogPresentationEquivalence NUMERIC(6,2) NOT NULL, 	
   CatalogPresentationSKU NVARCHAR(20),
   CatalogPresentationQRCode NVARCHAR(100), 
   CatalogPresentationCreatedUserID INT NOT NULL,
   CatalogPresentationCreatedUserName NVARCHAR(20) NOT NULL,
   CatalogPresentationCreatedUserFullName NVARCHAR(80) NOT NULL,
   CatalogPresentationCreatedDateTime DATETIME NOT NULL,  
   CatalogPresentationUpdatedUserID INT, 
   CatalogPresentationUpdatedUserName NVARCHAR(20),
   CatalogPresentationUpdatedUserFullName NVARCHAR(80),
   CatalogPresentationUpdatedDateTime DATETIME, 
   CONSTRAINT CatalogPresentation_PK_CatalogPresentationID PRIMARY KEY(CatalogPresentationID),
   CONSTRAINT CatalogPresentation_FK_CatalogVariantID FOREIGN KEY(CatalogVariantID) REFERENCES Product.CatalogVariant(CatalogVariantID),
   CONSTRAINT CatalogPresentation_FK_PresentationID FOREIGN KEY(PresentationID) REFERENCES Product.Presentation(PresentationID),
   CONSTRAINT CatalogPresentation_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))  
)
GO
CREATE UNIQUE INDEX CatalogPresentation_UQ_CatalogPresentationIsDefault
              ON Product.CatalogPresentation(CatalogVariantID) 
              WHERE CatalogPresentationIsDefault = 1

GO
CREATE TABLE Product.CatalogLot(
    CatalogLotID INT NOT NULL IDENTITY(1,1),
    CompanyID INT NOT NULL,
    CatalogVariantID INT NOT NULL,
    CatalogLotNumber NVARCHAR(50) NOT NULL,
    CatalogLotManufacturingDate DATE NULL,
    CatalogLotExpirationDate DATE NULL,
    RecordOriginID TINYINT NOT NULL,
    RecordStateID TINYINT NOT NULL,
    CatalogLotCreatedUserID INT NOT NULL,
    CatalogLotCreatedUserName NVARCHAR(20) NOT NULL,
    CatalogLotCreatedUserFullName NVARCHAR(80) NOT NULL,
    CatalogLotCreatedDateTime DATETIME NOT NULL,
    CatalogLotUpdatedUserID INT NULL,
    CatalogLotUpdatedUserName NVARCHAR(20) NULL,
    CatalogLotUpdatedUserFullName NVARCHAR(80) NULL,
    CatalogLotUpdatedDateTime DATETIME NULL,
    CONSTRAINT CatalogLot_PK_CatalogLotID PRIMARY KEY (CatalogLotID),
    CONSTRAINT CatalogLot_FK_CatalogVariantID FOREIGN KEY (CatalogVariantID) REFERENCES Product.CatalogVariant(CatalogVariantID),
    CONSTRAINT CatalogLot_UQ UNIQUE (CatalogVariantID,CatalogLotNumber),
    CONSTRAINT CatalogLot_CHK_RecordStateID CHECK (RecordStateID IN (0,1,2))
)
GO 
CREATE TABLE Product.CatalogStock(
    CatalogStockID INT NOT NULL IDENTITY(1,1),
	CompanyID INT NOT NULL,
	CatalogVariantID INT NOT NULL,
	WarehouseID INT NOT NULL, --Biene de otra BD	 
	CatalogStockCurrentQuantity NUMERIC(12,6) NOT NULL, 
	CatalogStockMinimumQuantity NUMERIC(12,6),
	CatalogStockMaximumQuantity NUMERIC(12,6),
    RecordOriginID TINYINT NOT NULL,
    RecordStateID TINYINT NOT NULL,
	CatalogStockCreatedUserID INT NOT NULL,
    CatalogStockCreatedUserName NVARCHAR(20) NOT NULL,
    CatalogStockCreatedUserFullName NVARCHAR(80) NOT NULL,
    CatalogStockCreatedDateTime DATETIME NOT NULL,  
    CatalogStockUpdatedUserID INT, 
    CatalogStockUpdatedUserName NVARCHAR(20),
    CatalogStockUpdatedUserFullName NVARCHAR(80),
    CatalogStockUpdatedDateTime DATETIME, 
    CONSTRAINT CatalogStock_PK_CatalogStockID PRIMARY KEY(CatalogStockID),
    CONSTRAINT CatalogStock_FK_CatalogVariantID FOREIGN KEY(CatalogVariantID) REFERENCES Product.CatalogVariant(CatalogVariantID),
	CONSTRAINT CatalogStock_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))  
)
GO
CREATE TABLE Product.ReasonType(
   ReasonTypeID TINYINT NOT NULL,
   ReasonTypeName NVARCHAR(50) NOT NULL,
   ReasonTypeDescription NVARCHAR(50) NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CONSTRAINT MovementType_PK_MovementTypeID PRIMARY KEY(ReasonTypeID),
   CONSTRAINT MovementType_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)) 
)
GO
CREATE TABLE Product.CatalogMovement(
    CatalogMovementID INT NOT NULL IDENTITY(1,1),
	CompanyID INT NOT NULL,
	CatalogVariantID INT NOT NULL,
	CatalogLotID INT,
	WarehouseID  INT NOT NULL, --Biene de otra BD		
	CatalogMovementDate DATE NOT NULL,
	CatalogMovementQuantity NUMERIC(12,6) NOT NULL, --Guardar con signo
	CatalogMovementType VARCHAR(10) NOT NULL,
	ReasonTypeID TINYINT NOT NULL,
	ReferenceID BIGINT,  --ID del documento origen
	RecordOriginID TINYINT NOT NULL,
    RecordStateID TINYINT NOT NULL,
	CatalogMovementDescription NVARCHAR(100),
	CatalogMovementCreatedUserID INT NOT NULL,
    CatalogMovementCreatedUserName NVARCHAR(20) NOT NULL,
    CatalogMovementCreatedUserFullName NVARCHAR(80) NOT NULL,
    CatalogMovementCreatedDateTime DATETIME NOT NULL,  
    CatalogMovementUpdatedUserID INT, 
    CatalogMovementUpdatedUserName NVARCHAR(20),
    CatalogMovementUpdatedUserFullName NVARCHAR(80),
    CatalogMovementUpdatedDateTime DATETIME, 
    CONSTRAINT CatalogMovement_PK_CatalogMovementID PRIMARY KEY(CatalogMovementID),
    CONSTRAINT CatalogMovement_FK_CatalogVariantID FOREIGN KEY(CatalogVariantID) REFERENCES Product.CatalogVariant(CatalogVariantID),	
	CONSTRAINT CatalogMovement_FK_CatalogLotID FOREIGN KEY (CatalogLotID) REFERENCES Product.CatalogLot(CatalogLotID),
    CONSTRAINT CatalogMovement_FK_ReasonTypeID FOREIGN KEY(ReasonTypeID) REFERENCES Product.ReasonType(ReasonTypeID),
	CONSTRAINT CatalogMovement_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)),
    CONSTRAINT CatalogMovement_CHK_CatalogMovementType CHECK(CatalogMovementType IN('IN','OUT'))  
)
GO
CREATE TABLE Product.CatalogAdjustment(
    CatalogAdjustmentID INT NOT NULL IDENTITY(1,1),
    CompanyID INT NOT NULL,
    WarehouseID INT NOT NULL, --Biene de otra BD		
    CatalogAdjustmentNumber NVARCHAR(20) NOT NULL,
    CatalogAdjustmentDate DATE NOT NULL, 
    CatalogAdjustmentObservation NVARCHAR(300),
    RecordOriginID TINYINT NOT NULL,
    RecordStateID TINYINT NOT NULL,
    CatalogAdjustmentCreatedUserID INT NOT NULL,
    CatalogAdjustmentCreatedUserName NVARCHAR(20) NOT NULL,
    CatalogAdjustmentCreatedUserFullName NVARCHAR(80) NOT NULL,
    CatalogAdjustmentCreatedDateTime DATETIME NOT NULL,
    CatalogAdjustmentUpdatedUserID INT,
    CatalogAdjustmentUpdatedUserName NVARCHAR(20),
    CatalogAdjustmentUpdatedUserFullName NVARCHAR(80),
    CatalogAdjustmentUpdatedDateTime DATETIME,
    CONSTRAINT CatalogAdjustment_PK_CatalogAdjustmentID  PRIMARY KEY(CatalogAdjustmentID),
    CONSTRAINT CatalogAdjustment_CHK_RecordStateID  CHECK(RecordStateID IN (0,1,2))
)
GO
CREATE TABLE Product.CatalogAdjustmentDetail(
    CatalogAdjustmentDetailID INT NOT NULL IDENTITY(1,1),
	CompanyID INT NOT NULL,
    CatalogAdjustmentID INT NOT NULL,
    CatalogVariantID INT NOT NULL,
	CatalogLotID INT,	
    CatalogAdjustmentDetailSystemQuantity NUMERIC(12,6) NOT NULL,
    CatalogAdjustmentDetailPhysicalQuantity NUMERIC(12,6) NOT NULL, 
    CatalogAdjustmentDetailObservation NVARCHAR(200),
    RecordOriginID TINYINT NOT NULL,
    RecordStateID TINYINT NOT NULL,
    CatalogAdjustmentCreatedUserID INT NOT NULL,
    CatalogAdjustmentCreatedUserName NVARCHAR(20) NOT NULL,
    CatalogAdjustmentCreatedUserFullName NVARCHAR(80) NOT NULL,
    CatalogAdjustmentCreatedDateTime DATETIME NOT NULL,
    CatalogAdjustmentUpdatedUserID INT,
    CatalogAdjustmentUpdatedUserName NVARCHAR(20),
    CatalogAdjustmentUpdatedUserFullName NVARCHAR(80),
    CatalogAdjustmentUpdatedDateTime DATETIME,
    CONSTRAINT CatalogAdjustmentDetail_PK_CatalogAdjustmentDetailID PRIMARY KEY (CatalogAdjustmentDetailID),
    CONSTRAINT CatalogAdjustmentDetail_FK_CatalogAdjustmentID FOREIGN KEY (CatalogAdjustmentID)  REFERENCES Product.CatalogAdjustment(CatalogAdjustmentID),
    CONSTRAINT CatalogAdjustmentDetail_FK_CatalogVariantID FOREIGN KEY (CatalogVariantID) REFERENCES Product.CatalogVariant(CatalogVariantID),
	CONSTRAINT CatalogAdjustmentDetail_FK_CatalogLotID FOREIGN KEY (CatalogLotID) REFERENCES Product.CatalogLot(CatalogLotID),
    CONSTRAINT CatalogAdjustmentDetail_CHK_RecordStateID  CHECK (RecordStateID IN (0,1,2))
)
GO
CREATE TABLE Product.PriceType( 
   PriceTypeID TINYINT NOT NULL,
   PriceTypeName NVARCHAR(30) NOT NULL,
   RecordOriginID TINYINT NOT NULL,
   RecordStateID TINYINT NOT NULL,
   CONSTRAINT PriceType_PK_PriceTypeID PRIMARY KEY(PriceTypeID),
   CONSTRAINT PriceType_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))  
)
GO
CREATE TABLE Product.CatalogPrice(
    CatalogPriceID INT NOT NULL IDENTITY(1,1),
	CatalogPresentationID INT NOT NULL,
	EstablishmentID INT NOT NULL, --Biene de otra BD
	PriceTypeID TINYINT NOT NULL,
	CurrencyTypeID TINYINT NOT NULL, --Biene de otra BD
	CatalogPriceAmount NUMERIC(12,6) NOT NULL,
	CatalogPriceIsTaxIncluded BIT NOT NULL,
    RecordOriginID TINYINT NOT NULL,
    RecordStateID TINYINT NOT NULL,
    CatalogPriceCreatedUserID INT NOT NULL,
    CatalogPriceCreatedUserName NVARCHAR(20) NOT NULL,
    CatalogPriceCreatedUserFullName NVARCHAR(80) NOT NULL,
    CatalogPriceCreatedDateTime DATETIME NOT NULL,
    CatalogPriceUpdatedUserID INT,
    CatalogPriceUpdatedUserName NVARCHAR(20),
    CatalogPriceUpdatedUserFullName NVARCHAR(80),
    CatalogPriceUpdatedDateTime DATETIME,
	CONSTRAINT CatalogPrice_PK_CatalogPriceID PRIMARY KEY(CatalogPriceID),
	CONSTRAINT CatalogPrice_FK_PriceTypeID FOREIGN KEY(PriceTypeID) REFERENCES Product.PriceType(PriceTypeID),
	CONSTRAINT CatalogPrice_FK_CatalogPresentationID FOREIGN KEY(CatalogPresentationID) REFERENCES Product.CatalogPresentation(CatalogPresentationID),
	CONSTRAINT CatalogPrice_CHK_RecordStateID  CHECK (RecordStateID IN (0,1,2))
)
/*
GO
CREATE TABLE Product.GalleryType(
  GalleryTypeID TINYINT NOT NULL,
  GalleryTypeName NVARCHAR(20) NOT NULL,
  RecordStateID TINYINT NOT NULL,   
  CONSTRAINT GalleryType_PK_GalleryTypeID PRIMARY KEY(GalleryTypeID),
  CONSTRAINT GalleryType_CHK_RecordStateID CHECK(RecordStateID IN (0,1,2))
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
*/