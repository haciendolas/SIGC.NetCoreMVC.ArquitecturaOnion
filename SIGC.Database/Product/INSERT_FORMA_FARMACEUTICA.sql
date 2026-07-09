INSERT INTO Product.PharmaceuticalForm
(
    PharmaceuticalFormName,
    PharmaceuticalFormDescription,
    RecordOriginID,
    RecordStateID,
    PharmaceuticalFormCreatedUserID,
    PharmaceuticalFormCreatedUserName,
    PharmaceuticalFormCreatedUserFullName,
    PharmaceuticalFormCreatedDateTime
)
VALUES

-- =========================
-- SÓLIDAS ORALES
-- =========================
('Tableta', 'Forma sólida comprimida de administración oral', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Tableta recubierta', 'Tableta con recubrimiento protector', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Tableta masticable', 'Tableta para masticación', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Tableta sublingual', 'Se disuelve bajo la lengua', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Tableta efervescente', 'Se disuelve en agua antes de consumo', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Comprimido', 'Forma sólida compactada', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Cápsula dura', 'Cubierta rígida de gelatina', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Cápsula blanda', 'Cápsula gelatinosa flexible', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Gragea', 'Tableta recubierta azucarada', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Pastilla', 'Forma sólida pequeña', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Polvo oral', 'Forma pulverizada para consumo', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Granulado', 'Partículas sólidas para disolver', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Pellets', 'Microesferas de liberación controlada', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),

-- =========================
-- LÍQUIDAS ORALES
-- =========================
('Jarabe', 'Solución azucarada oral', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Solución oral', 'Líquido homogéneo para ingestión', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Suspensión oral', 'Partículas no disueltas en líquido', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Emulsión oral', 'Mezcla de líquidos no miscibles', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Elixir', 'Solución alcohólica medicinal', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Gotas orales', 'Dosis líquida en gotas', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Concentrado oral', 'Preparado concentrado para diluir', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),

-- =========================
-- INYECTABLES
-- =========================
('Solución inyectable', 'Preparación líquida estéril', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Suspensión inyectable', 'Partículas en medio líquido estéril', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Emulsión inyectable', 'Líquidos no miscibles para inyección', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Polvo para inyección', 'Reconstituible antes de uso', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Liofilizado inyectable', 'Polvo deshidratado para reconstitución', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Infusión intravenosa', 'Administración IV continua', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Concentrado para infusión', 'Debe diluirse antes de uso', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Implante inyectable', 'Liberación prolongada subcutánea', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),

-- =========================
-- TÓPICAS
-- =========================
('Crema', 'Preparación semisólida para piel', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Gel', 'Forma gelatinosa de aplicación tópica', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Ungüento', 'Base oleosa para piel', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Pomada', 'Preparación grasa medicinal', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Loción', 'Solución líquida para piel', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Espuma tópica', 'Preparación en forma de espuma', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Solución tópica', 'Líquido para aplicación externa', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Aerosol cutáneo', 'Spray para piel', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Champú medicinal', 'Uso dermatológico capilar', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Pasta dermatológica', 'Base espesa para piel', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),

-- =========================
-- OFTÁLMICAS / ÓTICAS
-- =========================
('Gotas oftálmicas', 'Aplicación ocular', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Ungüento oftálmico', 'Aplicación ocular semisólida', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Solución oftálmica', 'Líquido estéril para ojos', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Gotas óticas', 'Aplicación en oído', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Solución ótica', 'Preparado líquido para oído', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),

-- =========================
-- NASALES
-- =========================
('Spray nasal', 'Nebulización nasal', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Gotas nasales', 'Aplicación en cavidad nasal', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Solución nasal', 'Líquido para uso nasal', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Gel nasal', 'Aplicación semisólida nasal', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),

-- =========================
-- RECTALES / VAGINALES
-- =========================
('Supositorio', 'Administración rectal sólida', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Óvulo', 'Administración vaginal sólida', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Enema', 'Líquido rectal', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Espuma rectal', 'Administración en espuma', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Gel vaginal', 'Aplicación vaginal semisólida', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Tableta vaginal', 'Forma sólida vaginal', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),

-- =========================
-- INHALATORIAS
-- =========================
('Aerosol inhalador', 'Spray pulmonar', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Inhalador presurizado', 'Dispositivo dosificado', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Polvo para inhalación', 'Polvo respirable', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Nebulización', 'Solución para nebulizador', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),

-- =========================
-- TRANSDÉRMICAS / ESPECIALES
-- =========================
('Parche transdérmico', 'Liberación a través de la piel', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Sistema terapéutico transdérmico', 'Liberación controlada continua', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE()),
('Película transdérmica', 'Capa delgada medicada', 1, 1, 1, 'administrador', 'Joel Castillo', GETDATE());