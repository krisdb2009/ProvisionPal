CREATE TABLE [dbo].[Requests] (
    [RequestID]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [FormID]           UNIQUEIDENTIFIER NOT NULL,
    [RequestedBy]      TEXT             NOT NULL,
    [RequestedTime]    DATETIME         DEFAULT (sysdatetime()) NOT NULL,
    [LastModifiedBy]   TEXT             NOT NULL,
    [LastModifiedTime] DATETIME         DEFAULT (sysdatetime()) NOT NULL,
    [ProcessedBy]      TEXT             NULL,
    [ProcessedTime]    DATETIME         NULL,
    [Status]           TEXT             DEFAULT ('New') NOT NULL,
    PRIMARY KEY CLUSTERED ([RequestID] ASC),
    CONSTRAINT [FK_Requests_Forms] FOREIGN KEY ([FormID]) REFERENCES [dbo].[Forms] ([FormID])
);





