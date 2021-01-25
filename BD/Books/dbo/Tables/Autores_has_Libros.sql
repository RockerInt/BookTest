CREATE TABLE [dbo].[Autores_has_Libros] (
    [Autores_Id]  INT NOT NULL,
    [Libros_ISBN] INT NOT NULL,
    CONSTRAINT [PK_Autores_has_Libros] PRIMARY KEY CLUSTERED ([Autores_Id] ASC, [Libros_ISBN] ASC),
    CONSTRAINT [FK_Autores_has_Libros_Autores] FOREIGN KEY ([Autores_Id]) REFERENCES [dbo].[Autores] ([Id]),
    CONSTRAINT [FK_Autores_has_Libros_Libros] FOREIGN KEY ([Libros_ISBN]) REFERENCES [dbo].[Libros] ([ISBN])
);



