CREATE TABLE [dbo].[Editoriales] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Nombre] VARCHAR (45) NOT NULL,
    [Sede]   VARCHAR (45) NOT NULL,
    CONSTRAINT [PK_Editoriales] PRIMARY KEY CLUSTERED ([Id] ASC)
);

