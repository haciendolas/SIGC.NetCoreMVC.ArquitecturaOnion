/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            20/06/2026
   Description:            Permite crear un registro en la tabla [Security].Audit
   Execute:
 
		  DECLARE @AuditID INT  
		  EXECUTE [Security].uspAuditCreate 
			@AuditID=@AuditID OUTPUT,
			@CompanyID=1,
			@OperationType='Create',
			@TableName='[Security].Role',
			@RecordOriginID = 1,
			@DateTime='2025-09-02 11:00',
			@OldValues= NULL,
			@NewValues =  '"{"RoleName":"Administrador"}"',
			@PrimaryKey = '{"Id":2}',
			@AffectedColumns ='["RoleName"]',
			@UserID = 1,
			@UserName = 'Administrador',
			@UserFullName = 'Joel Rolando Castillo Rojas'	 
		  SELECT @AuditID 	   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE [Security].uspAuditCreate
(  @AuditID INT OUTPUT,
   @CompanyID INT,
   @OperationType VARCHAR(20),
   @TableName VARCHAR(100),
   @RecordOriginID TINYINT,
   @DateTime datetime2(7),
   @OldValues NVARCHAR(max),
   @NewValues NVARCHAR(max),
   @PrimaryKey NVARCHAR(max),
   @AffectedColumns NVARCHAR(max) = null,
   @UserID INT,
   @UserName NVARCHAR(20),
   @UserFullName NVARCHAR(80)
)
AS
BEGIN 
  INSERT INTO [Security].Audit(
   CompanyID,
   OperationType,
   TableName,
   RecordOriginID,
   [DateTime],
   OldValues,
   NewValues,
   PrimaryKey,
   AffectedColumns,
   UserID,
   UserName,
   UserFullName
  )
  VALUES(
   @CompanyID,
   @OperationType,
   @TableName,
   @RecordOriginID,
   @DateTime,
   @OldValues,
   @NewValues,
   @PrimaryKey,
   @AffectedColumns,
   @UserID,
   @UserName,
   @UserFullName)
 SET @AuditID = SCOPE_IDENTITY()
END