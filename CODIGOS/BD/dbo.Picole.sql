CREATE TABLE [dbo].[Picole] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [SaborID]      INT NULL,
    [Data]       DATETIME           NULL,
    [Quantidade] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Sabor] FOREIGN KEY ([SaborID]) REFERENCES [Sabores]([Id])
);

