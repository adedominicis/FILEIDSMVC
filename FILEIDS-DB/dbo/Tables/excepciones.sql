CREATE TABLE [dbo].[excepciones] (
    [id]      INT           IDENTITY (1, 1) NOT NULL,
    [mensaje] VARCHAR (300) NOT NULL,
    [fecha]   DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

