INSERT INTO Product.TherapeuticAction
(
    TherapeuticActionName,
    TherapeuticActionDescription,
    RecordOriginID,
    RecordStateID,
    TherapeuticActionCreatedUserID,
    TherapeuticActionCreatedUserName,
    TherapeuticActionCreatedUserFullName,
    TherapeuticActionCreatedDateTime
)
VALUES

-- =========================
-- ANALGÉSICOS / ANTIINFLAMATORIOS
-- =========================
('Analgésico','Alivia el dolor',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antiinflamatorio','Reduce inflamación',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antipirético','Reduce la fiebre',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Analgésico opioide','Actúa sobre receptores opioides',1,1,1,'administrador','Joel Castillo',GETDATE()),
('AINE','Antiinflamatorio no esteroideo',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- ANTIINFECCIOSOS
-- =========================
('Antibiótico','Elimina bacterias',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antiviral','Inhibe virus',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antifúngico','Elimina hongos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antiparasitario','Elimina parásitos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antibacteriano','Actúa contra bacterias',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- CARDIOVASCULAR
-- =========================
('Antihipertensivo','Reduce presión arterial',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Vasodilatador','Dilata vasos sanguíneos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Anticoagulante','Evita formación de coágulos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antiagregante plaquetario','Inhibe agregación de plaquetas',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Hipolipemiante','Reduce colesterol y triglicéridos',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- METABÓLICO
-- =========================
('Antidiabético','Controla glucosa en sangre',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Hipoglucemiante','Reduce nivel de azúcar',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- RESPIRATORIO
-- =========================
('Broncodilatador','Dilata vías respiratorias',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antitusivo','Suprime la tos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Mucolítico','Disuelve secreciones bronquiales',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Expectorante','Facilita expulsión de flema',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- GASTROINTESTINAL
-- =========================
('Antiácido','Neutraliza ácido gástrico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antiemético','Controla náuseas y vómitos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antiespasmódico','Reduce espasmos musculares',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Gastroprotector','Protege mucosa gástrica',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- SISTEMA NERVIOSO
-- =========================
('Ansiolítico','Reduce ansiedad',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Antidepresivo','Mejora estado de ánimo',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Sedante','Induce relajación o sueño',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Anticonvulsivo','Controla convulsiones',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- ANTIHISTAMÍNICOS / INMUNO
-- =========================
('Antihistamínico','Reduce alergias',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Inmunosupresor','Reduce actividad inmune',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- HORMONAL
-- =========================
('Hormonal','Reemplazo hormonal',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Corticoide','Reduce inflamación e inmunidad',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- OFTALMOLÓGICO / DERMATOLÓGICO
-- =========================
('Antiséptico','Elimina microorganismos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Dermatológico','Uso en piel',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Oftálmico','Uso en ojos',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- OTROS IMPORTANTES
-- =========================
('Anestésico local','Bloquea dolor local',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Anestésico general','Induce pérdida de conciencia',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Diurético','Elimina exceso de líquidos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Relajante muscular','Reduce tensión muscular',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Regenerador celular','Favorece reparación de tejidos',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Suplemento','Apoyo nutricional o metabólico',1,1,1,'administrador','Joel Castillo',GETDATE());