CREATE TABLE [dbo].[metadata] (
    [id_metadata]              INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [descriptores]    VARCHAR (500) NULL,
    [descriptoren]    VARCHAR (500) NULL,
    [oemsku]          VARCHAR (500) NULL,
    [descriptorextra] VARCHAR (500) NULL, 
    [partid] VARCHAR(30) NULL,
);

