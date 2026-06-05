-- ===========================================
-- 1) Category
-- ===========================================
INSERT INTO Product.Category (CompanyID, CategoryName, CategorySlug, StateID) VALUES
(1, 'Computación', 'computacion', 1),
(1, 'Reparación de Equipos', 'reparacion-equipos', 1),
(1, 'Soporte Técnico', 'soporte-tecnico', 1);

-- ===========================================
-- 2) UnitMeasure (incluye Servicio - SUNAT ZZ)
-- ===========================================
INSERT INTO Product.UnitMeasure (CountryID, UnitMeasureCode, UnitMeasureName, UnitMeasureFactorConversion, StateID) VALUES
('PE', 'NIU', 'UNIDAD', 1.000000, 1),
('PE', 'ZZ', 'SERVICIO', NULL, 1); -- SUNAT unidad servicio (ZZ/SERV) :contentReference[oaicite:1]{index=1}

-- ===========================================
-- 3) CatalogType
-- ===========================================
INSERT INTO Product.CatalogType (CatalogTypeID, CatalogTypeName, StateID) VALUES
(1, 'PRODUCTO', 1),
(2, 'SERVICIO', 1),
(3, 'CONCEPTO', 1),
(4, 'ACTIVO FIJO', 1);

-- ===========================================
-- 4) PharmaceuticalForm (farmacia, opcional)
-- ===========================================
INSERT INTO Product.PharmaceuticalForm (PharmaceuticalFormName, PharmaceuticalFormDescription, StateID) VALUES
('Tableta', 'Tableta sólida', 1),
('Jarabe', 'Jarabe líquido', 1);

-- ===========================================
-- 5) PrescriptionType (farmacia, opcional)
-- ===========================================
INSERT INTO Product.PrescriptionType (PrescriptionTypeName, PrescriptionTypeDescription, StateID) VALUES
('Venta Libre', 'Medicamento de venta libre', 1),
('Con Receta', 'Medicamento requiere receta', 1);

-- ===========================================
-- 6) Brand
-- ===========================================
INSERT INTO Product.Brand (CompanyID, BrandName, StateID) VALUES
(1, 'Marca Deportiva', 1),
(1, 'Brand Computo', 1);

-- ===========================================
-- 7) Catalog
-- ===========================================
INSERT INTO Product.[Catalog] (CompanyID, CatalogTypeID, CategoryID, UnitMeasureID, CatalogSlug, CatalogName, PrescriptionTypeID, BrandID, CatalogSalePrice, CatalogDiscount, CatalogUnitInStock, CatalogDescription, StateID) VALUES
-- Productos físicos
(1, 1, 1, 1, 'camiseta-deportiva', 'Camiseta Deportiva', NULL, 1, 20.00, 0, 100, 'Camiseta deportiva con tallas y colores', 1),
(1, 1, 1, 1, 'pantalon-deportivo',  'Pantalón Deportivo',  NULL, 1, 45.00, 0, 50,  'Pantalón deportivo', 1),

-- Servicios
(1, 2, 2, 2, 'reparacion-computo',  'Reparación de Computadoras y Laptops', NULL, NULL, 120.00, 0, 0, 'Servicio técnico de reparación', 1),
(1, 2, 3, 2, 'soporte-tecnico-remoto', 'Soporte Técnico Remoto',      NULL, NULL, 60.00, 0, 0, 'Asistencia remota', 1);

-- ===========================================
-- 8) Presentation (tallas y servicio)
-- ===========================================
INSERT INTO Product.Presentation (UnitMeasureID, PharmaceuticalFormID, PresentationName, PresentationEquivalence, StateID) VALUES
(1, NULL, 'UNIDAD', 1.00, 1),
(1, NULL, 'PACK 6 UNIDADES', 6.00, 1),
(2, NULL, 'SERVICIO', 1.00, 1); -- unidad base para servicios

-- ===========================================
-- 9) CatalogPresentation
-- ===========================================
INSERT INTO Product.CatalogPresentation (CatalogID, PresentationID, CatalogPresentationIsDefault, CatalogPresentationEquivalence, CatalogPresentationSKU, CatalogPresentationQRCode, StateID) VALUES
-- Productos físicos
(100, 1, 1, 1.00, 'CAMDEP-UN', 'QR-CAMDEP-UN', 1),
(100, 2, 0, 6.00, 'CAMDEP-P6', 'QR-CAMDEP-P6', 1),
(101, 1, 1, 1.00, 'PANDP-UN', 'QR-PANDP-UN', 1),

-- Servicios
(200, 3, 1, 1.00, 'SERV-REP-COMP', 'QR-SERV-REP-COMP', 1),
(201, 3, 1, 1.00, 'SERV-SUP-REMO', 'QR-SERV-SUP-REMO', 1);

-- ===========================================
-- 10) CatalogPresentationVariant
-- ===========================================
INSERT INTO Product.CatalogPresentationVariant (CatalogPresentationID, CatalogPresentationVariantName) VALUES
-- Ropa (colores)
(1, 'Azul'),
(1, 'Rojo'),
(2, 'Azul'),
(2, 'Rojo'),
(3, 'Negro'),
(3, 'Gris'),

-- Servicios (modalidades/duración)
(4, '1 hora'),
(4, '2 horas'),
(5, '30 min'),
(5, '1 hora');

-- ===========================================
-- 11) CatalogStock (solo para variantes físicas)
-- ===========================================
INSERT INTO Product.CatalogStock (CatalogPresentationVariantID, EstablishmentID, CatalogStockInitialQuantity, CatalogStockCurrentQuantity, CatalogStockPhysicalQuantity, CatalogStockMinimumQuantity, CatalogStockMaximumQuantity, StateID) VALUES
(1, 1,  30, 30, 30, 5, 100, 1),
(2, 1,  20, 20, 20, 5, 100, 1),
(3, 1,  18, 18, 18, 5, 100, 1),
(4, 1,  15, 15, 15, 5, 100, 1),
(5, 1,  14, 14, 14, 5, 100, 1),
(6, 1,  10, 10, 10, 5, 100, 1);

-- ===========================================
-- 12) PriceType
-- ===========================================
INSERT INTO Product.PriceType (PriceTypeID, PriceTypeName, StateID) VALUES
(1, 'Precio Minorista', 1),
(2, 'Precio Mayorista', 1),
(3, 'Precio Promoción', 1);

-- ===========================================
-- 13) CatalogPresentationVariantPrice
-- ===========================================
INSERT INTO Product.CatalogPresentationVariantPrice (CatalogPresentationID, EstablishmentID, PriceTypeID, CurrencyTypeID, CatalogPresentationPriceSale, StateID) VALUES
-- Físicos
(1, 1, 1, 1, 20.00,   1),
(2, 1, 1, 1, 20.00,   1),
(3, 1, 1, 1, 45.00,   1),
-- Servicios (puedes repetir si no hay variantes de precio)
(4, 1, 1, 1, 120.00,  1),
(4, 1, 2, 1, 100.00,  1),
(5, 1, 1, 1, 60.00,   1),
(5, 1, 3, 1, 50.00,   1);

-- ===========================================
-- 14) CatalogConfiguration
-- ===========================================
INSERT INTO Product.CatalogConfiguration (CatalogID, EstablishmentID, CatalogConfigurationIsStockManaged, CatalogConfigurationIsAffectStock, StateID) VALUES
(100, 1, 1, 1, 1),
(101, 1, 1, 1, 1),
(200, 1, 0, 0, 1),
(201, 1, 0, 0, 1);

-- ===========================================
-- 15) CatalogTax (opcional)
-- ===========================================
INSERT INTO Product.CatalogTax (CatalogID, TaxID, CalculationTypeID, CatalogTaxValor) VALUES
(100, 1, 1, 18.00),
(200, 1, 1, 18.00);
