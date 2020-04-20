IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Languages] (
    [Id] int NOT NULL IDENTITY,
    [Culture] nvarchar(max) NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Countries] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Area] nvarchar(max) NULL,
    [Population] nvarchar(max) NULL,
    [LanguageId] int NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Countries_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Provinces] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Area] nvarchar(max) NULL,
    [Population] nvarchar(max) NULL,
    [CountryId] int NULL,
    CONSTRAINT [PK_Provinces] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Provinces_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Countries_LanguageId] ON [Countries] ([LanguageId]);

GO

CREATE INDEX [IX_Provinces_CountryId] ON [Provinces] ([CountryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200330094524_0001', N'3.1.3');

GO

