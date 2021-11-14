CREATE TABLE [dbo].[archivos] (
    [id_archivo]               INT           IDENTITY (1, 1) NOT NULL,
    [nombre_archivo]             VARCHAR (255) NOT NULL,
    [id_revisionlevel] INT           NOT NULL,
    [id_carpeta_padre]      INT NOT NULL,
    [activo] BIT NOT NULL , 
    PRIMARY KEY CLUSTERED ([id_archivo] ASC),
    CONSTRAINT [FK_ARCHIVO_PROYECTOS] FOREIGN KEY ([id_carpeta_padre]) REFERENCES [directorios]([id_directorio]), 
    
);

