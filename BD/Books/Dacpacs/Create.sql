USE [master]
GO

/****** Object:  Database [Books]    Script Date: 2021-01-25 2:40:00 AM ******/
CREATE DATABASE [Books2]
 CONTAINMENT = NONE
GO

ALTER DATABASE [Books] SET  READ_WRITE 
GO

USE [Books2]
GO

CREATE TABLE [dbo].[Editoriales] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Nombre] VARCHAR (45) NOT NULL,
    [Sede]   VARCHAR (45) NOT NULL,
    CONSTRAINT [PK_Editoriales] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Libros] (
    [ISBN]           INT          IDENTITY (1, 1) NOT NULL,
    [Editoriales_Id] INT          NULL,
    [Titulo]         VARCHAR (45) NOT NULL,
    [Sinopsis]       TEXT         NOT NULL,
    [N_Paginas]      VARCHAR (45) NOT NULL,
    CONSTRAINT [PK_Libros] PRIMARY KEY CLUSTERED ([ISBN] ASC),
    CONSTRAINT [FK_Libros_Editoriales] FOREIGN KEY ([Editoriales_Id]) REFERENCES [dbo].[Editoriales] ([Id])
);
GO

CREATE TABLE [dbo].[Autores] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Nombre]    VARCHAR (45) NOT NULL,
    [Apellidos] VARCHAR (45) NOT NULL,
    CONSTRAINT [PK_Autores] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Autores_has_Libros] (
    [Autores_Id]  INT NOT NULL,
    [Libros_ISBN] INT NOT NULL,
    CONSTRAINT [PK_Autores_has_Libros] PRIMARY KEY CLUSTERED ([Autores_Id] ASC, [Libros_ISBN] ASC),
    CONSTRAINT [FK_Autores_has_Libros_Autores] FOREIGN KEY ([Autores_Id]) REFERENCES [dbo].[Autores] ([Id]),
    CONSTRAINT [FK_Autores_has_Libros_Libros] FOREIGN KEY ([Libros_ISBN]) REFERENCES [dbo].[Libros] ([ISBN])
);
GO
