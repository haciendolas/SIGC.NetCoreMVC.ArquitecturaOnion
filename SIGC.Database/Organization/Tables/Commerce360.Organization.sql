CREATE SCHEMA Organization

GO
-- NOTA 
--- Aqui debe ir company y companyRegistrer
CREATE Table Organization.Establishment(  
   EstablishmentID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,  
   PersonID INT NOT NULL,
   TypeID TINYINT NOT NULL,
   EstablishmentCode VARCHAR(10) NOT NULL,
   EstablishmentName VARCHAR(50) NOT NULL,
   EstablishmentAddress VARCHAR(150) NOT NULL,  
   EstablishmentLogo VARCHAR(100),
   RecordOriginID TINYINT NOT NULL, --WebPForm=1
   RecordStateID TINYINT NOT NULL,
   EstablishmentCreatedUserID INT NOT NULL,
   EstablishmentCreatedUserName VARCHAR(20) NOT NULL,
   EstablishmentCreatedUserFullName VARCHAR(80) NOT NULL,
   EstablishmentCreatedDateTime DATETIME NOT NULL,  
   EstablishmentUpdatedUserID INT, 
   EstablishmentUpdatedUserName VARCHAR(20),
   EstablishmentUpdatedUserFullName VARCHAR(80),
   EstablishmentUpdatedDateTime DATETIME,
   CONSTRAINT Establishment_PK_EstablishmentID PRIMARY KEY(EstablishmentID),
   CONSTRAINT Establishment_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2))
)
GO
CREATE Table Organization.Warehouse(  
   WarehouseID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,  
   EstablishmentID INT NOT NULL,  
   WarehouseTypeID TINYINT NOT NULL,
   WarehouseCode VARCHAR(10) NOT NULL,
   WarehouseName NVARCHAR(50) NOT NULL,  
   WarehouseAddress NVARCHAR(150) NULL,  
   RecordOriginID TINYINT NOT NULL, --WebPForm=1
   RecordStateID TINYINT NOT NULL,
   WarehouseCreatedUserID INT NOT NULL,
   WarehouseCreatedUserName NVARCHAR(20) NOT NULL,
   WarehouseCreatedUserFullName NVARCHAR(80) NOT NULL,
   WarehouseCreatedDateTime DATETIME NOT NULL,  
   WarehouseUpdatedUserID INT, 
   WarehouseUpdatedUserName NVARCHAR(20),
   WarehouseUpdatedUserFullName NVARCHAR(80),
   WarehouseUpdatedDateTime DATETIME,
   CONSTRAINT Warehouse_PK_WarehouseID PRIMARY KEY(WarehouseID),
   CONSTRAINT Warehouse_FK_EstablishmentID FOREIGN KEY(EstablishmentID) REFERENCES Organization.Establishment(EstablishmentID),
   CONSTRAINT Warehouse_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)),
   CONSTRAINT Warehouse_CHK_WarehouseTypeID CHECK(WarehouseTypeID IN(1,2)) --1 = Interno, 2 = Externo
)
GO