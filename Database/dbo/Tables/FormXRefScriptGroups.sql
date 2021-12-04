CREATE TABLE [dbo].[FormXRefScriptGroups] (
    [XRefID]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [FormID]        UNIQUEIDENTIFIER NOT NULL,
    [ScriptGroupID] UNIQUEIDENTIFIER NOT NULL,
    [Order]         INT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([XRefID] ASC),
    CONSTRAINT [FK_FormsXRefScriptGroups_ScriptGroups] FOREIGN KEY ([ScriptGroupID]) REFERENCES [dbo].[ScriptGroups] ([ScriptGroupID])
);

