CREATE TABLE [dbo].[archivos] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [id_metadata]       INT           NOT NULL,
    [nombre_archivo]             VARCHAR (255) NOT NULL,
    [id_revisionlevel] INT           NOT NULL,
    [id_carpeta_padre]      INT NULL,
    [id_extension] INT NULL, 
    [activo] BIT NOT NULL , 
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_RUTAS_METADATA] FOREIGN KEY ([id_metadata]) REFERENCES [dbo].[metadata] ([id_metadata]),
    CONSTRAINT [FK_RUTAS_REVISIONES] FOREIGN KEY ([id_revisionlevel]) REFERENCES [dbo].[revisionlevels] ([id]), 
    CONSTRAINT [FK_RUTAS_PROYECTOS] FOREIGN KEY ([id_carpeta_padre]) REFERENCES [directorios]([id_directorio]), 
    CONSTRAINT [FK_RUTAS_EXTENSIONES] FOREIGN KEY ([id_extension]) REFERENCES [extensiones]([id_extension])
);

