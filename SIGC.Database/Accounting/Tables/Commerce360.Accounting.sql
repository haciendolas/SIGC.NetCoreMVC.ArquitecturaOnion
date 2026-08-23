USE master
GO
USE [Commerce360]
GO
CREATE SCHEMA Accounting
GO
CREATE TABLE Accounting.CalculationType(
  CalculationTypeID TINYINT NOT NULL IDENTITY(1,1),
  CalculationTypeName NVARCHAR(20) NOT NULL, --Porcentaje , Monto por unidad
  RecordStateID TINYINT NOT NULL, 
  CONSTRAINT CalculationType_PK_CalculationType PRIMARY KEY(CalculationTypeID),
  CONSTRAINT CalculationType_CHK_RecordStateID CHECK(RecordStateID IN (0,1,2))
)
GO
CREATE TABLE Accounting.Tax(
   TaxID SMALLINT NOT NULL IDENTITY(1,1),
   CountryID INT NOT NULL, -- BIEN DE BD
   TaxCode VARCHAR(15) NOT NULL,
   TaxName NVARCHAR(50) NOT NULL,
   CalculationTypeID TINYINT NOT NULL,
   TaxValor NUMERIC(5,2) NOT NULL,
   TaxDescription NVARCHAR(100),
   RecordStateID TINYINT NOT NULL,
   CONSTRAINT Tax_PK_TaxID PRIMARY KEY(TaxID),
   CONSTRAINT Tax_FK_CalculationTypeID FOREIGN KEY(CalculationTypeID) REFERENCES Accounting.CalculationType(CalculationTypeID),
   CONSTRAINT Tax_CHK_RecordStateID CHECK(RecordStateID IN (0,1,2))
)