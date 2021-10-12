CREATE TABLE [dbo].[metadata] (
    [id_metadata]              INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [descriptores]    VARCHAR (255) NOT NULL,
    [descriptoren]    VARCHAR (255) NOT NULL,
    [oemsku]          VARCHAR (255) NULL,
    [descriptorextra] VARCHAR (255) NULL,
);

