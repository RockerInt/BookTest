CREATE TABLE [dbo].[Libros] (
    [ISBN]           INT          IDENTITY (1, 1) NOT NULL,
    [Editoriales_Id] INT          NULL,
    [Titulo]         VARCHAR (45) NOT NULL,
    [Sinopsis]       TEXT         NOT NULL,
    [N_Paginas]      VARCHAR (45) NOT NULL,
    CONSTRAINT [PK_Libros] PRIMARY KEY CLUSTERED ([ISBN] ASC),
    CONSTRAINT [FK_Libros_Editoriales] FOREIGN KEY ([Editoriales_Id]) REFERENCES [dbo].[Editoriales] ([Id])
);

