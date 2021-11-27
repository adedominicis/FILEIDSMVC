﻿CREATE TABLE [dbo].[almacenamiento]
(
	[ID_ALMACENAMIENTO] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RUTA] VARCHAR(256) NOT NULL,
	[VERSION_ARCHIVO] INT NOT NULL, 
	[ID_ARCHIVO] INT NOT NULL, 
    [ID_METADATA] INT NOT NULL, 
    [ID_EXTENSION] INT NOT NULL,
	[ID_REVISION] INT NOT NULL, 
    [MD5] VARCHAR(32) NULL, 
    CONSTRAINT [FK_ALMACENAMIENTO_METADATA] FOREIGN KEY ([id_metadata]) REFERENCES [dbo].[metadata] ([id_metadata]),
	CONSTRAINT [FK_ALMACENAMIENTO_ARCHIVO] FOREIGN KEY ([id_archivo]) REFERENCES [dbo].[archivos] ([id_archivo]),
	CONSTRAINT [FK_ALMACENAMIENTO_EXTENSIONES] FOREIGN KEY ([id_extension]) REFERENCES [extensiones]([id_extension]),
	CONSTRAINT [FK_ALMACENAMIENTO_REVISIONES] FOREIGN KEY ([ID_REVISION]) REFERENCES [dbo].[revisionlevels] ([ID_REVISIONLEVEL]), 

)