USE master
GO
USE [Commerce360]
GO
CREATE SCHEMA Accounting
GO
CREATE TABLE Accounting.CalculationType(
  CalculationTypeID TINYINT NOT NULL IDENTITY(1,1),
  CalculationTypeName VARCHAR(20) NOT NULL, --Porcentaje , Monto por unidad
  StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
  CONSTRAINT CalculationType_PK_CalculationType PRIMARY KEY(CalculationTypeID)
)
GO
CREATE TABLE Accounting.Tax(
   TaxID SMALLINT NOT NULL IDENTITY(1,1),
   CountryID INT NOT NULL, -- BIEN DE BD
   TaxCode VARCHAR(15) NOT NULL,
   TaxName VARCHAR(50) NOT NULL,
   CalculationTypeID TINYINT NOT NULL,
   TaxValor NUMERIC(5,2) NOT NULL,
   TaxDescription VARCHAR(100),
   StateID TINYINT NOT NULL DEFAULT 1 CHECK(StateID IN(0,1,2)),
   CONSTRAINT Tax_PK_TaxID PRIMARY KEY(TaxID),
   CONSTRAINT Tax_FK_CalculationTypeID FOREIGN KEY(CalculationTypeID) REFERENCES Accounting.CalculationType(CalculationTypeID)
)