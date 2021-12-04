CREATE TABLE [dbo].[Parameters] (
    [ParameterID]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]              TEXT             NOT NULL,
    [Description]       TEXT             NULL,
    [RegularExpression] TEXT             NULL,
    [Required]          BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ParameterID] ASC)
);

