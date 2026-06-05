 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            27/09/2025
-- Description:            Permite obtener listado de paginas activas de la tabla  [Security].[Page]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Security.uspPageList 
-- ============================================================================== 
ALTER PROCEDURE Security.uspPageList
AS
BEGIN
	SET NOCOUNT ON
     
    SELECT P.PageID,P.PageParentID,P.PageHierarchy,P.PageName,P.PageIconName,P.PageOrder,
	      'PageAction'=  
	      '[' + ISNULL(STUFF((SELECT ','  + '{'+ 
								 '"PageActionID":' + CONVERT(VARCHAR(10), PA.PageActionID)+','+
								  '"PageActionName":"' +ISNULL(PA.PageActionName,'')+'",'+ 
								  '"PageActionDescription":"' +ISNULL(PA.PageActionDescription,'') +'"'+
							 '}'  
							 FROM [Security].PageAction PA										 						 
							 WHERE PA.PageID=P.PageID AND PA.StateID=1 
							 FOR XML PATH(''), TYPE
					    )
						.value(N'.[1]', N'varchar(max)'),1,1,''
					)
				,'')
		+']'
	FROM [Security].[Page] P WITH(NOLOCK) 
	WHERE P.StateID=1 

	SET NOCOUNT OFF
END