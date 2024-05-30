IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

BEGIN TRANSACTION;

CREATE TABLE [MBAOptions] (
    [Id] int NOT NULL IDENTITY,
    [Country] nvarchar(max) NOT NULL,
    [CountryCode] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MBAOptions] PRIMARY KEY ([Id])
);

CREATE TABLE [MBAs] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [MBAOptionId] int NOT NULL,
    CONSTRAINT [PK_MBAs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MBAs_MBAOptions_MBAOptionId] FOREIGN KEY ([MBAOptionId]) REFERENCES [MBAOptions] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_MBAs_MBAOptionId] ON [MBAs] ([MBAOptionId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240529083512_InitialCreate', N'7.0.20');

COMMIT;

