CREATE TABLE [dbo].[directorios] (
    [id_directorio]     INT           IDENTITY (1, 1) primary key NOT NULL,
    [nombre_directorio] VARCHAR (200) NULL,
    [descriptor_directorio]      VARCHAR (500) NULL
);

