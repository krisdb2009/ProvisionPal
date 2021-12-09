CREATE TABLE [dbo].[Scripts] (
    [ScriptID]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Enabled]     BIT              DEFAULT ((1)) NOT NULL,
    [Name]        VARCHAR (50)     DEFAULT ('') NOT NULL,
    [Description] VARCHAR (300)    NULL,
    [Script]      VARCHAR (MAX)    DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([ScriptID] ASC)
);



