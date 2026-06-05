-- ===========================================
-- 1) Product.Category
-- ===========================================
INSERT INTO Product.Category (CompanyID, CategoryName, CategorySlug, StateID) VALUES
(1, 'Computación', 'computacion', 1),
(1, 'Reparación de Equipos', 'reparacion-equipos', 1),
(1, 'Soporte Técnico', 'soporte-tecnico', 1);

-- ===========================================
-- 2) Product.UnitMeasure (incluye servicio)
-- ===========================================
INSERT INTO Product.UnitMeasure (CountryID, UnitMeasureCode, UnitMeasureName, UnitMeasureFactorConversion, StateID) VALUES
('PE', 'NIU', 'UNIDAD', 1.000000, 1),
('PE', 'ZZ', 'SERVICIO', NULL, 1); -- SUNAT: unidad de medida para servicios :contentReference[oaicite:1]{index=1}

-- ===========================================
-- 3) Product.CatalogType
-- ===========================================
INSERT INTO Product.CatalogType (CatalogTypeID, CatalogTypeName, StateID) VALUES
(1, 'PRODUCTO', 1),
(2, 'SERVICIO', 1),
(3, 'CONCEPTO', 1),
(4, 'ACTIVO FIJO', 1);

-- ===========================================
-- 4) Product.PharmaceuticalForm (opcional)
-- ===========================================
INSERT INTO Product.PharmaceuticalForm (PharmaceuticalFormName, PharmaceuticalFormDescription, StateID) VALUES
('Tableta', 'Tableta sólida', 1),
('Jarabe', 'Jarabe líquido', 1);

-- ===========================================
-- 5) Product.PrescriptionType (opcional)
-- ===========================================
INSERT INTO Product.PrescriptionType (PrescriptionTypeName, PrescriptionTypeDescription, StateID) VALUES
('Venta Libre', 'Medicamento de venta libre', 1),
('Con Receta', 'Medicamento que requiere receta', 1);

-- ===========================================
-- 6) Product.Brand
-- ===========================================
INSERT INTO Product.Brand (CompanyID, BrandName, StateID) VALUES
(1, 'Marca Deportiva', 1),
(1, 'Brand Computo', 1);

-- ===========================================
-- 7) Product.Catalog — productos y servicios
-- ===========================================
INSERT INTO Product.[Catalog] 
(CompanyID, CatalogTypeID, CategoryID, UnitMeasureID, CatalogSlug, CatalogName, PrescriptionTypeID, BrandID, CatalogSalePrice, CatalogDiscount, CatalogUnitInStock, CatalogDescription, StateID) VALUES
-- Productos físicos
(1, 1, 1, 1, 'camiseta-deportiva',       'Camiseta Deportiva',      NULL, 1, 20.00, 0, 100, 'Camiseta deportiva con tallas y colores', 1),
(1, 1, 1, 1, 'pantalon-deportivo',       'Pantalón Deportivo',      NULL, 1, 45.00, 0, 50,  'Pantalón deportivo', 1),
-- Servicios
(1, 2, 2, 2, 'reparacion-computo',        'Reparación de Computadoras y Laptops', NULL, NULL, 120.00, 0, 0, 'Servicio técnico de reparación', 1),
(1, 2, 3, 2, 'soporte-tecnico-remoto',    'Soporte Técnico Remoto',                NULL, NULL, 60.00,  0, 0, 'Asistencia remota', 1);

-- ===========================================
-- 8) Product.Presentation — unidades/presentaciones
-- ===========================================
INSERT INTO Product.Presentation (UnitMeasureID, PharmaceuticalFormID, PresentationName, PresentationEquivalence, StateID) VALUES
(1, NULL, 'UNIDAD',           1.00, 1),
(1, NULL, 'PACK 6 UNIDADES',  6.00, 1),
(2, NULL, 'SERVICIO',         1.00, 1);

-- ===========================================
-- 9) Product.CatalogPresentation
-- (sin campos SKU/QRCode aquí)
-- ===========================================
INSERT INTO Product.CatalogPresentation (CatalogID, PresentationID, CatalogPresentationIsDefault, CatalogPresentationEquivalence, StateID) VALUES
-- Productos físicos
(100, 1, 1, 1.00, 1),
(100, 2, 0, 6.00, 1),
(101, 1, 1, 1.00, 1),
-- Servicios
(200, 3, 1, 1.00, 1),
(201, 3, 1, 1.00, 1);

-- ===========================================
-- 10) Product.CatalogPresentationVariant — con SKU/QRCode
-- ===========================================
INSERT INTO Product.CatalogPresentationVariant (CatalogPresentationID, CatalogPresentationVariantName, VariantSKU, VariantQRCode, StateID) VALUES
-- Ropa (colores)
(1, 'Azul',  'CAM-100-UN-BLUE',  'QR-CAM-100-UN-BLUE',  1),
(1, 'Rojo',  'CAM-100-UN-RED',   'QR-CAM-100-UN-RED',   1),
(2, 'Azul',  'CAM-100-P6-BLUE',  'QR-CAM-100-P6-BLUE',  1),
(2, 'Rojo',  'CAM-100-P6-RED',   'QR-CAM-100-P6-RED',   1),
(3, 'Negro', 'PAN-101-UN-BLACK','QR-PAN-101-UN-BLACK', 1),
(3, 'Gris',  'PAN-101-UN-GRAY', 'QR-PAN-101-UN-GRAY',  1),
-- Servicios (duraciones/modalidades)
(4, '1 hora',  'SRV-200-1H',   'QR-SRV-200-1H',   1),
(4, '2 horas', 'SRV-200-2H',   'QR-SRV-200-2H',   1),
(5, '30 min',  'SRV-201-30M',  'QR-SRV-201-30M',  1),
(5, '1 hora',  'SRV-201-1H',   'QR-SRV-201-1H',   1);

-- ===========================================
-- 11) Product.CatalogStock — solo para variantes físicas
-- ===========================================
INSERT INTO Product.CatalogStock 
(CatalogPresentationVariantID, EstablishmentID, CatalogStockInitialQuantity, CatalogStockCurrentQuantity, CatalogStockPhysicalQuantity, CatalogStockMinimumQuantity, CatalogStockMaximumQuantity, StateID) VALUES
(1, 1, 30, 30, 30, 5, 100, 1),
(2, 1, 20, 20, 20, 5, 100, 1),
(3, 1, 18, 18, 18, 5, 100, 1),
(4, 1, 15, 15, 15, 5, 100, 1),
(5, 1, 14, 14, 14, 5, 100, 1),
(6, 1, 10, 10, 10, 5, 100, 1);

-- ===========================================
-- 12) Product.PriceType
-- ===========================================
INSERT INTO Product.PriceType (PriceTypeID, PriceTypeName, StateID) VALUES
(1, 'Precio Minorista', 1),
(2, 'Precio Mayorista', 1),
(3, 'Precio Promoción', 1);

-- ===========================================
-- 13) Product.CatalogPresentationVariantPrice
-- ===========================================
INSERT INTO Product.CatalogPresentationVariantPrice 
(CatalogPresentationID, EstablishmentID, PriceTypeID, CurrencyTypeID, CatalogPresentationPriceSale, StateID) VALUES
-- Productos
(1, 1, 1, 1, 20.00, 1),
(2, 1, 1, 1, 20.00, 1),
(3, 1, 1, 1, 45.00, 1),
-- Servicios
(4, 1, 1, 1, 120.00, 1),
(4, 1, 2, 1, 100.00, 1),
(5, 1, 1, 1, 60.00, 1),
(5, 1, 3, 1, 50.00, 1);

-- ===========================================
-- 14) Product.CatalogConfiguration
-- ===========================================
INSERT INTO Product.CatalogConfiguration 
(CatalogID, EstablishmentID, CatalogConfigurationIsStockManaged, CatalogConfigurationIsAffectStock, StateID) VALUES
(100, 1, 1, 1, 1),
(101, 1, 1, 1, 1),
(200, 1, 0, 0, 1),
(201, 1, 0, 0, 1);

-- ===========================================
-- 15) Product.CatalogTax
-- ===========================================
INSERT INTO Product.CatalogTax (CatalogID, TaxID, CalculationTypeID, CatalogTaxValor) VALUES
(100, 1, 1, 18.00),
(200, 1, 1, 18.00);
