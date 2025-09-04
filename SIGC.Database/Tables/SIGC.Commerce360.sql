CREATE DATABASE Commerce360
GO
USE Commerce360
GO
CREATE SCHEMA Security
GO
CREATE TABLE Security.Ubigeo(
  UbigeoID INT NOT NULL,
  UbigeoClass INT NOT NULL,
  UbigeoCode VARCHAR(25) NOT NULL,
  UbigeoName VARCHAR(100) NOT NULL,
  StateID SMALLINT NOT NULL DEFAULT 1,   
  UbigeoCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
  UbigeoCreatedUserID INT NOT NULL,
  UbigeoUpdatedDateTime DATETIME,
  UbigeoUpdatedUserID INT, 
  CONSTRAINT Ubigeo_PK_UbigeoID PRIMARY KEY CLUSTERED(UbigeoID) ,
  CONSTRAINT Ubigeo_CHK_StateID CHECK(StateID IN(0,1,2))  
)
GO
CREATE TABLE Security.Company(
  CompanyID INT NOT NULL IDENTITY(1,1),
  CompanyTradeName VARCHAR(100) NOT NULL,
  CompanySocialReason VARCHAR(150) NOT NULL,
  CompanyDocumentNumber VARCHAR(11) NOT NULL,
  CompanyBirthDate DATE NOT NULL DEFAULT GETDATE(),
  CountryID INT NOT NULL,
  CompanyAddress VARCHAR(200), 
  TaxpayerTypeID SMALLINT DEFAULT 2, 
  SectorID SMALLINT NOT NULL DEFAULT 3, 
  CompanyCorporateEmail VARCHAR(150),
  CompanyMobile VARCHAR(15),
  CompanyPhone VARCHAR(15),
  CompanyLogo VARCHAR(100),
  StateID SMALLINT NOT NULL DEFAULT 1,  
  CompanyCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
  CompanyCreatedUserID INT NOT NULL,
  CompanyUpdatedDateTime DATETIME,
  CompanyUpdatedUserID INT,  
  CONSTRAINT Company_PK_CompanyID PRIMARY KEY CLUSTERED(CompanyID), 
  CONSTRAINT Company_FK_CountryID FOREIGN KEY(CountryID) REFERENCES Security.Ubigeo(UbigeoID),  
  CONSTRAINT Company_CHK_StateID CHECK(StateID IN(0,1,2))
) 
GO

CREATE TABLE Security.CompanyRegister(
   CompanyIDRegister   INT NOT NULL,
   CompanyID INT NOT NULL,
   CompanyRegisterCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
   CompanyRegisterCreatedUserID INT NOT NULL,
   CONSTRAINT CompanyRegister_PK_CompanyIDRegister_CompanyID PRIMARY KEY(CompanyIDRegister,CompanyID),
   CONSTRAINT CompanyRegister_FK_CompanyID FOREIGN KEY(CompanyID) REFERENCES Security.Company(CompanyID)
)
GO
CREATE TABLE Security.[User](  
  UserID INT NOT NULL IDENTITY(1,1),   
  UserFirstName VARCHAR(50) NULL,
  UserLastName VARCHAR(30) NULL,
  UserName varchar(15) NOT NULL,
  UserPassword varchar(10) NOT NULL,
  UserMail VARCHAR(100),
  UserPhoto VARCHAR(100),
  StateID SMALLINT NOT NULL DEFAULT 1,  
  UserCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
  UserCreatedUserID INT NOT NULL,
  UserUpdatedDateTime DATETIME,
  UserUpdatedUserID INT,  
  CONSTRAINT User_PK_UserID PRIMARY KEY CLUSTERED(UserID),  
  CONSTRAINT User_CHK_StateID CHECK(StateID IN(0,1,2))
) 
GO
CREATE TABLE Security.UserCompany(
  CompanyID INT NOT NULL,
  UserID INT NOT NULL, 
  StateID SMALLINT NOT NULL DEFAULT 1,   
  UserCompanyCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
  UserCompanyCreatedUserID INT NOT NULL,
  UserCompanyUpdatedDateTime DATETIME NULL,
  UserCompanyUpdatedUserID INT,  
  CONSTRAINT Usercompany_PK_CompanyID_UserID PRIMARY KEY NONCLUSTERED(CompanyID,UserID),
  CONSTRAINT Usercompany_FK_CompanyID FOREIGN KEY(CompanyID) REFERENCES Security.Company(CompanyID),
  CONSTRAINT Usercompany_FK_UserID FOREIGN KEY(UserID) REFERENCES Security.[User](UserID),   
  CONSTRAINT Usercompany_CHK_StateID CHECK(StateID IN(0,1,2))
) 
GO
CREATE  TABLE Security.Token(
   TokenID INT NOT NULL IDENTITY(1,1),
   CompanyID INT NOT NULL,
   UserID INT NOT NULL,   
   TokenSessionJson NVARCHAR(max),
   TokenRefreshRandom VARCHAR(100),
   TokenAccessJWT VARCHAR(100),
   TokenCreateDateTime DATETIME NOT NULL DEFAULT GETDATE(),
   TokenExpirationRandomDateTime DATETIME NOT NULL,
   TokenExpirationJWTDateTime DATETIME NOT NULL,
   TokenRevocationDateTime DATETIME ,
   StateID AS (IIf(TokenRevocationDateTime IS NULL,1 ,0)), 
   CONSTRAINT Token_PK_TokenID PRIMARY KEY(TokenID),
   CONSTRAINT Token_FK_UserID FOREIGN KEY(UserID) REFERENCES Security.[User](UserID)
)