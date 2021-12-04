CREATE TABLE [dbo].[ScriptGroups] (
    [ScriptGroupID] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Enabled]       BIT              DEFAULT ((1)) NOT NULL,
    [Hidden]        BIT              DEFAULT ((0)) NOT NULL,
    [Name]          VARCHAR (50)     DEFAULT ('') NOT NULL,
    [Description]   TEXT             NULL,
    PRIMARY KEY CLUSTERED ([ScriptGroupID] ASC)
);

