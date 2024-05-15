CREATE TABLE [dbo].[Banks] (
    [Id]      INT        NOT NULL,
    [Name]    NCHAR (50) NOT NULL,
    [Bic]     NCHAR (20) NOT NULL,
    [Country] NCHAR (2)  DEFAULT ('NO') NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);