CREATE TABLE [dbo].[Scripts] (
    [ScriptID]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Enabled]     BIT              DEFAULT ((1)) NOT NULL,
    [Name]        VARCHAR (50)     DEFAULT ('') NOT NULL,
    [Description] TEXT             NULL,
    [Script]      TEXT             DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([ScriptID] ASC)
);

