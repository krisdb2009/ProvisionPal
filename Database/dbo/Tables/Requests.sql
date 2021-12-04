CREATE TABLE [dbo].[Requests] (
    [RequestID]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [RequestedBy]   TEXT             NOT NULL,
    [RequestedTime] DATETIME         DEFAULT (sysdatetime()) NOT NULL,
    [ProcessedBy]   TEXT             NULL,
    [ProcessedTime] DATETIME         NULL,
    [Status]        TEXT             DEFAULT ('New') NOT NULL,
    PRIMARY KEY CLUSTERED ([RequestID] ASC)
);

