 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            04/10/2025
-- Description:            Permite obtener un rol apartir de su RoleID tabla [Security].[Role]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec [Security].uspRoleGet  @RoleID=5
-- ============================================================================== 
ALTER PROCEDURE [Security].uspRoleGet(
   @RoleID INT 
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT R.RoleID,R.RoleCode,R.RoleName,R.RoleDescription,R.StateID,
		'RolePermission'=  
			  '[' + ISNULL(STUFF((SELECT ','  + '{'+ 
			                         '"PageID":' + CONVERT(VARCHAR(10), RP.PageID)+','+
									 '"PageActionID":' + CONVERT(VARCHAR(10), RP.PageActionID)+''+
								 '}'  
								 FROM [Security].RolePermission RP WITH(NOLOCK)							 						 
								 WHERE RP.RoleID=R.RoleID AND RP.CompanyID=R.CompanyID								      
								 FOR XML PATH(''), TYPE
							)
							.value(N'.[1]', N'varchar(max)'),1,1,''
						)
					,'')
			+']'
		FROM [Security].[Role] R WITH(NOLOCK)		 
		WHERE R.RoleID=@RoleID 
	SET NOCOUNT OFF
END