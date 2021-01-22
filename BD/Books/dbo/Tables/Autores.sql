CREATE TABLE [dbo].[Autores] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Nombre]    VARCHAR (45) NOT NULL,
    [Apellidos] VARCHAR (45) NOT NULL,
    CONSTRAINT [PK_Autores] PRIMARY KEY CLUSTERED ([Id] ASC)
);

