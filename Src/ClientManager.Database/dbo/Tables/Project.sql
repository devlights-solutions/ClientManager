CREATE TABLE [dbo].[Project] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]      VARCHAR (250) NOT NULL,
    [Descripcion] VARCHAR (MAX) NULL,
    [FechaInicio] DATE          NULL,
    [ClientId]    INT           NOT NULL,
    [IsDeleted]   BIT           NOT NULL,
    [CreatedDate] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Project_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id])
);

