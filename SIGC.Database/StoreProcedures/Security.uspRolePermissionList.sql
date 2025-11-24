 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            10/00/2025
-- Description:            Permite obtener los permisos del usuario apartir de su UserID y @companyID tabla Security.RolePermission
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspRolePermissionList  @UserID=1, @companyID=1
-- ============================================================================== 
ALTER PROCEDURE Security.uspRolePermissionList(
   @UserID INT,
   @companyID INT
)
AS
BEGIN
	SET NOCOUNT ON
     
	 DECLARE @PermissionTemp TABLE(ID INT IDENTITY(1,1),PageID INT, PageHierarchy VARCHAR(12))

	 INSERT INTO @PermissionTemp(PageID,PageHierarchy)
	 SELECT DISTINCT P.PageID,P.PageHierarchy FROM  [Security].[RolePermission] RP WITH(NOLOCK)
	 INNER JOIN [Security].PageCompany PC WITH(NOLOCK) ON RP.CompanyID=PC.CompanyID AND RP.PageID=PC.PageID AND PC.StateID=1
	 INNER JOIN Security.Page P WITH(NOLOCK) ON RP.PageID=P.PageID AND P.StateID=1
	 INNER JOIN Security.UserRole UR WITH(NOLOCK) ON RP.CompanyID=UR.CompanyID AND RP.RoleID=UR.RoleID
	 WHERE RP.CompanyID=@companyID AND UR.UserID=@UserID

	 DECLARE @IDMin INT=1,@IDMax INT =(SELECT MAX(ID) FROM @PermissionTemp)
	 DECLARE @PageHierarchy VARCHAR(13)='' 

	 DECLARE @PageTemp TABLE(
		PageID INT,
		PageParentID INT,
		PageHierarchy VARCHAR(12),
		PageName VARCHAR(50),
		PageUrlName VARCHAR(50),
		PageIconName VARCHAR(1500),
		PageOrder SMALLINT	 
	 )
	 WHILE @IDMax>=@IDMin
	  BEGIN
	      SELECT @PageHierarchy=PageHierarchy FROM @PermissionTemp WHERE Id=@IDMin

		  INSERT INTO @PageTemp(PageID,PageParentID,PageHierarchy,PageName,PageUrlName,PageIconName,PageOrder)
		  SELECT P.PageID,P.PageParentID,P.PageHierarchy,P.PageName,P.PageUrlName,P.PageIconName,PageOrder FROM Security.Page P WITH(NOLOCK) 
		  WHERE P.PageHierarchy = SUBSTRING(@PageHierarchy,1,LEN(P.PageHierarchy))
		  AND P.StateID=1
		   AND NOT EXISTS(SELECT 1 FROM @PageTemp PT WHERE PT.PageHierarchy=P.PageHierarchy)
		  SET @IDMin+=1
	  END

	 SELECT * FROM @PageTemp

	SET NOCOUNT OFF
END