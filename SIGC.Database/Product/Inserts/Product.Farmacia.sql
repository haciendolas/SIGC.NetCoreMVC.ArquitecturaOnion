--FARMACIA

INSERT INTO Product.CatalogType (CatalogTypeID, CatalogTypeName, StateID)
VALUES
(1, 'Producto', 1),
(2, 'Servicio', 1),
(3, 'Concepto', 1),
(4, 'Activo Fijo', 1);

INSERT INTO Product.UnitMeasure (CountryID, UnitMeasureCode, UnitMeasureName, UnitMeasureFactorConversion, StateID)
VALUES
(1, 'UND', 'Unidad', 1, 1),
(1, 'MG', 'Miligramo', 0.001, 1),
(1, 'ML', 'Mililitro', 0.001, 1),
(1, 'CAJ', 'Caja', 1, 1);

INSERT INTO Product.Category (CompanyID, CategoryName, CategorySlug, StateID)
VALUES
(1, 'Medicamentos', 'medicamentos', 1),
(1, 'Suplementos', 'suplementos', 1),
(1, 'Servicios', 'servicios', 1);

INSERT INTO Product.ActiveIngredient (ActiveIngredientName, ActiveIngredienDescription, StateID)
VALUES
('Diclofenaco', 'Anti-inflamatorio no esteroideo', 1),
('Paracetamol', 'Analgesico y antipiretico', 1),
('Ibuprofeno', 'Anti-inflamatorio y analgesico', 1);

INSERT INTO Product.PharmaceuticalForm (PharmaceuticalFormName, PharmaceuticalFormDescription, StateID)
VALUES
('Tableta', 'Tableta oral sólida', 1),
('Jarabe', 'Jarabe líquido', 1);

INSERT INTO Product.PrescriptionType (PrescriptionTypeName, PrescriptionTypeDescription, StateID)
VALUES
('Venta libre', 'Medicamento sin receta', 1),
('Con receta', 'Requiere receta médica', 1);

INSERT INTO Product.Brand (CompanyID, BrandName, StateID)
VALUES
(1, 'Genfar', 1),
(1, 'Bayer', 1),
(1, 'Pfizer', 1);

INSERT INTO Product.[Catalog] (
  CompanyID, CatalogTypeID, CategoryID, UnitMeasureID,
  CatalogSlug, CatalogName,
  PrescriptionTypeID, BrandID,
  CatalogConcentration, CatalogSanitaryRegistrationNumber,
  CatalogSalePrice, CatalogDiscount, CatalogUnitInStock, CatalogDescription, StateID)
VALUES
(1, 1, 1, 1, 'diclofenaco-50mg', 'Diclofenaco 50mg', 1, 1, '50 mg', 'REG123', 5.50, 0, 100, 'Anti-inflamatorio tableta', 1),
(1, 1, 1, 1, 'paracetamol-500mg', 'Paracetamol 500mg', 1, 2, '500 mg', 'REG456', 3.20, 0, 200, 'Analgesico y antipiretico', 1),
(1, 1, 2, 1, 'vitamina-c-500mg', 'Vitamina C 500mg', NULL, 3, '500 mg', 'REG789', 8.00, 0, 50, 'Suplemento vitamínico', 1);


INSERT INTO Product.CatalogActiveIngredient (CatalogID, ActiveIngredientID, CatalogActiveIngredientQuantity, UnitMeasureID)
VALUES
(1, 1, 50, 2),  -- Diclofenaco 50 mg
(2, 2, 500, 2); -- Paracetamol 500 mg

INSERT INTO Product.Presentation (UnitMeasureID, PharmaceuticalFormID, PresentationName, PresentationEquivalence, StateID)
VALUES
(1, 1, 'Tableta suelta', 1, 1),
(1, 1, 'Caja x 10 tabletas', 10, 1),
(1, 2, 'Botella 100 ml jarabe', 100, 1);

INSERT INTO Product.CatalogPresentationVariant (CatalogPresentationID, CatalogPresentationVariantName, StateID)
VALUES
(1, 'Default', 1),
(2, 'Default', 1),
(3, 'Default', 1),
(4, 'Default', 1),
(5, 'Default', 1);

INSERT INTO Product.CatalogStock (CatalogPresentationVariantID, EstablishmentID, CatalogStockInitialQuantity, CatalogStockCurrentQuantity, CatalogStockPhysicalQuantity, CatalogStockMinimumQuantity, CatalogStockMaximumQuantity, StateID)
VALUES
(1, 1, 100, 100, 100, 10, 500, 1),
(2, 1, 50, 50, 50, 10, 300, 1),
(3, 1, 200, 200, 200, 20, 800, 1),
(4, 1, 80, 80, 80, 10, 200, 1),
(5, 1, 25, 25, 25, 5, 100, 1);

INSERT INTO Product.PriceType (PriceTypeID, PriceTypeName, StateID)
VALUES
(1, 'Venta', 1),
(2, 'Oferta', 1),
(3, 'Mayorista', 1);

INSERT INTO Product.CatalogPresentationPrice (CatalogPresentationID, EstablishmentID, PriceTypeID, CurrencyTypeID, CatalogPresentationPriceSale, StateID)
VALUES
(1, 1, 1, 1, 5.50, 1),
(2, 1, 1, 1, 50.00, 1),
(3, 1, 1, 1, 3.20, 1),
(4, 1, 2, 1, 30.00, 1),
(5, 1, 3, 1, 7.50, 1);


