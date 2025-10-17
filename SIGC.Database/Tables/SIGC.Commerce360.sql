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
CREATE TABLE Security.Page (
  PageID INT NOT NULL IDENTITY(1,1),  
  PageParentID INT,
  PageHierarchy VARCHAR(12) NOT NULL,
  PageName VARCHAR(50) NOT NULL, 
  PageUrlName VARCHAR(50),
  PageIconName VARCHAR(1500),
  PageDescription VARCHAR(200),
  PageOrder SMALLINT NOT NULL,
  StateID SMALLINT NOT NULL DEFAULT 1,   
  PageCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
  PageCreatedUserID INT NOT NULL,
  PageUpdatedDateTime DATETIME,
  PageUpdatedUserID INT,    
  CONSTRAINT Page_PK_PageID PRIMARY KEY CLUSTERED(PageID) ,
  CONSTRAINT Page_FK_PageParentID FOREIGN KEY(PageParentID) REFERENCES Security.Page(PageID),  
  CONSTRAINT Page_CHK_StateID CHECK(StateID IN(0,1,2))   
)
GO
CREATE TABLE Security.PageAction(
	PageActionID INT IDENTITY(1,1),
	PageID INT NOT NULL,
	PageActionName VARCHAR(30) NOT NULL ,	 	
	PageActionDescription VARCHAR(50) NULL,
	StateID SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT PageAction_PK_PageActionID PRIMARY KEY CLUSTERED(PageActionID),
	CONSTRAINT PageAction_FK_PageID FOREIGN KEY(PageID) REFERENCES Security.Page(PageID),
    CONSTRAINT PageAction_CHK_StateID CHECK(StateID IN(0,1,2)),
    CONSTRAINT PageAction_UNQ_PageActionName UNIQUE(PageActionName)
) 
GO
CREATE TABLE Security.PageCompany(
  CompanyID INT NOT NULL,
  PageID INT NOT NULL,
  StateID SMALLINT NOT NULL DEFAULT 1,   
  PageCompanyCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
  PageCompanyCreatedUserID INT NOT NULL,
  PageCompanyUpdatedDateTime DATETIME,
  PageCompanyUpdatedUserID INT,
  CONSTRAINT PageCompany_PK_CompanyID_PageID PRIMARY KEY NONCLUSTERED(CompanyID,PageID),
  CONSTRAINT PageCompany_FK_CompanyID FOREIGN KEY(CompanyID) REFERENCES Security.Company(CompanyID),
  CONSTRAINT PageCompany_FK_PageID FOREIGN KEY(PageID) REFERENCES Security.Page(PageID),  
  CONSTRAINT PageCompany_CHK_StateID CHECK(StateID IN(0,1,2))
) 
GO
CREATE TABLE Security.[Role](
  RoleID INT NOT NULL IDENTITY(1,1),
  CompanyID INT NOT NULL,  
  RoleCode VARCHAR(5) NOT NULL, 
  RoleName VARCHAR(50) NOT NULL,
  RoleDescription VARCHAR(150),
  StateID SMALLINT NOT NULL DEFAULT 1,    
  RoleCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
  RoleCreatedUserID INT NOT NULL,
  RoleUpdatedDateTime DATETIME,
  RoleUpdatedUserID INT,  
  CONSTRAINT Role_PK_RoleID PRIMARY KEY CLUSTERED(RoleID),
  CONSTRAINT Role_FK_CompanyID FOREIGN KEY(CompanyID) REFERENCES Security.Company(CompanyID),   
  CONSTRAINT Role_CHK_StateID CHECK(StateID IN(0,1,2))
)  
GO 
CREATE TABLE Security.RolePermission(
	CompanyID INT NOT NULL,
	RoleID int NOT NULL,
    PageID INT NOT NULL,
	PageActionID int NOT NULL,  
	PageRoleCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT RolePermission_PK_CompanyID_RoleID_PageActionID PRIMARY KEY NONCLUSTERED(CompanyID,RoleID,PageID,PageActionID),	 
	CONSTRAINT RolePermission_FK_CompanyID_RoleID FOREIGN KEY(RoleID) REFERENCES Security.[Role](RoleID)
)  
GO
CREATE TABLE Security.UserRole(
  CompanyID INT NOT NULL,
  UserID INT NOT NULL,
  RoleID INT NOT NULL, 
  CONSTRAINT UserRole_PK_CompanyID_UserID_RoleID PRIMARY KEY NONCLUSTERED(CompanyID,UserID,RoleID),
  CONSTRAINT UserRole_FK_UserID FOREIGN KEY(UserID) REFERENCES Security.[User](UserID), 
  CONSTRAINT UserRole_FK_CompanyID_RoleID FOREIGN KEY(RoleID) REFERENCES Security.[Role](RoleID) 
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
GO
CREATE TABLE Security.Constant(  
   ConstantID SMALLINT NOT NULL,
   ConstantClass INT NOT NULL,
   ConstantAbbreviation VARCHAR(10),
   ConstantName VARCHAR(100) NOT NULL,   
   StateID SMALLINT NOT NULL DEFAULT 1,
   ConstantCreatedDateTime DATETIME NOT NULL DEFAULT GETDATE(), 
   CONSTRAINT Constant_PK_ConstantID_ConstantClass PRIMARY KEY CLUSTERED(ConstantClass,ConstantID)  ,
   CONSTRAINT Constant_CHK_StateID CHECK(StateID IN(0,1,2))
)
