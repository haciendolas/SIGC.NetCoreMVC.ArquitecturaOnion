INSERT INTO Product.ActiveIngredient
(
    ActiveIngredientName,
    ActiveIngredientDescription,
    RecordOriginID,
    RecordStateID,
    ActiveIngredientCreatedUserID,
    ActiveIngredientCreatedUserName,
    ActiveIngredientCreatedUserFullName,
    ActiveIngredientCreatedDateTime
)
VALUES

-- =========================
-- ANALGÉSICOS / ANTIINFLAMATORIOS
-- =========================
('Paracetamol','Analgésico y antipirético',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ibuprofeno','AINE analgésico antiinflamatorio',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Diclofenaco','AINE antiinflamatorio',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Naproxeno','AINE',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ketorolaco','Analgésico potente AINE',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Meloxicam','AINE selectivo COX-2',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Indometacina','AINE potente',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Piroxicam','AINE',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Tramadol','Analgésico opioide',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Morfina','Opioide fuerte',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- ANTIBIÓTICOS
-- =========================
('Amoxicilina','Penicilina antibiótico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ampicilina','Antibiótico beta lactámico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Azitromicina','Macrólido',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Claritromicina','Macrólido',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ciprofloxacino','Quinolona',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Levofloxacino','Quinolona',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Metronidazol','Antibacteriano/antiparasitario',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Clindamicina','Lincosamida',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Cefalexina','Cefalosporina',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ceftriaxona','Cefalosporina 3ra generación',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- ANTIHIPERTENSIVOS
-- =========================
('Losartan','ARA II',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Enalapril','IECA',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Captopril','IECA',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Amlodipino','Calcioantagonista',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Nifedipino','Calcioantagonista',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Valsartan','ARA II',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Telmisartan','ARA II',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Hydralazina','Vasodilatador',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- DIURÉTICOS
-- =========================
('Furosemida','Diurético de asa',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Hidroclorotiazida','Tiazida',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Espironolactona','Ahorrador de potasio',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Bumetanida','Diurético de asa',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- ANTIDIABÉTICOS
-- =========================
('Metformina','Biguanida',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Glibenclamida','Sulfonilurea',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Glimepirida','Sulfonilurea',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Sitagliptina','Inhibidor DPP-4',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Empagliflozina','Inhibidor SGLT2',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- GASTROINTESTINALES
-- =========================
('Omeprazol','IBP',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Pantoprazol','IBP',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Lansoprazol','IBP',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ranitidina','Antagonista H2',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Domperidona','Antiemético',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Metoclopramida','Antiemético',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- ANTIHISTAMÍNICOS
-- =========================
('Loratadina','Antihistamínico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Cetirizina','Antihistamínico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Clorfenamina','Antihistamínico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Difenhidramina','Antihistamínico sedante',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- RESPIRATORIO
-- =========================
('Salbutamol','Broncodilatador',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Budesonida','Corticoide inhalado',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Formoterol','Broncodilatador LABA',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ipratropio','Anticolinérgico respiratorio',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- CORTICOIDES
-- =========================
('Prednisona','Corticoide',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Dexametasona','Corticoide potente',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Hidrocortisona','Corticoide',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- CARDIOVASCULAR / OTROS
-- =========================
('Atorvastatina','Hipolipemiante',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Simvastatina','Hipolipemiante',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Clopidogrel','Antiagregante plaquetario',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Aspirina','Antiagregante/analgésico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Warfarina','Anticoagulante',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Heparina','Anticoagulante inyectable',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- ANTIINFECCIOSOS OTROS
-- =========================
('Fluconazol','Antifúngico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ketoconazol','Antifúngico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Nistatina','Antifúngico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Albendazol','Antiparasitario',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Ivermectina','Antiparasitario',1,1,1,'administrador','Joel Castillo',GETDATE()),

-- =========================
-- SISTEMA NERVIOSO
-- =========================
('Diazepam','Benzodiacepina',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Alprazolam','Ansiolítico',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Clonazepam','Anticonvulsivo',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Sertralina','Antidepresivo ISRS',1,1,1,'administrador','Joel Castillo',GETDATE()),
('Fluoxetina','Antidepresivo ISRS',1,1,1,'administrador','Joel Castillo',GETDATE());