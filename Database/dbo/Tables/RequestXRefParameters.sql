CREATE TABLE [dbo].[RequestXRefParameters] (
    [XRefID]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [RequestID]   UNIQUEIDENTIFIER NOT NULL,
    [ParameterID] UNIQUEIDENTIFIER NOT NULL,
    [Value]       TEXT             NULL,
    PRIMARY KEY CLUSTERED ([XRefID] ASC),
    CONSTRAINT [FK_RequestXRefParameters_Parameters] FOREIGN KEY ([ParameterID]) REFERENCES [dbo].[Parameters] ([ParameterID]),
    CONSTRAINT [FK_RequestXRefParameters_Requests] FOREIGN KEY ([RequestID]) REFERENCES [dbo].[Requests] ([RequestID])
);

