USE [Sorveteria]
GO

/****** Objeto: Table [dbo].[Sabores] Data do Script: 01/12/2024 22:20:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sabores] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Nome]  NVARCHAR (MAX) NULL,
    [Valor] FLOAT (53)     NULL
);


