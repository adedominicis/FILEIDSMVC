CREATE TABLE [dbo].[directorios] (
    [id_directorio]     INT           IDENTITY (1, 1) primary key NOT NULL,
    [nombre_directorio] VARCHAR (200) NOT NULL,
    [descriptor_directorio]      VARCHAR (500) NOT NULL, 
    [activo] BIT NOT NULL DEFAULT 1, 
    [id_padre] INT NULL
    CONSTRAINT [FK_DIRECTORIO_PADRE] FOREIGN KEY ([id_padre]) REFERENCES [dbo].[directorios] ([id_directorio]), 
    [id_raiz] INT NULL
);

