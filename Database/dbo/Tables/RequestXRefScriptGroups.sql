CREATE TABLE [dbo].[RequestXRefScriptGroups] (
    [XRefID]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [RequestID]     UNIQUEIDENTIFIER NOT NULL,
    [ScriptGroupID] UNIQUEIDENTIFIER NOT NULL,
    [RunTime]       DATETIME         NULL,
    [Status]        TEXT             DEFAULT ('Un-processed') NOT NULL,
    PRIMARY KEY CLUSTERED ([XRefID] ASC),
    CONSTRAINT [FK_RequestXRefScriptGroups_RequestID] FOREIGN KEY ([RequestID]) REFERENCES [dbo].[Requests] ([RequestID]),
    CONSTRAINT [FK_RequestXRefScriptGroups_ScriptGroupID] FOREIGN KEY ([ScriptGroupID]) REFERENCES [dbo].[ScriptGroups] ([ScriptGroupID])
);

