CREATE TABLE [dbo].[Requests] (
    [RequestID]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [FormID]           UNIQUEIDENTIFIER NOT NULL,
    [RequestedBy]      VARCHAR (50)     NOT NULL,
    [RequestedTime]    DATETIME         DEFAULT (sysdatetime()) NOT NULL,
    [LastModifiedBy]   VARCHAR (50)     NOT NULL,
    [LastModifiedTime] DATETIME         DEFAULT (sysdatetime()) NOT NULL,
    [ProcessedBy]      VARCHAR (50)     NULL,
    [ProcessedTime]    DATETIME         NULL,
    [Status]           VARCHAR (20)     DEFAULT ('New') NOT NULL,
    PRIMARY KEY CLUSTERED ([RequestID] ASC),
    CONSTRAINT [FK_Requests_Forms] FOREIGN KEY ([FormID]) REFERENCES [dbo].[Forms] ([FormID])
);







