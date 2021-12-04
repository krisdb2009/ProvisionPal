CREATE TABLE [dbo].[ScriptGroupXRefScripts] (
    [XRefID]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ScriptGroupID]      UNIQUEIDENTIFIER NOT NULL,
    [ChildScriptGroupID] UNIQUEIDENTIFIER NULL,
    [ChildScriptID]      UNIQUEIDENTIFIER NULL,
    [Order]              INT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([XRefID] ASC),
    CONSTRAINT [FK_ScriptGroupXRef_ScriptGroups0] FOREIGN KEY ([ScriptGroupID]) REFERENCES [dbo].[ScriptGroups] ([ScriptGroupID]),
    CONSTRAINT [FK_ScriptGroupXRef_ScriptGroups1] FOREIGN KEY ([ChildScriptGroupID]) REFERENCES [dbo].[ScriptGroups] ([ScriptGroupID]),
    CONSTRAINT [FK_ScriptGroupXRef_Scripts] FOREIGN KEY ([ChildScriptID]) REFERENCES [dbo].[Scripts] ([ScriptID])
);

