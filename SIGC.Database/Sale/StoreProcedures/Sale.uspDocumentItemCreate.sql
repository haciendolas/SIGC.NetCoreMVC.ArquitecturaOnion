/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            27/11/2025
   Description:            Permite crear un registro en la tabla Sale.DocumentItem
   Execute:
		 
		  DECLARE @DocumentItemID BIGINT  
		  EXECUTE Sale.uspDocumentItemCreate 
			@DocumentItemID=@DocumentItemID OUTPUT,
			@DocumentID=1
			@DocumentItemRow=1
			@CatalogID=1,	
			@CatalogName='COCACOLA,	
			@DocumentItemAdditionalInformation='Se agrego este producto adicional'	 
			@DocumentItemSalePrice = 100.00,
			@DocumentItemBasePrice = 100.00,
			@DocumentItemQuantity = 1,
			@DocumentItemWeight = NULL,
			@DiscountTypeID = NULL,
			@DocumentItemDiscountValue = NULL,
			@DocumentItemDiscountAmount = NULL,
			@DocumentItemSubTotalNet = 100.00,
			@DocumentItemTaxRate = 18.00, // PORCENTAJE
			@DocumentItemTaxAmount = 18.00
            @DocumentItemTotalAmount = 118.00
		  SELECT @DocumentItemID						   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1 DocumentItemID
==============================================================================*/
ALTER PROCEDURE Sale.uspDocumentItemCreate
(  @DocumentItemID BIGINT OUTPUT,
   @DocumentID BIGINT,
   @DocumentItemRow SMALLINT,
   @CatalogID INT,
   @CatalogName VARCHAR(200),    
   @DocumentItemAdditionalInformation VARCHAR(300),
   @DocumentItemSalePrice NUMERIC(12,6),
   @DocumentItemBasePrice NUMERIC(12,6),
   @DocumentItemQuantity NUMERIC(12,6),
   @DocumentItemWeight NUMERIC(10,3),
   @DiscountTypeID SMALLINT,
   @DocumentItemDiscountValue NUMERIC(5,2),
   @DocumentItemDiscountAmount NUMERIC(12,6),
   @DocumentItemSubTotalNet NUMERIC(12,6),
   @DocumentItemTaxRate NUMERIC(5,3),
   @DocumentItemTaxAmount NUMERIC(12,6),
   @DocumentItemTotalAmount NUMERIC(12,6)
)
AS
BEGIN 
  INSERT INTO Sale.DocumentItem(DocumentID,DocumentItemRow,CatalogID,CatalogName,DocumentItemAdditionalInformation,
     DocumentItemSalePrice,DocumentItemBasePrice,DocumentItemQuantity,DocumentItemWeight,DiscountTypeID,
	 DocumentItemDiscountValue,DocumentItemDiscountAmount,DocumentItemSubTotalNet,DocumentItemTaxRate,
	 DocumentItemTaxAmount,DocumentItemTotalAmount)
  VALUES(@DocumentID,@DocumentItemRow,@CatalogID,@CatalogName,@DocumentItemAdditionalInformation,
     @DocumentItemSalePrice,@DocumentItemBasePrice,@DocumentItemQuantity,@DocumentItemWeight,@DiscountTypeID,
	 @DocumentItemDiscountValue,@DocumentItemDiscountAmount,@DocumentItemSubTotalNet,@DocumentItemTaxRate,
	 @DocumentItemTaxAmount,@DocumentItemTotalAmount)

 SET @DocumentItemID = SCOPE_IDENTITY()
END