	 ---- TABLA 1030 : TIPO CONTRIBUYENTE -------
  INSERT INTO [Security].Constant
        (ConstantClass,ConstantID,ConstantName,StateID,ConstantCreatedDateTime)
  VALUES  (1030,0,'TABLA TIPO CONTRIBUYENTE (para el campo nPerJurTipo en PerJuridica)',1,GETDATE()),          
		  (1030,1,'PERSONA NATURAL SIN NEGOCIO',1,GETDATE()),
		  (1030,2,'PERSONA NATURAL CON NEGOCIO',1,GETDATE()),
		  (1030,3,'SOCIEDAD CONYUGAL SIN NEGOCIO',1,GETDATE()),
		  (1030,4,'SOCIEDAD CONYUGAL CON NEGOCIO',1,GETDATE()),
		  (1030,5,'SUCESION INDIVISA SIN NEGOCIO',1,GETDATE()),
	      (1030,6,'SUCESION INDIVISA CON NEGOCIO',1,GETDATE()),
		  (1030,7,'EMPRESA INDIVIDUAL DE RESP. LTDA',1,GETDATE()),
		  (1030,8,'SOCIEDAD CIVIL',1,GETDATE()),
		  (1030,9,'SOCIEDAD IRREGULAR',1,GETDATE()),
		  (1030,10,'ASOCIACION EN PARTICIPACION',1,GETDATE()),
		  (1030,11,'ASOCIACION',1,GETDATE()),
		  (1030,12,'FUNDACION',1,GETDATE()),
		  (1030,13,'SOCIEDAD EN COMANDITA SIMPLE',1,GETDATE()),
		  (1030,14,'SOCIEDAD COLECTIVA',1,GETDATE()),
		  (1030,15,'INSTITUCIONES PUBLICAS',1,GETDATE()),
		  (1030,16,'INSTITUCIONES RELIGIOSAS',1,GETDATE()),
		  (1030,17,'SOCIEDAD DE BENEFICIENCIA',1,GETDATE()),
		  (1030,18,'ENTIDADES DE AUXILIO MUTUO',1,GETDATE()),
		  (1030,19,'UNIVERS. CENTROS EDUCAT. Y CULT.',1,GETDATE()),
		  (1030,20,'GOBIERNO REGIONAL, LOCAL',1,GETDATE()),
		  (1030,21,'GOBIERNO CENTRAL',1,GETDATE()),
		  (1030,22,'COMUNIDAD LABORAL',1,GETDATE()),
		  (1030,23,'COMUNIDAD CAMPESINA,NATIVA,COMUNAL',1,GETDATE()),
          (1030,24,'COOPERATIVAS, SAIS, CAPS',1,GETDATE()),
		  (1030,25,'EMPRESA DE PROPIEDAD SOCIAL',1,GETDATE()),
		  (1030,26,'SOCIEDAD ANONIMA',1,GETDATE()),
		  (1030,27,'SOCIEDAD EN COMANDITA POR ACCIONES',1,GETDATE()),
		  (1030,28,'SOC.COM.RESPONS. LTDA',1,GETDATE()),
		  (1030,29,'SUC,AG.EMP.EXTRANJ,EST.PERM NO DOM.',1,GETDATE()),
		  (1030,30,'EMPRESA DE DERECHO PUBLICO',1,GETDATE()),
		  (1030,31,'EMPRESA ESTATAL DE DERECHO PRIVADO',1,GETDATE()),
		  (1030,32,'EMPRESA DE ECONOMIA MIXTA',1,GETDATE()),
		  (1030,33,'ACCIONARIADO DEL ESTADO',1,GETDATE()),
		  (1030,34,'MISIONES DIPLOMATICAS Y ORG. INTER.',1,GETDATE()),
		  (1030,35,'JUNTA DE PROPIETARIOS',1,GETDATE()),
		  (1030,36,'OF.REPRESENTACION DE NO DOMICILIADO',1,GETDATE()),
		  (1030,37,'FONDOS MUTUOS DE INVERSION',1,GETDATE()),
		  (1030,38,'SOCIEDAD ANONIMA ABIERTA',1,GETDATE()),
		  (1030,39,'SOCIEDAD ANONIMA CERRADA',1,GETDATE()),
		  (1030,40,'CONTRATOS COLABORACION EMPRESARIAL',1,GETDATE()),
		  (1030,41,'ENT.INST.COOPERAC.TECNICA - ENIEX',1,GETDATE()),
		  (1030,42,'COMUNIDAD DE BIENES',1,GETDATE()),
		  (1030,43,'SOCIEDAD MINERA DE RESP.LIMITADA',1,GETDATE()),
		  (1030,44,'ASOC. FUNDAC. Y COMITE NO INSCRITOS',1,GETDATE()),
		  (1030,45,'PARTIDOS,MOVIM, ALIANZAS POLITICAS',1,GETDATE()),
		  (1030,46,'ASOC. DE HECHO DE PROFESIONALES',1,GETDATE()),
		  (1030,47,'CAFAES Y SUBCAFAES',1,GETDATE()),
		  (1030,48,'SINDICATOS Y FEDERACIONES',1,GETDATE()),
		  (1030,49,'COLEGIOS PROFESIONALES',1,GETDATE()),
		  (1030,50,'COMITES INSCRITOS',1,GETDATE()),
		  (1030,51,'ORGANIZACIONES SOCIALES DE BASE',1,GETDATE())
GO

 	 ---- TABLA 1033 : SECTOR DEL CONTRIBUYENTE -------
  INSERT INTO [Security].Constant
        (ConstantClass,ConstantID,ConstantAbbreviation,ConstantName,StateID)
  VALUES (1033	,0	,'T.S.C','TABLA SECTOR DEL CONTRIBUYENTE',1),
         (1033	,1,'','PUBLICO',1),
		 (1033	,2,'','PRIVADO',1),	  
		 (1033	,3,'','OTROS',1)

GO
 	 ---- TABLA 1034 : RUBRO DEL CONTRIBUYENTE -------
  INSERT INTO [Security].Constant
        (ConstantClass,ConstantID,ConstantAbbreviation,ConstantName,StateID)
  VALUES (1034	,0	,'T.S.C','TABLA RUBRO DEL CONTRIBUYENTE',1),
         (1034	,1,'','TEXTIL',1),
		 (1034	,2,'','SEGURIDAD Y RESGUARDO',1),	  
		 (1034	,3,'','FARMACIA',1),
		 (1034	,4,'','CENTRO MEDICO',1),
		 (1034	,99,'','VARIOS',1) 
GO
GO
	  ---- TABLA 1040 : TIPO DE MONEDA -------
  INSERT INTO [Security].Constant
        (ConstantClass,ConstantID,ConstantAbbreviation,ConstantName,StateID)
  VALUES (1040	,0	,'T.T.M','TIPO DE MONEDA',1),
         (1040	,1,'PEN','Soles',1),
		 (1040	,2,'USD','Dolares',1),	  
		 (1040	,3,'','Euros',0)
 
 GO
 	   --- TABLA 1065 : SUNAT TIPO AFECTACION ------
  INSERT INTO [Security].Constant		 
        (ConstantClass,ConstantID,ConstantAbbreviation,ConstantName,StateID)
  VALUES (1065	,0	,'00','SUNAT:TIPO AFECTACION IGV VENTA',1),
         (1065	,10,'1','10:Gravado - Operación Onerosa',1),
		 (1065	,11,'4','11:[Gratuita] Gravado – Retiro por premio',1),		  
		 (1065	,12,'4','12:[Gratuita] Gravado – Retiro por donación',1)	,
		 (1065	,13,'4','13:[Gratuita] Gravado – Retiro ',1),
		 (1065	,14,'4','14:[Gratuita] Gravado – Retiro por publicidad',1),
		 (1065	,15,'4','15:[Gratuita] Gravado – Bonificaciones',1),
		 (1065	,16,'4','16:[Gratuita] Gravado – Retiro por entrega a trabajadores',1),
         (1065	,17,'5','17:Gravado – IVAP',1),
		 (1065	,20,'2','20:Exonerado - Operación Onerosa',1),
         (1065	,21,'4','21:[Gratuita] Exonerado – Transferencia Gratuita',1),
		 (1065	,30,'3','30:Inafecto - Operación Onerosa',1),
		 (1065	,31,'4','31:[Gratuita] Inafecto – Retiro por Bonificación',1),
		 (1065	,32,'4','32:[Gratuita] Inafecto – Retiro',1),
		 (1065	,33,'4','33:[Gratuita] Inafecto – Retiro por Muestras Médicas',1),
		 (1065	,34,'4','34:[Gratuita] Inafecto - Retiro por Convenio Colectivo',1),
		 (1065	,35,'4','35:[Gratuita] Inafecto – Retiro por premio',1),
		 (1065	,36,'4','36:[Gratuita] Inafecto - Retiro por publicidad',1),
		 (1065	,40,'3','40:Exportación',1),
		 (1065	,41,'4','101:[Gratuita] Gravado - IVAP',1)

GO