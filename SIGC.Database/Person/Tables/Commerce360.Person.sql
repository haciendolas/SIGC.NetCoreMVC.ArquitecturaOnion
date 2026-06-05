CREATE SCHEMA HumanResources
GO
CREATE TABLE HumanResources.Person(
  PersonID INT NOT NULL IDENTITY(1,1),
  PersonType TINYINT NOT NULL,
  PersonFirstName VARCHAR(120) NOT NULL,
  PersonLastName VARCHAR(120) NOT NULL,
  PersonFullName AS REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONCAT(PersonLastName,' ',IIF(PersonType=1,PersonFirstName,'')),'á','a'),'é','e') ,'í','i'),'ó','o'),'ú','u'),'ñ','n') PERSISTED,
  CountryID INT NOT NULL,
  RecordOriginID TINYINT NOT NULL,
  RecordStateID TINYINT NOT NULL,
  PersonCreatedUserID INT NOT NULL,
  PersonCreatedUserName VARCHAR(20) NOT NULL,
  PersonCreatedUserFullName VARCHAR(80) NOT NULL,
  PersonCreatedDateTime DATETIME NOT NULL,  
  PersonUpdatedUserID INT, 
  PersonUpdatedUserName VARCHAR(20),
  PersonUpdatedUserFullName VARCHAR(80),
  PersonUpdatedDateTime DATETIME,
  CONSTRAINT Person_PK_PersonID PRIMARY KEY(PersonID),
  CONSTRAINT Person_CHK_RecordStateID CHECK(RecordStateID IN(0,1,2)),
  CONSTRAINT Person_CHK_PersonType CHECK(PersonType IN(1,2))
)
GO