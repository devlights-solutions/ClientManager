CREATE TABLE [dbo].[Payment] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Secuencia]        INT           NOT NULL,
    [Pagado]           BIT           NOT NULL,
    [FechaPago]        DATETIME2 (7) NULL,
    [Monto]            MONEY         NOT NULL,
    [FechaVencimiento] DATETIME2 (7) NULL,
    [ProjectId]        INT           NOT NULL,
    [IsDeleted]        BIT           NOT NULL,
    [CreatedDate]      DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payment_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);







