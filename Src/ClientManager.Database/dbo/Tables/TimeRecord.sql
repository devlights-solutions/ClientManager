CREATE TABLE [dbo].[TimeRecord] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Descripcion] VARCHAR (MAX) NOT NULL,
    [Fecha]       DATETIME2 (7) NULL,
    [HoraDesde]   TIME (7)      NULL,
    [HoraHasta]   TIME (7)      NULL,
    [Pagado]      BIT           NOT NULL,
    [ProjectId]   INT           NOT NULL,
    [UserId]      INT           NOT NULL,
    [IsDeleted]   BIT           NOT NULL,
    [CreatedDate] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_TimeRecord] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TimeRecord_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [FK_TimeRecord_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);





