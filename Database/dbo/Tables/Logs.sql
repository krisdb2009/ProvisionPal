CREATE TABLE [dbo].[Logs] (
    [LogID]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Time]     DATETIME         DEFAULT (sysdatetime()) NOT NULL,
    [Level]    TINYINT          DEFAULT ((255)) NOT NULL,
    [Identity] TEXT             NOT NULL,
    [Source]   TEXT             NOT NULL,
    [Message]  TEXT             NOT NULL,
    PRIMARY KEY CLUSTERED ([LogID] ASC)
);

