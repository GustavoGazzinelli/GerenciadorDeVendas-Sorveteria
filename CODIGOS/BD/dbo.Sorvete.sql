USE [Sorveteria]
GO

/****** Objeto: Table [dbo].[Sorvete] Data do Script: 01/12/2024 22:20:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sorvete] (
    [Id]    INT        IDENTITY (1, 1) NOT NULL,
    [Peso]  FLOAT (53) NULL,
    [Valor] FLOAT (53) NULL,
    [Data]  DATETIME   NULL
);


