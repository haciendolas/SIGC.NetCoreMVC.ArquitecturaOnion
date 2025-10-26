 /*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            23/02/2025
   Description:            Permite listar registros de la tabla [Security].[Constant] por ConstantClass 
   Execute:                Exec [Security].uspConstantList @ConstantClass='1030,1001' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     
==============================================================================*/
ALTER PROCEDURE [Security].uspConstantList
  @ConstantClass VARCHAR(49) --MAXIMO 10 C0NSTANTES
AS
BEGIN
  SET NOCOUNT ON
    SELECT C.ConstantID,C.ConstantClass,C.ConstantAbbreviation,C.ConstantName
	     FROM [Security].[Constant] C WITH(NOLOCK) WHERE C.StateID=1
		 AND ConstantClass IN(SELECT * FROM Globals.fnConvertToTable(@ConstantClass,','))
END