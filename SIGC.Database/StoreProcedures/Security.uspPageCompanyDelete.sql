/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            23/11/2025
   Description:            Permite eliminar registros en la tabla Security.PageCompany
   Execute:                 EXECUTE [Security].uspPageCompanyDelete @CompanyID=1 					   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE [Security].uspPageCompanyDelete
(  
   @CompanyID INT  
)
AS
BEGIN 
   DELETE FROM [Security].PageCompany WHERE CompanyID=@CompanyID
END