CREATE TABLE [dbo].[Client] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [RazonSocial] VARCHAR (250) NOT NULL,
    [Responsable] VARCHAR (250) NOT NULL,
    [Cuit]        VARCHAR (50)  NOT NULL,
    [Dni]         VARCHAR (50)  NOT NULL,
    [Telefono]    VARCHAR (50)  NULL,
    [Direccion]   VARCHAR (250) NOT NULL,
	[Celular]	VARCHAR(50) NULL,
	[IsDeleted]	BIT NOT NULL,
	[CreatedDate] DATETIME2(7) NOT NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([Id] ASC)
);

